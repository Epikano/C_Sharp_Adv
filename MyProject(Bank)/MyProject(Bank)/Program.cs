using System;
using System.IO;
using BankLibrary;

namespace BankApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.BackgroundColor = ConsoleColor.White;
            Console.Clear();

            Bank<Account> bank = new Bank<Account>("ITEA");

            Console.WriteLine($"\t\t\t\t| Добро пожаловать в банк {bank.Name}! |");

            bool alive = true;
            string path = "";

            while (alive)
            {
                Console.ForegroundColor = ConsoleColor.Black;
                Console.Write("\nПожалуйста, введите путь текстового документа, с которым будет работать приложение: ");
                Console.ForegroundColor = ConsoleColor.Blue;
                path = Console.ReadLine().Trim(new char[] { '\"' });

                FileInfo file = new FileInfo(path);
                if (file.Exists)
                {
                    alive = false;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Такой путь не существует");
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.WriteLine("Если желаете продолжить пользоваться программой без создания текстового документа, нажмите 1.");
                    Console.WriteLine("Другой любой символ даст возможность ввести путь еще раз:");
                    Console.ForegroundColor = ConsoleColor.Blue;
                    string value = Console.ReadLine();
                    if(value == "1")
                    {
                        alive = false;
                    }
                }
            }

            alive = true;
            while (alive)
            {
                Console.ForegroundColor = ConsoleColor.Black;
                Console.WriteLine("-------------------------------------------------------------------------------------------------------------------");
                Console.ForegroundColor = ConsoleColor.DarkGreen;
                Console.WriteLine("1. Открыть счет \t 2. Вывести средства  \t 3. Добавить на счет");
                Console.WriteLine("4. Закрыть счет \t 5. Пропустить день \t 6. Выйти из программы");
                Console.ForegroundColor = ConsoleColor.Black;
                Console.WriteLine("Введите номер пункта:");

                try
                {
                    Console.ForegroundColor = ConsoleColor.Blue;
                    int command = Convert.ToInt32(Console.ReadLine());

                    bool forCalc = true;
                    switch (command)
                    {
                        case 1:
                            OpenAccount(bank);
                            break;
                        case 2:
                            Withdraw(bank);
                            break;
                        case 3:
                            Put(bank);
                            break;
                        case 4:
                            CloseAccount(bank);
                            break;
                        case 5:
                            Console.ForegroundColor = ConsoleColor.DarkMagenta;
                            break;
                        case 6:
                            alive = false;
                            continue;
                        default:
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("| Неизвестная команда |");
                            forCalc = false;
                            break;
                    }
                    if (forCalc)
                        bank.CalculatePercentage();

                    bank.TxtDoc(path);
                    using (AccountContext db = new AccountContext())
                    {
                        db.Database.ExecuteSqlCommand("TRUNCATE TABLE [AccountDBs]");

                        foreach (AccountDB a in bank.accountsDB)
                        {
                            db.Accounts.Add(a);
                        }
                        db.SaveChanges();
                    }
                }
                catch (Exception ex)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(ex.Message);
                }
            }
        }

        private static void OpenAccount(Bank<Account> bank)
        {
            Console.ForegroundColor = ConsoleColor.Black;
            Console.Write("Укажите сумму для создания счета: ");
            Console.ForegroundColor = ConsoleColor.Blue;
            decimal sum = Convert.ToDecimal(Console.ReadLine());

            Console.ForegroundColor = ConsoleColor.Black;
            Console.WriteLine("Выберите тип счета: | 1. До востребования | 2. Депозит |");
            AccountType accountType = AccountType.temp;

            Console.ForegroundColor = ConsoleColor.Blue;
            int type = Convert.ToInt32(Console.ReadLine());
            switch (type)
            {
                case 1:
                    accountType = AccountType.Demand;
                    break;
                case 2:
                    accountType = AccountType.Deposit;
                    break;
                default:
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("| Неизвестная команда |");
                    break;
            }

            Console.ForegroundColor = ConsoleColor.DarkMagenta;
            bank.Open(accountType,
                sum,
                AddSumHandler,
                WithdrawSumHandler,
                (o, e) => Console.WriteLine(e.Message),
                CloseAccountHandler,
                OpenAccountHandler);
        }

        private static void Withdraw(Bank<Account> bank)
        {
            Console.ForegroundColor = ConsoleColor.Black;
            Console.Write("Укажите сумму для вывода со счета: ");
            Console.ForegroundColor = ConsoleColor.Blue;
            decimal sum = Convert.ToDecimal(Console.ReadLine());

            Console.ForegroundColor = ConsoleColor.Black;
            Console.Write("Введите id счета: ");
            Console.ForegroundColor = ConsoleColor.Blue;
            int id = Convert.ToInt32(Console.ReadLine());

            Console.ForegroundColor = ConsoleColor.DarkMagenta;
            bank.Withdraw(sum, id);
        }

        private static void Put(Bank<Account> bank)
        {
            Console.ForegroundColor = ConsoleColor.Black;
            Console.WriteLine("Укажите сумму, чтобы положить на счет:");
            Console.ForegroundColor = ConsoleColor.Blue;
            decimal sum = Convert.ToDecimal(Console.ReadLine());

            Console.ForegroundColor = ConsoleColor.Black;
            Console.WriteLine("Введите Id счета:");
            Console.ForegroundColor = ConsoleColor.Blue;
            int id = Convert.ToInt32(Console.ReadLine());

            Console.ForegroundColor = ConsoleColor.DarkMagenta;
            bank.Put(sum, id);
        }

        private static void CloseAccount(Bank<Account> bank)
        {
            Console.ForegroundColor = ConsoleColor.Black;
            Console.WriteLine("Введите id счета, который надо закрыть:");
            Console.ForegroundColor = ConsoleColor.Blue;
            int id = Convert.ToInt32(Console.ReadLine());

            Console.ForegroundColor = ConsoleColor.DarkMagenta;
            bank.Close(id);
        }

        // Обработчики событий класса Account:
        // обработчик открытия счета
        private static void OpenAccountHandler(object sender, AccountEventArgs e)
        {
            Console.WriteLine(e.Message);
        }
        // обработчик добавления денег на счет
        private static void AddSumHandler(object sender, AccountEventArgs e)
        {
            Console.WriteLine(e.Message);
        }
        // обработчик вывода средств
        private static void WithdrawSumHandler(object sender, AccountEventArgs e)
        {
            Console.WriteLine(e.Message);
        }
        // обработчик закрытия счета
        private static void CloseAccountHandler(object sender, AccountEventArgs e)
        {
            Console.WriteLine(e.Message);
        }
    }
}
