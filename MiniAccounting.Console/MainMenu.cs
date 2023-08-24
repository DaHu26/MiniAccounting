using MiniAccountingConsole.Core;
using MiniAccountingConsole.Logger;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;


namespace MiniAccountingConsole
{
    internal class MainMenu
    {
        private Operator _operator;
        private readonly ILogger _logger;

        public MainMenu(ILogger logger)
        {
            _logger = logger;
            _operator = new Operator(logger);
        }

        public void Start()
        {
            _operator.Users.Add(new User("Dan", 0));
            _operator.Users.Add(new User("Val", 0));
            _operator.Users.Add(new User("Sne", 0));
            SayHi();
            RegisterOrDeleteUsers();
            StartMenu();
        }

        private void StartMenu()
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
                        OperationsWithPersonalAccount();
                        break;
                    case 2:
                        TopUpTotalBalance();
                        break;
                    case 3:
                        RemoveFromTotalBalance();
                        break;
                    case 4:
                        _logger.WriteLine($"Ваш текущий баланс: {_operator.TotalMoney}");
                        break;
                    case 5:
                        _logger.WriteLine("Пока.");
                        onOff = false;
                        break;

                }
            }
        }

        private void RegisterOrDeleteUsers()
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
                    AddUser();
                    break;
                case 2:
                    DeleteUser();
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

        private void OperationsWithPersonalAccount()
        {
            _logger.WriteLine($"Вы выбрали операции с личным аккаунтом.");
            _logger.WriteLine("Выберите аккаунт для взаимодействия.");
            for (int i = 0; i < _operator.Users.Count; i++)
            {
                var currentUser = _operator.Users[i];
                _logger.WriteLine($"{i + 1} - {currentUser.Name} {currentUser.Money}");
            }
            var choose1 = Convert.ToInt32(Console.ReadLine());

            switch (choose1)
            {
                case 1:
                    _logger.WriteLine($"Вы выбрали аккаунт - {_operator.Users[choose1 - 1]}");

                    break;
                case 2:
                    _logger.WriteLine($"Вы выбрали аккаунт - {_operator.Users[choose1 - 1]}");

                    break;
                case 3:
                    _logger.WriteLine($"Вы выбрали аккаунт - {_operator.Users[choose1 - 1]}");

                    break;
            }
        }

        private void TopUpTotalBalance()
        {
            _logger.WriteLine("Вы выбрали операцию пополнения общего баланса.");
            _logger.WriteLine($"Ваш текущий баланс: {_operator.TotalMoney}");
            _logger.WriteLine("Введите сумму для пополнения.");
            var addMoney = Convert.ToInt32(Console.ReadLine());
            _logger.WriteLine("Введите комментариий.");
            var comment = Console.ReadLine();
            _operator.TopUpTotalBalance(addMoney, comment);
            _logger.WriteLine($"Ваш текущий баланс: {_operator.TotalMoney}");
        }

        private void RemoveFromTotalBalance()
        {
            _logger.WriteLine("Вы выбрали операцию снятия с общего баланса.");
            _logger.WriteLine($"Ваше текущий баланс: {_operator.TotalMoney}");
            _logger.WriteLine("Введите сумму для снятия.");
            var takeOffMoney = Convert.ToInt32(Console.ReadLine());
            _logger.WriteLine("Введите комментарий.");
            var comment = Console.ReadLine();
            _operator.RemoveFromTotalBalance(takeOffMoney, comment);
            _logger.WriteLine($"Ваше текущий баланс: {_operator.TotalMoney}");
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

        private void AddUser()
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
            _operator.Users.Add(newUser);
        }

        private void DeleteUser()
        {
            _logger.WriteLine("Выберите юзера для удаления.");
            for (int i = 0; i < _operator.Users.Count; i++)
            {
                _logger.WriteLine($"{i + 1} - {_operator.Users[i]}");
            }
            var choose = Convert.ToInt32(Console.ReadLine());
            _operator.Users.RemoveAt(choose - 1);
            for (int i = 0; i < _operator.Users.Count; i++)
            {
                _logger.WriteLine($"{i + 1} - {_operator.Users[i]}");
            }
        }
    }
}
