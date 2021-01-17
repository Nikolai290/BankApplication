namespace BankLibrary
{
    public abstract class Account : IAccount
    {
        protected internal event AccountStateHandler Withdrowed;
        protected internal event AccountStateHandler Added;
        protected internal event AccountStateHandler Opened;
        protected internal event AccountStateHandler Closed;
        protected internal event AccountStateHandler Calculated;

        static int counter = 0;
        protected int _days = 0;
        public decimal Sum { get; private set; }
        public int Percentage { get; private set; }
        public int Id { get; private set; }

        public Account(decimal sum, int percentage)
        {
            Sum = sum;
            Percentage = percentage;
            Id = ++counter;
        }

        private void CallEvent (AccountEventArgs e, AccountStateHandler handler)
        {
            if (e != null)
                handler?.Invoke(this, e);
        }

        protected virtual void OnAdded (AccountEventArgs e)
        {
            CallEvent(e, Added);
        }
        protected virtual void OnWithdrawed (AccountEventArgs e)
        {
            CallEvent(e, Withdrowed);
        }
        protected virtual void OnOpened (AccountEventArgs e)
        {
            CallEvent(e, Opened);
        }
        protected virtual void OnClosed (AccountEventArgs e)
        {
            CallEvent(e, Closed);
        }
        protected virtual void OnCalculated (AccountEventArgs e)
        {
            CallEvent(e, Calculated);
        }

        public virtual void Put (decimal sum)
        {
            Sum += sum;
            OnAdded(new AccountEventArgs($"На счёт поступило {sum}.", sum));
        }

        public virtual decimal Withdraw (decimal sum)
        {
            decimal result = 0;

            if ( Sum >= sum )
            {
                Sum -= sum;
                result = sum;
                OnWithdrawed(new AccountEventArgs($"Сумма {sum} снята со счёта {Id}.", sum));
            }
            else
            {
                OnWithdrawed(new AccountEventArgs($"Недостаточно средств на счёте {Id}.", 0));
            }
            return result;
        }

        protected internal virtual void Open()
        {
            OnOpened(new AccountEventArgs($"Открыт новый счёт, Id:{Id}.", Sum));
        }

        protected internal virtual void Close()
        {
            OnClosed(new AccountEventArgs($"Счёт Id: {Id} закрыт. Сумма: {Sum}.", Sum));
        }

        protected internal virtual void IncrementDays()
        {
            _days++;
        }

        protected internal virtual void Calculate()
        {
            decimal inkrement = Sum * Percentage / 100;
            Sum += inkrement;
            OnCalculated(new AccountEventArgs($"Id: {Id} Начислены проценты в размере {inkrement}. Баланс {Sum}.",inkrement));
        }



    }
}
