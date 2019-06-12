namespace BankLibrary
{
    // Интернет посоветовал писать делегат для ивента с параметром объект-инициатор события, ведь он может пригодится(особенно важно в графических программах)
    public delegate void AccountStateHandler(object sender, AccountEventArgs e);

    public class AccountEventArgs
    {
        // Сообщение
        public string Message { get; private set; }
        // Сумма, на которую изменился счет
        public decimal Sum { get; private set; }

        public AccountEventArgs(string _mes, decimal _sum)
        {
            Message = _mes;
            Sum = _sum;
        }
    }
}
