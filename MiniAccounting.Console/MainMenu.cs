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
        public void Start()
        {
            var users = new List<User>();
            users.Add(new User("Dan", 0));
            users.Add(new User("Val", 0));
            users.Add(new User("Sne", 0));
            SayHi();
            RegisterOrDeleteUsers(users);
            StartMenu(users);
        }

        private static void StartMenu(List<User> users)
        {
            var totalMoney = 0;
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
                        TopUpTotalBalance(totalMoney);
                        break;
                    case 3:
                        RemoveFromTotalBalance(totalMoney);
                        break;
                    case 4:
                        Console.WriteLine($"Ваш текущий баланс: {totalMoney}");
                        break;
                    case 5:
                        Console.WriteLine("Пока.");
                        onOff = false;
                        break;

                }
            }
        }

        private static void RegisterOrDeleteUsers(List<User> users)
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

        private static string ReadAndValidateString()
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

        private static void SayHi()
        {
            Console.WriteLine("Вас приведствует программа \"Mini Accounting\"");
            Console.WriteLine("Здесь вы можете вести учет потраченных и заработанных средств.");
        }

        private static void OperationsWithPersonalAccount(List<User> users)
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

        private static void TopUpTotalBalance(int totalMoney)
        {
            Console.WriteLine("Вы выбрали операцию пополнения общего баланса.");
            Console.WriteLine($"Ваш текущий баланс: {totalMoney}");
            Console.WriteLine("Введите сумму для пополнения.");
            var addMoney = Convert.ToInt32(Console.ReadLine());
            totalMoney = totalMoney + addMoney;
            Console.WriteLine($"Ваш текущий баланс: {totalMoney}");
        }

        private static void RemoveFromTotalBalance(int totalMoney)
        {
            Console.WriteLine("Вы выбрали операцию снятия с общего баланса.");
            Console.WriteLine($"Ваше текущий баланс: {totalMoney}");
            Console.WriteLine("Введите сумму для снятия.");
            var takeOffMoney = Convert.ToInt32(Console.ReadLine());
            totalMoney = totalMoney - takeOffMoney;
            Console.WriteLine($"Ваше текущий баланс: {totalMoney}");
        }

        private static int ChooseOperation()
        {
            Console.WriteLine("Выберите необходимую вам операцию:");
            Console.WriteLine($"1 - Операции с личным аккаунтом.");
            Console.WriteLine($"2 - Пополнить общий баланс.");
            Console.WriteLine($"3 - Снять с общего баланс.");
            Console.WriteLine($"4 - Проверить общий баланс.");
            Console.WriteLine($"5 - Завершить работу.");
            return Convert.ToInt32(Console.ReadLine());
        }

        private static void AddUser(List<User> users)
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

        private static void DeleteUser(List<User> users)
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


        //private static double ReadAndValidateInt()
        //{
        //    string str = ReadDouble();
        //    Console.WriteLine($"Вы написали: {str}, это верно? 1 - Да 2 - Нет.");
        //    var choose = Convert.ToInt32(Console.ReadLine());
        //    while (choose != 1)
        //    {
        //        Console.WriteLine("Введите повторно.");
        //        str = Console.ReadLine();
        //        Console.WriteLine($"Вы написали: {str}, это верно? 1 - Да 2 - Нет.");
        //        choose = Convert.ToInt32(Console.ReadLine());
        //    }

        //    return str;
        //}

        //private static double ReadDouble()
        //{
        //    var str = Console.ReadLine();
        //    while (!double.TryParse(str, out var result))
        //    {
        //        Console.WriteLine("Формат значения не верен, попробуйте снова.");
        //        str = Console.ReadLine();
        //    }

        //    return result;
        //}
    }
}
