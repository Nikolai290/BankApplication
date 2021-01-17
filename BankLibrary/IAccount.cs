namespace BankLibrary
{
    public interface IAccount
    {
        void Put(decimal sum);                  // Положить деньги на счёт
        decimal Withdraw(decimal sum);          // Списать деньги со счёта
    }
}
