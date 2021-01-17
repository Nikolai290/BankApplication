
namespace BankLibrary
{
    class DemandAccount : Account
    {
        public DemandAccount(decimal sum, int percentage) : base(sum, percentage) { }

        protected internal override void Open()
        {
            OnOpened(new AccountEventArgs($"Открыт новый счёт до востребования. Id: {Id}", Sum));
        }
    }
}
