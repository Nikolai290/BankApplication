using System;
using BankLibrary;

namespace BankApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            Bank<Account> bank = new Bank<Account>("DreamBank");

            bool alive = true;

            while (alive)
            {
                Console.Clear();
                bank.CalculatePercentage();
                ConsoleColor defaultColor = Console.ForegroundColor;
                Console.ForegroundColor = ConsoleColor.DarkGreen;
                Console.WriteLine($"День: {bank.Day}");
                Console.WriteLine("1. Открыть счёт \t 2. Вывести средства \t 3. Внести средства");
                Console.WriteLine("4. Закрыть счёт \t 5. Пропустить день \t 6. Выйти из программы");
                Console.WriteLine("Введите номер пункта: ");
                Console.ForegroundColor = defaultColor;
                try
                {
                    int command = Convert.ToInt32(Console.ReadLine());

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
                            Close(bank);
                            break;
                        case 5:

                            break;
                        case 6:
                            alive = false;
                            continue;

                    }

                }
                catch (Exception ex)
                {
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine(ex.Message);
                        Console.ForegroundColor = defaultColor;
                    }
                }



            }

        }
        private static void OpenAccount(Bank<Account> bank)
        {
            Console.Write("Укажите сумму для создания депозита: ");
            decimal sum = Convert.ToDecimal(Console.ReadLine());

            Console.WriteLine("Укажите тип счёта: \t 1. До востребования \t 2. Депозит");
            int type = Convert.ToInt32(Console.ReadLine());
            AccountType accountType;

            if (type == 2)
                accountType = AccountType.Deposit;
            else
                accountType = AccountType.Ordinary;

            bank.Open(accountType, sum,
                AddSumHandler,
                WithdrawSumHandler,
                OpenAccountHandler,
                CloseAccountHandler,
                (o, e) => Console.WriteLine(e.Message));


        }



        private static void Withdraw(Bank<Account> bank)
        {
            Console.Write("Укажите сумму которую хотите внести: ");
            decimal sum = Convert.ToDecimal(Console.ReadLine());
            Console.Write("Укажите Id счёта: ");
            int id = Convert.ToInt32(Console.ReadLine());

            bank.Withdraw(sum, id);

        }
        private static void Put(Bank<Account> bank)
        {
            Console.Write("Укажите сумму которую хотите снять: ");
            decimal sum = Convert.ToDecimal(Console.ReadLine());
            Console.Write("Укажите Id счёта: ");
            int id = Convert.ToInt32(Console.ReadLine());

            bank.Put(sum, id);
        }

        private static void Close(Bank<Account> bank)
        {
            Console.Write("Укажите Id счёта, который надо закрыть: ");
            int id = Convert.ToInt32(Console.ReadLine());

            bank.Close(id);
        }


        // Обработчики событий класса Account
      
        private static void AddSumHandler(object sender, AccountEventArgs e)
        {
            Console.WriteLine(e.Message);
        }

        private static void WithdrawSumHandler(object sender, AccountEventArgs e)
        {
            Console.WriteLine(e.Message);
            if (e.Sum == 0)
                Console.WriteLine("Идём тратить деньги");
        }

        private static void OpenAccountHandler(object sender, AccountEventArgs e)
        {
            Console.WriteLine(e.Message);
        }

        private static void CloseAccountHandler(object sender, AccountEventArgs e)
        {
            Console.WriteLine(e.Message);
        }



    }
}
