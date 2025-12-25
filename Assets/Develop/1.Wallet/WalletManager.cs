namespace Develop._1.Wallet
{
    public class WalletManager
    {
        private readonly Wallet _wallet;

        public WalletManager(Wallet wallet)
        {
            _wallet = wallet;
        }

        public void AddCoins(int amount) => _wallet.AddCurrencyAmount(CurrencyType.Coin, amount);
        public void SpendCoins(int amount) => _wallet.SpendCurrency(CurrencyType.Coin, amount);

        public void AddDiamonds(int amount) => _wallet.AddCurrencyAmount(CurrencyType.Diamond, amount);
        public void SpendDiamonds(int amount) => _wallet.SpendCurrency(CurrencyType.Diamond, amount);

        public void AddEnergy(int amount) => _wallet.AddCurrencyAmount(CurrencyType.Energy, amount);
        public void SpendEnergy(int amount) => _wallet.SpendCurrency(CurrencyType.Energy, amount);
    }
}
