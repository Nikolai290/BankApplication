namespace BankLibrary
{
    class DepostiAccount : Account
    {
        public DepostiAccount(decimal sum, int percentage) : base(sum, percentage) { }

        public override void Put(decimal sum)
        {
            if (_days > 30)
                base.Put(sum);
            else
                base.OnAdded(new AccountEventArgs($"На счёт можно положить только после 30 дней", 0));
        }

        public override decimal Withdraw(decimal sum)
        {
            if (_days > 30)
                return base.Withdraw(sum);
            else
                base.OnWithdrawed(new AccountEventArgs($"Снять деньги можно не ранее, чем через 30 дней после открытия", 0));
            return 0;
        }

        protected internal override void Calculate()
        {
            if (_days % 30 == 0)
                base.Calculate();
            else 
                OnCalculated(new AccountEventArgs($"Id: {Id} Баланс {Sum}.", 0));

        }

    }
}
