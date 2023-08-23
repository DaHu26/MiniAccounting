using MiniAccountingConsole.Core;
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

        public MainMenu()
        {
            _operator = new Operator();
        }

        public void Start()
        {
            _operator.Users.Add(new User("Dan", 0));
            _operator.Users.Add(new User("Val", 0));
            _operator.Users.Add(new User("Sne", 0));
            SayHi();
            RegisterOrDeleteUsers(_operator.Users);
            StartMenu(_operator.Users);
        }

        private void StartMenu(List<User> users)
        {
            var onOff = true;
            while (onOff)
            {
                var choose = ChooseOperation();

                while (choose < 1 || choose > 5)
                {
                    Console.WriteLine("Выбрана неизвестная операция, повторите ввод.");
                    choose = Convert.ToInt32(Console.ReadLine());
                }

                switch (choose)
                {
                    case 1:
                        OperationsWithPersonalAccount(users);
                        break;
                    case 2:
                        TopUpTotalBalance();
                        break;
                    case 3:
                        RemoveFromTotalBalance();
                        break;
                    case 4:
                        Console.WriteLine($"Ваш текущий баланс: {_operator.TotalMoney}");
                        break;
                    case 5:
                        Console.WriteLine("Пока.");
                        onOff = false;
                        break;

                }
            }
        }

        private void RegisterOrDeleteUsers(List<User> users)
        {
            Console.WriteLine("1 - Добавить пользователя 2 - Убрать пользователя");
            var choose = Convert.ToInt32(Console.ReadLine());

            while (choose < 1 && choose > 2)
            {
                Console.WriteLine("Выбрана неизвестная операция.");
                Console.WriteLine("Выберите заново.");
                choose = Convert.ToInt32(Console.ReadLine());
            }

            switch (choose)
            {
                case 1:
                    AddUser(users);
                    break;
                case 2:
                    DeleteUser(users);
                    break;
            }
        }

        private string ReadAndValidateString()
        {
            var str = Console.ReadLine();
            Console.WriteLine($"Вы написали: {str}, это верно? 1 - Да 2 - Нет.");
            var choose = Convert.ToInt32(Console.ReadLine());
            while (choose != 1)
            {
                Console.WriteLine("Введите повторно.");
                str = Console.ReadLine();
                Console.WriteLine($"Вы написали: {str}, это верно? 1 - Да 2 - Нет.");
                choose = Convert.ToInt32(Console.ReadLine());
            }
            return str;
        }

        private void SayHi()
        {
            Console.WriteLine("Вас приведствует программа \"Mini Accounting\"");
            Console.WriteLine("Здесь вы можете вести учет потраченных и заработанных средств.");
        }

        private void OperationsWithPersonalAccount(List<User> users)
        {
            Console.WriteLine($"Вы выбрали операции с личным аккаунтом.");
            Console.WriteLine("Выберите аккаунт для взаимодействия.");
            for (int i = 0; i < users.Count; i++)
            {
                var currentUser = users[i];
                Console.WriteLine($"{i + 1} - {currentUser.Name} {currentUser.Money}");
            }
            var choose1 = Convert.ToInt32(Console.ReadLine());

            switch (choose1)
            {
                case 1:
                    Console.WriteLine($"Вы выбрали аккаунт - {users[choose1 - 1]}");

                    break;
                case 2:
                    Console.WriteLine($"Вы выбрали аккаунт - {users[choose1 - 1]}");

                    break;
                case 3:
                    Console.WriteLine($"Вы выбрали аккаунт - {users[choose1 - 1]}");

                    break;
            }
        }

        private void TopUpTotalBalance()
        {
            Console.WriteLine("Вы выбрали операцию пополнения общего баланса.");
            Console.WriteLine($"Ваш текущий баланс: {_operator.TotalMoney}");
            Console.WriteLine("Введите сумму для пополнения.");
            var addMoney = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Введите комментариий.");
            var comment = Console.ReadLine();
            _operator.TopUpTotalBalance(addMoney, comment);
            Console.WriteLine($"Ваш текущий баланс: {_operator.TotalMoney}");
        }

        private void RemoveFromTotalBalance()
        {
            Console.WriteLine("Вы выбрали операцию снятия с общего баланса.");
            Console.WriteLine($"Ваше текущий баланс: {_operator.TotalMoney}");
            Console.WriteLine("Введите сумму для снятия.");
            var takeOffMoney = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Введите комментарий.");
            var comment = Console.ReadLine();
            _operator.RemoveFromTotalBalance(takeOffMoney, comment);
            Console.WriteLine($"Ваше текущий баланс: {_operator.TotalMoney}");
        }

        private int ChooseOperation()
        {
            Console.WriteLine("Выберите необходимую вам операцию:");
            Console.WriteLine($"1 - Операции с личным аккаунтом.");
            Console.WriteLine($"2 - Пополнить общий баланс.");
            Console.WriteLine($"3 - Снять с общего баланс.");
            Console.WriteLine($"4 - Проверить общий баланс.");
            Console.WriteLine($"5 - Завершить работу.");
            return Convert.ToInt32(Console.ReadLine());
        }

        private void AddUser(List<User> users)
        {
            Console.WriteLine("Введите имя нового аккаунта");
            var chooseName = ReadAndValidateString();

            Console.WriteLine("Введите начальное количество денег на счету у этого аккаунта.");
            var chooseMoney = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine($"Количество денег: {chooseMoney}, это верно? 1 - Да 2 - Нет.");

            var choose = Convert.ToInt32(Console.ReadLine());
            while (choose != 1)
            {
                Console.WriteLine("Введите количество денег повторно.");
                chooseMoney = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine($"Количество денег: {chooseMoney}, это верно? 1 - Да 2 - Нет.");
                choose = Convert.ToInt32(Console.ReadLine());
            }

            var newUser = new User(chooseName, chooseMoney);
            users.Add(newUser);
        }

        private void DeleteUser(List<User> users)
        {
            Console.WriteLine("Выберите юзера для удаления.");
            for (int i = 0; i < users.Count; i++)
            {
                Console.WriteLine($"{i + 1} - {users[i]}");
            }
            var choose = Convert.ToInt32(Console.ReadLine());
            users.RemoveAt(choose - 1);
            for (int i = 0; i < users.Count; i++)
            {
                Console.WriteLine($"{i + 1} - {users[i]}");
            }
        }
    }
}
