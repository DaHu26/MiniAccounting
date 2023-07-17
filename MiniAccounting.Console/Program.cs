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
            var wallet = 0;
            int pocket1 = 0;
            int pocket2 = 0;
            Console.WriteLine("Введите имя первого пользователя.");
            var userName1 = Console.ReadLine();
            Console.WriteLine($"Ваше имя: {userName1}, это верно? 1 - Да 2 - Нет.");
            var choose = Convert.ToInt32(Console.ReadLine());

            while (choose == 2)
            {
                Console.WriteLine("Введите имя повторно.");
                userName1 = Console.ReadLine();
                Console.WriteLine($"Ваше имя: {userName1}, это верно? 1 - Да 2 - Нет.");
                choose = Convert.ToInt32(Console.ReadLine());
            }

            Console.WriteLine("Введите имя второго пользователя.");
            var userName2 = Console.ReadLine();
            Console.WriteLine($"Ваше имя: {userName2}, это верно? 1 - Да 2 - Нет.");
            choose = Convert.ToInt32(Console.ReadLine());

            while (choose == 2)
            {
                Console.WriteLine("Введите имя повторно.");
                userName2 = Console.ReadLine();
                Console.WriteLine($"Ваше имя: {userName2}, это верно? 1 - Да 2 - Нет.");
                choose = Convert.ToInt32(Console.ReadLine());
            }

            bool onOff = true;
            while (onOff)
            {
                Console.WriteLine("Выберите необходимую вам операцию:");
                Console.WriteLine($"1 - Операции {userName1}.");
                Console.WriteLine($"2 - Операции {userName2}.");
                Console.WriteLine($"3 - Пополнить общий баланс.");
                Console.WriteLine($"4 - Снять с общего баланс.");
                Console.WriteLine($"5 - Проверить общий баланс.");
                Console.WriteLine($"6 - Завершить работу.");

                choose = Convert.ToInt32(Console.ReadLine());

                while (choose < 1 || choose > 6)
                {
                    Console.WriteLine("Выбрана неизвестная операция, повторите ввод.");
                    choose = Convert.ToInt32(Console.ReadLine());
                }



                switch (choose)
                {
                    case 1:
                        Console.WriteLine($"Вы выбрали операции {userName1}.");
                        Console.WriteLine($"Ваш баланс - {pocket1}");
                        Console.WriteLine("Вы желаете 1 - Пополнить баланс 2 - Снять со счета.");
                        var choose2 = Convert.ToInt32(Console.ReadLine());
                        switch (choose2)
                        {
                            case 1:
                                Console.WriteLine("Введите сумму для пополнения.");
                                var addPocket1 = Convert.ToInt32(Console.ReadLine());
                                pocket1 = addPocket1 + pocket1;
                                Console.WriteLine($"Ваш текущий баланс: {pocket1}");
                                break;
                            case 2:
                                Console.WriteLine("Введите сумму для снятия.");
                                var takeOffPocket1 = Convert.ToInt32(Console.ReadLine());
                                var percentPayToWebsite1 = takeOffPocket1 / 100 * 3;

                                while (pocket1 < takeOffPocket1 + percentPayToWebsite1)
                                {
                                    Console.WriteLine("Недостаточно средств для выполения действия.");
                                    Console.WriteLine($"Ваш баланс: {pocket1}");
                                    Console.WriteLine("Выберите сумму для снятия.");
                                    takeOffPocket1 = Convert.ToInt32(Console.ReadLine());
                                    percentPayToWebsite1 = takeOffPocket1 / 100 * 3;
                                }

                                pocket1 = pocket1 - (takeOffPocket1 + percentPayToWebsite1);
                                Console.WriteLine($"Ваш текущий баланс: {pocket1}");
                                break;
                        }
                        break;

                    case 2:
                        Console.WriteLine($"Вы выбрали операции {userName2}.");
                        Console.WriteLine($"Ваш баланс - {pocket2}");
                        Console.WriteLine("Вы желаете 1 - Пополнить баланс 2 - Снять со счета.");
                        choose2 = Convert.ToInt32(Console.ReadLine());
                        switch (choose2)
                        {
                            case 1:
                                Console.WriteLine("Введите сумму для пополнения.");
                                var addPocket2 = Convert.ToInt32(Console.ReadLine());
                                pocket2 = addPocket2 + pocket2;
                                Console.WriteLine($"Ваш текущий баланс: {pocket2}");
                                break;
                            case 2:
                                Console.WriteLine("Введите сумму для снятия.");
                                var takeOffPocket2 = Convert.ToInt32(Console.ReadLine());
                                var percentPayToWebsite2 = takeOffPocket2 / 100 * 3;

                                while (pocket2 < takeOffPocket2 + percentPayToWebsite2)
                                {
                                    Console.WriteLine("Недостаточно средств для выполения действия.");
                                    Console.WriteLine($"Ваш баланс: {pocket2}");
                                    Console.WriteLine("Выберите сумму для снятия.");
                                    takeOffPocket2 = Convert.ToInt32(Console.ReadLine());
                                    percentPayToWebsite2 = takeOffPocket2 / 100 * 3;
                                }

                                pocket2 = pocket2 - (takeOffPocket2 + percentPayToWebsite2);
                                Console.WriteLine($"Ваш текущий баланс: {pocket2}");
                                break;
                        }
                        break;
                    case 3:
                        Console.WriteLine("Вы выбрали операцию пополнения общего баланса.");
                        Console.WriteLine($"Ваш текущий баланс: {wallet}");
                        Console.WriteLine("Введите сумму для пополнения.");
                        var addWallet = Convert.ToInt32(Console.ReadLine());
                        wallet = wallet + addWallet;
                        Console.WriteLine($"Ваш текущий баланс: {wallet}");
                        break;
                    case 4:
                        Console.WriteLine("Вы выбрали операцию снятия с общего баланса.");
                        Console.WriteLine($"Ваше текущий баланс: {wallet}");
                        Console.WriteLine("Введите сумму для снятия.");
                        var takeOffWallet = Convert.ToInt32(Console.ReadLine());
                        wallet = wallet - takeOffWallet;
                        Console.WriteLine($"Ваше текущий баланс: {wallet}");
                        break;
                    case 5:
                        Console.WriteLine($"Ваш текущий баланс: {wallet}");
                        break;
                    case 6:
                        Console.WriteLine("Пока.");
                        onOff = false;
                        break;

                }
            }
        }
    }
}
