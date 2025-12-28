using System.Collections.Generic;
using UnityEngine;

namespace Develop._1.Wallet
{
    public class WalletView : MonoBehaviour
    {
        [SerializeField] private RectTransform _currencyContainerTransform;

        private Wallet _wallet;
        private List<CurrencyView> _currencies;

        public void Initialize(Wallet wallet, List<CurrencyView> currencies) 
        {
            _wallet = wallet;
            _currencies = currencies;

            _wallet.CurrencyAdded += OnCurrencyChanged;
            _wallet.CurrencySpent += OnCurrencyChanged;

            foreach (CurrencyView currency in _currencies)
            {
                currency.transform.SetParent(_currencyContainerTransform, false);
                currency.OnAddButtonClicked += OnAddCurrencyButtonClick;
                currency.OnSpendButtonClicked += OnSpendCurrencyButtonClick;
            }
        }

        private void OnDestroy()
        {
            _wallet.CurrencyAdded -= OnCurrencyChanged;
            _wallet.CurrencySpent -= OnCurrencyChanged;

            foreach (CurrencyView currency in _currencies)
            {
                currency.OnAddButtonClicked -= OnAddCurrencyButtonClick;
                currency.OnSpendButtonClicked -= OnSpendCurrencyButtonClick;
            }
        }

        private void OnAddCurrencyButtonClick(CurrencyType currencyType, int amount) => _wallet.AddCurrency(currencyType, amount);
        private void OnSpendCurrencyButtonClick(CurrencyType currencyType, int amount) => _wallet.SpendCurrency(currencyType, amount);

        private void OnCurrencyChanged(CurrencyType currencyType, int amount)
        {
            foreach (CurrencyView currency in _currencies)
            {
                if(currency.CurrencyType ==  currencyType)
                    currency.SetCurrencyText(amount.ToString());
            }
        }

    }
}
