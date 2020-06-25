using System;

namespace BankLibrary
{
    public class Bank<T> where T : Account
    {
        T[] accounts;


        public string Name { get; private set; }


        public Bank(string name)
        {
            this.Name = name;
        }


        public void Open(AccountType accountType, decimal sum, AccountStateHandler addSumHandler, AccountStateHandler withdrawSumHandler, AccountStateHandler calculationHandler, AccountStateHandler closeAccountHandler, AccountStateHandler openAccountHandler)
        {
            T newAccount = null;

            switch (accountType)
            {
                case AccountType.Ordinary:
                    newAccount = new DemandAccound(sum, 1) as T;
                    break;
                case AccountType.Deposit:
                    newAccount = new DepositAccount(sum, 40) as T;
                    break;
            }

            if (newAccount == null)
                throw new Exception("Ошибка создания счета");

            if (accounts == null)
                accounts = new T[] { newAccount };
            else
            {
                T[] temptAccounts = new T[accounts.Length + 1];
                for (int i = 0; i < accounts.Length; i++)
                    temptAccounts[i] = accounts[i];
                temptAccounts[temptAccounts.Length - 1] = newAccount;
                accounts = temptAccounts;
            }

            newAccount.Added += addSumHandler;
            newAccount.Withdrawed += withdrawSumHandler;
            newAccount.Closed += closeAccountHandler;
            newAccount.Opened += openAccountHandler;
            newAccount.Calculated += calculationHandler;

            newAccount.Open();
        }

        public void Put(decimal sum, int id)
        {
            T account = FindAccount(id);

            if (account == null)
                throw new Exception("Счет не найден");

            account.Put(sum);
        }

        public void Withdraw(decimal sum, int id)
        {
            T account = FindAccount(id);

            if (account == null)
                throw new Exception("Счет не найден");

            account.Withdraw(sum);
        }

        public void Close(int id)
        {
            int index;
            T account = FindAccount(id, out index);

            if (account == null)
                throw new Exception("Счет не найден");

            account.Close();

            if (accounts.Length <= 1)
                accounts = null;
            else
            {
                T[] temptAccounts = new T[accounts.Length - 1];
                for (int i = 0, j = 0; i < accounts.Length; i++)
                {
                    if (i != index)
                        temptAccounts[j++] = accounts[i];
                }
                accounts = temptAccounts;
            }
        }

        public void CalculatePercentage()
        {
            if (accounts == null)
                return;

            for (int i = 0; i < accounts.Length; i++)
            {
                T account = accounts[i];
                account.IncrementDays();
                account.Calculate();
            }
        }

        public T FindAccount(int id)
        {
            for (int i = 0; i < accounts.Length; i++)
            {
                if (accounts[i].Id == id)
                    return accounts[i];
            }

            return null;
        }

        public T FindAccount(int id, out int index)
        {
            for (int i = 0; i < accounts.Length; i++)
            {
                if (accounts[i].Id == id)
                {
                    index = i;
                    return accounts[i];
                }
            }

            index = -1;
            return null;
        }
    }


    public enum AccountType
    {
        Ordinary,
        Deposit
    }
}
