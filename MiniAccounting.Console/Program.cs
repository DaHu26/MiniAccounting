using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace MiniAccountingConsole
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int wallet = 0;
            var user = new User("None", 0);
            var users = new List<User>();
            Console.WriteLine("1 - Добавить акк 2 - Убрать");
            var choose = Convert.ToInt32(Console.ReadLine());
            switch (choose)
            {
                case 1:
                    Console.WriteLine("Введите имя нового аккаунта");
                    var chooseName = Console.ReadLine();
                    Console.WriteLine($"Ваше имя: {chooseName}, это верно? 1 - Да 2 - Нет.");
                    var choose1 = Convert.ToInt32(Console.ReadLine());

                    while (choose1 != 1)
                    {
                        Console.WriteLine("Введите имя повторно.");
                        chooseName = Console.ReadLine();
                        Console.WriteLine($"Ваше имя: {chooseName}, это верно? 1 - Да 2 - Нет.");
                        choose1 = Convert.ToInt32(Console.ReadLine());
                    }

                    Console.WriteLine("Введите начальное количество денег на счету у этого аккаунта.");
                    var chooseWallet = Convert.ToInt32(Console.ReadLine());
                    Console.WriteLine($"Количество денег: {chooseWallet}, это верно? 1 - Да 2 - Нет.");
                    choose1 = Convert.ToInt32(Console.ReadLine());

                    while (choose1 != 1)
                    {
                        Console.WriteLine("Введите количество денег повторно.");
                        chooseName = Console.ReadLine();
                        Console.WriteLine($"Количество денег: {chooseWallet}, это верно? 1 - Да 2 - Нет.");
                        choose1 = Convert.ToInt32(Console.ReadLine());
                    }

                    var newUser = new User(chooseName, chooseWallet);
                    users.Add(newUser);

                    break;
                case 2:
                    Console.WriteLine("Выберите юзера для удаления.");
                    for (int i = 0; i < users.Count; i++)
                    {
                        Console.WriteLine($"{i + 1} - {users[i]}");
                    }
                    choose1 = Convert.ToInt32(Console.ReadLine());
                    users.RemoveAt(choose1 - 1);
                    for (int i = 0; i < users.Count; i++)
                    {
                        Console.WriteLine($"{i + 1} - {users[i]}");
                    }
                    break;
            }
            bool onOff = true;
            while (onOff)
            {
                Console.WriteLine("Выберите необходимую вам операцию:");
                Console.WriteLine($"1 - Операции с личным аккаунтом.");
                Console.WriteLine($"2 - Пополнить общий баланс.");
                Console.WriteLine($"3 - Снять с общего баланс.");
                Console.WriteLine($"4 - Проверить общий баланс.");
                Console.WriteLine($"5 - Завершить работу.");

                choose = Convert.ToInt32(Console.ReadLine());

                while (choose < 1 || choose > 5)
                {
                    Console.WriteLine("Выбрана неизвестная операция, повторите ввод.");
                    choose = Convert.ToInt32(Console.ReadLine());
                }



                switch (choose)
                {
                    case 1:
                        Console.WriteLine($"Вы выбрали операции с личным аккаунтом.");
                        Console.WriteLine("Выберите аккаунт для взаимодействия.");
                        for (int i = 0; i < users.Count; i++)
                        {
                            var currentUser = users[i];
                            Console.WriteLine($"{i + 1} - {currentUser.Name} {currentUser.Wallet}");
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
                        break;
                    case 2:
                        Console.WriteLine("Вы выбрали операцию пополнения общего баланса.");
                        Console.WriteLine($"Ваш текущий баланс: {wallet}");
                        Console.WriteLine("Введите сумму для пополнения.");
                        var addWallet = Convert.ToInt32(Console.ReadLine());
                        wallet = wallet + addWallet;
                        Console.WriteLine($"Ваш текущий баланс: {wallet}");
                        break;
                    case 3:
                        Console.WriteLine("Вы выбрали операцию снятия с общего баланса.");
                        Console.WriteLine($"Ваше текущий баланс: {wallet}");
                        Console.WriteLine("Введите сумму для снятия.");
                        var takeOffWallet = Convert.ToInt32(Console.ReadLine());
                        wallet = wallet - takeOffWallet;
                        Console.WriteLine($"Ваше текущий баланс: {wallet}");
                        break;
                    case 4:
                        Console.WriteLine($"Ваш текущий баланс: {wallet}");
                        break;
                    case 5:
                        Console.WriteLine("Пока.");
                        onOff = false;
                        break;

                }
            }
        }
    }
}

