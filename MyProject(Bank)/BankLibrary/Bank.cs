﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace BankLibrary
{
    // Тип счета
    public enum AccountType
    {
        Demand,
        Deposit,
        temp // Нейтральное значение на замену null, что бы VS не матюкался(не влияет на программу)
    }

    public class Bank<T> where T : Account
    {
        T[] accounts;

        public string Name { get; private set; }

        public Bank(string name)
        {
            this.Name = name;
        }

        // Создание счета
        public void Open(AccountType accountType, decimal sum,
            AccountStateHandler addSumHandler, 
            AccountStateHandler withdrawSumHandler,
            AccountStateHandler calculationHandler, 
            AccountStateHandler closeAccountHandler,
            AccountStateHandler openAccountHandler)
        {
            T newAccount = null;

            switch (accountType)
            {
                case AccountType.Demand:
                    newAccount = new DemandAccount(sum, 1) as T;
                    break;
                case AccountType.Deposit:
                    newAccount = new DepositAccount(sum, 40) as T;
                    break;
            }
            if (newAccount == null)
                throw new Exception("Ошибка создания счета");

            // Добавляем новый счет в массив счетов      
            if (accounts == null)
                accounts = new T[] { newAccount };
            else
            {
                T[] tempAccounts = new T[accounts.Length + 1];
                for (int i = 0; i < accounts.Length; i++)
                    tempAccounts[i] = accounts[i];
                tempAccounts[tempAccounts.Length - 1] = newAccount;
                accounts = tempAccounts;
            }

            // Установка обработчиков событий счета
            newAccount.Added += addSumHandler;
            newAccount.Withdrawed += withdrawSumHandler;
            newAccount.Closed += closeAccountHandler;
            newAccount.Opened += openAccountHandler;
            newAccount.Calculated += calculationHandler;

            newAccount.Open();
        }

        // Добавление средств на счет
        public void Put(decimal sum, int id)
        {
            T account = FindAccount(id);
            if (account == null)
                throw new Exception("Счет не найден");
            account.Put(sum);
        }
        // Вывод средств
        public void Withdraw(decimal sum, int id)
        {
            T account = FindAccount(id);
            if (account == null)
                throw new Exception("Счет не найден");
            account.Withdraw(sum);
        }

        // Закрытие счета
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
                // Уменьшаем массив счетов, удаляя из него закрытый счет
                T[] tempAccounts = new T[accounts.Length - 1];
                for (int i = 0, j = 0; i < accounts.Length; i++)
                {
                    if (i != index)
                        tempAccounts[j++] = accounts[i];
                }
                accounts = tempAccounts;
            }
        }

        // Начисление процентов по счетам
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

        // Поиск счета по id
        public T FindAccount(int id)
        {
            for (int i = 0; i < accounts.Length; i++)
            {
                if (accounts[i].Id == id)
                    return accounts[i];
            }
            return null;
        }
        // Перегруженная версия поиска счета
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

        // Запись информации о счетах банка в текстовый документ
        public async Task TxtDoc(string path)
        {
            await Task.Run(() =>
            {
                string Path = @"" + path;
                using (FileStream fstream = new FileStream(Path, FileMode.Create))
                {
                    using (StreamWriter sw = new StreamWriter(fstream))
                    {
                        sw.WriteLine($"\t\t\t\t\t\t\t| Информация о счетах банка {Name} |");
                        newAccoundAdd();
                        foreach (Account acc in accounts)
                        {
                            string kind = "";
                            if (acc is DemandAccount)
                            {
                                kind = "Demand";
                            }
                            if (acc is DepositAccount)
                            {
                                kind = "Deposit";
                            }
                            sw.WriteLine("-------------------------------------------------------------------------------------------------------------------");
                            sw.WriteLine($"Id: {acc.Id}");
                            sw.WriteLine($"Вид: {kind}");
                            sw.WriteLine($"Время существования: {acc.Days}");
                            sw.WriteLine($"Текущая сумма: {acc.CurrentSum}");

                            accountAdd(acc.Id, kind, acc.Days, acc.CurrentSum);
                        }
                    }
                }
            });
        }

        // Для базы данных
        public List<AccountDB> accountsDB;

        void newAccoundAdd()
        {
            accountsDB = new List<AccountDB>();
        }
        void accountAdd(int id, string kind, int days, decimal currentSum)
        {
            accountsDB.Add(new AccountDB { Id = id, Kind = kind, Days = days, CurrentSum = currentSum});
        }
    }
}