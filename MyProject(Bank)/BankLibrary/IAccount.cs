namespace BankLibrary
{
    public interface IAccount
    {
        void Put(decimal sum); // Положить деньги на счет
        decimal Withdraw(decimal sum); // Взять со счета
    }
}
