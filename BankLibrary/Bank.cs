using System;

namespace BankLibrary
{
    public enum AccountType
    {
        Ordinary,
        Deposit
    }
    public class Bank<T> where T : Account
    {
        private T[] _accounts;

        public int Day { get; private set; } = 0;
        public string Name { get; private set; }

        public Bank(string name) => Name = name;

        public void Open(AccountType accountType, decimal sum,
            AccountStateHandler addSumHandler,
            AccountStateHandler withdrawSumHandler,
            AccountStateHandler openAccountHandler,
            AccountStateHandler closeAccountHandler,
            AccountStateHandler calculateHandler)
        {
            T newAccount = null;

            // Создание нового экземпляра объекта Account
            switch (accountType)
            {
                case AccountType.Ordinary:
                    newAccount = new DemandAccount(sum, 1) as T;
                    break;
                case AccountType.Deposit:
                    newAccount = new DepostiAccount(sum, 40) as T;
                    break;
            }

            if (newAccount == null)
                throw new Exception("Ошибка создания счёта");

            // Добавление нового аккауна в массив
            if (_accounts == null)
                _accounts = new T[] { newAccount };
            else
            {
                T[] tempAccounts = new T[_accounts.Length + 1];
                for (int i = 0; i < _accounts.Length; i++)
                    tempAccounts[i] = _accounts[i];
                tempAccounts[tempAccounts.Length - 1] = newAccount;
                _accounts = tempAccounts;
            }

            // Установка обработчиков событий
            newAccount.Added += addSumHandler;
            newAccount.Withdrowed += withdrawSumHandler;
            newAccount.Opened += openAccountHandler;
            newAccount.Closed += closeAccountHandler;
            newAccount.Calculated += calculateHandler;

            newAccount.Open();
        }

        // Добавление средств на счёт
        public void Put (decimal sum, int id)
        {
            T account = FindAccount(id);
            if (account == null)
                throw new Exception("Ошибка! Счёт не найден!");
            account.Put(sum);
        }

        public void Withdraw(decimal sum, int id)
        {
            T account = FindAccount(id);
            if (account == null)
                throw new Exception("Ошибка! Счёт не найден!");
            account.Withdraw(sum);
        }

        public void Close (int id)
        {
            int index;
            T account = FindAccount(id, out index);

            if(account == null)
                throw new Exception("Ошибка! Счёт не найден!");

            account.Close();

            if (_accounts.Length <= 1)
                _accounts = null;
            else
            {
                T[] tempAccounts = new T[_accounts.Length - 1];
                for (int i = 0, j = 0; i < _accounts.Length; i++)
                {
                    if (i != index)
                        tempAccounts[j++] = _accounts[i];
                }
                _accounts = tempAccounts;
            }


        }

        public void CalculatePercentage()
        {
            if (_accounts == null)
                return;
            else
            {
                Day++;
                for(int i = 0; i < _accounts.Length; i++)
                {
                    _accounts[i].IncrementDays();
                    _accounts[i].Calculate();
                }
            }

        }

        public T FindAccount (int id)
        {
            for (int  i = 0; i < _accounts.Length; i++)
            {
                if (_accounts[i].Id == id)
                    return _accounts[i];
            }

            return null;
        }

        public T FindAccount (int id, out int index)
        {
            for (int i = 0; i < _accounts.Length; i++)
            {
                if (_accounts[i].Id == id)
                {
                    index = i;
                    return _accounts[i];
                }
            }
            index = -1;
            return null;
        }





    }
}
