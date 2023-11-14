using MiniAccounting.Infrastructure.DataKeepers;
using Console = System.Console;

namespace MiniAccounting.UIConsole;

public class MainMenu
{
    private readonly ILogger _logger;
    private readonly MiniAccountingClient _client;

    public MainMenu(ILogger logger, MiniAccountingClient client)
    {
        _logger = logger;
        _client = client;
    }

    public async Task StartAsync()
    {
        SayHi();
        await RegisterOrDeleteUsersAsync();
        await StartMenuAsync();
    }

    private async Task StartMenuAsync()
    {
        var onOff = true;
        while (onOff)
        {
            var choose = ChooseOperation();

            while (choose < 1 || choose > 5)
            {
                _logger.WriteLine("Выбрана неизвестная операция, повторите ввод.");
                choose = Convert.ToInt32(Console.ReadLine());
            }

            switch (choose)
            {
                case 1:
                    await OperationsWithPersonalAccountAsync();
                    break;
                case 2:
                    await TopUpTotalBalanceAsync();
                    break;
                case 3:
                    await RemoveFromTotalBalanceAsync();
                    break;
                case 4:
                    _logger.WriteLine($"Ваш текущий баланс: {_client.GetTotalBalanceAsync().Result}");
                    break;
                case 5:
                    _logger.WriteLine("Пока.");
                    onOff = false;
                    break;
            }
        }
    }

    private async Task RegisterOrDeleteUsersAsync()
    {
        _logger.WriteLine("1 - Добавить пользователя 2 - Убрать пользователя");
        var choose = Convert.ToInt32(Console.ReadLine());

        while (choose < 1 && choose > 2)
        {
            _logger.WriteLine("Выбрана неизвестная операция.");
            _logger.WriteLine("Выберите заново.");
            choose = Convert.ToInt32(Console.ReadLine());
        }

        switch (choose)
        {
            case 1:
                await AddUserAsync();
                break;
            case 2:
                await DeleteUserAsync();
                break;
        }
    }

    private string ReadAndValidateString()
    {
        var str = Console.ReadLine();
        _logger.WriteLine($"Вы написали: {str}, это верно? 1 - Да 2 - Нет.");
        var choose = Convert.ToInt32(Console.ReadLine());
        while (choose != 1)
        {
            _logger.WriteLine("Введите повторно.");
            str = Console.ReadLine();
            _logger.WriteLine($"Вы написали: {str}, это верно? 1 - Да 2 - Нет.");
            choose = Convert.ToInt32(Console.ReadLine());
        }
        return str;
    }

    private void SayHi()
    {
        _logger.WriteLine("Вас приведствует программа \"Mini Accounting\"");
        _logger.WriteLine("Здесь вы можете вести учет потраченных и заработанных средств.");
    }

    private async Task OperationsWithPersonalAccountAsync()
    {
        _logger.WriteLine($"Вы выбрали операции с личным аккаунтом.");
        _logger.WriteLine("Выберите аккаунт для взаимодействия.");
        var users = await _client.ReadUsersAsync();
        for (int i = 0; i < users.Count; i++)
        {
            var currentUser = users[i];
            _logger.WriteLine($"{i + 1} - {currentUser.Name} {currentUser.Money}");
        }
        var choose1 = Convert.ToInt32(Console.ReadLine());

        // TODO : Реализовать механику передачи денег между юзерами
        switch (choose1)
        {
            case 1:
                _logger.WriteLine($"Вы выбрали аккаунт - {users[choose1 - 1]}");

                break;
            case 2:
                _logger.WriteLine($"Вы выбрали аккаунт - {users[choose1 - 1]}");

                break;
            case 3:
                _logger.WriteLine($"Вы выбрали аккаунт - {users[choose1 - 1]}");

                break;
        }
    }

    private async Task TopUpTotalBalanceAsync()
    {
        _logger.WriteLine("Вы выбрали операцию пополнения общего баланса.");
        _logger.WriteLine($"Ваш текущий баланс: {await _client.GetTotalBalanceAsync()}");
        _logger.WriteLine("Введите сумму для пополнения.");
        var addMoney = Convert.ToInt32(Console.ReadLine());
        _logger.WriteLine("Введите комментариий.");
        var comment = Console.ReadLine();
        await _client.TopUpTotalBalanceAsync(addMoney, comment);
        _logger.WriteLine($"Ваш текущий баланс: {await _client.GetTotalBalanceAsync()}");
    }

    private async Task RemoveFromTotalBalanceAsync()
    {
        _logger.WriteLine("Вы выбрали операцию снятия с общего баланса.");
        _logger.WriteLine($"Ваше текущий баланс: {await _client.GetTotalBalanceAsync()}");
        _logger.WriteLine("Введите сумму для снятия.");
        var takeOffMoney = Convert.ToInt32(Console.ReadLine());
        _logger.WriteLine("Введите комментарий.");
        var comment = Console.ReadLine();
        await _client.RemoveFromTotalBalanceAsync(takeOffMoney, comment);
        _logger.WriteLine($"Ваше текущий баланс: {await _client.GetTotalBalanceAsync()}");
    }

    private int ChooseOperation()
    {
        _logger.WriteLine("Выберите необходимую вам операцию:");
        _logger.WriteLine($"1 - Операции с личным аккаунтом.");
        _logger.WriteLine($"2 - Пополнить общий баланс.");
        _logger.WriteLine($"3 - Снять с общего баланс.");
        _logger.WriteLine($"4 - Проверить общий баланс.");
        _logger.WriteLine($"5 - Завершить работу.");
        return Convert.ToInt32(Console.ReadLine());
    }

    private async Task AddUserAsync()
    {

        _logger.WriteLine("Введите имя нового аккаунта");
        var chooseName = ReadAndValidateString();

        _logger.WriteLine("Введите начальное количество денег на счету у этого аккаунта.");
        var chooseMoney = Convert.ToInt32(Console.ReadLine());
        _logger.WriteLine($"Количество денег: {chooseMoney}, это верно? 1 - Да 2 - Нет.");
        var choose = Convert.ToInt32(Console.ReadLine());
        while (choose != 1)
        {
            _logger.WriteLine("Введите количество денег повторно.");
            chooseMoney = Convert.ToInt32(Console.ReadLine());
            _logger.WriteLine($"Количество денег: {chooseMoney}, это верно? 1 - Да 2 - Нет.");
            choose = Convert.ToInt32(Console.ReadLine());
        }

        var newUser = new User(chooseName, chooseMoney);

        await _client.SaveAsync(newUser);
    }

    private async Task DeleteUserAsync()
    {
        _logger.WriteLine("Выберите юзера для удаления.");
        var users = await _client.ReadUsersAsync();
        for (int i = 0; i < users.Count; i++)
        {
            _logger.WriteLine($"{i + 1} - {users[i]}");
        }
        var choose = Convert.ToInt32(Console.ReadLine());
        var user = users[choose - 1];
        await _client.DeleteAsync(user.Uid);
        users.RemoveAt(choose - 1);
        for (int i = 0; i < users.Count; i++)
        {
            _logger.WriteLine($"{i + 1} - {users[i]}");
        }
    }
}
