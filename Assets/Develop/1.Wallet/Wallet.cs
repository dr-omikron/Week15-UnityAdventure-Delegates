using System;
using System.Collections.Generic;
using UnityEngine;

namespace Develop._1.Wallet
{
    public class Wallet
    {
        public event Action<CurrencyType, int> CurrencyAdded;
        public event Action<CurrencyType, int> CurrencySpent;

        private readonly Dictionary<CurrencyType, int> _account;

        public Wallet(Dictionary<CurrencyType, int> currencies)
        {
            _account = currencies;
        }

        public void AddCurrencyAmount(CurrencyType currency, int amount)
        {
            if(amount < 0)
            {
                PrintNegativeValueWarning();

                return;
            }

            if(_account.ContainsKey(currency))
            {
                _account[currency] += amount;
                CurrencyAdded?.Invoke(currency, _account[currency]);

                PrintWalletOperation(currency, amount);
                return;
            }

            PrintInvalidKeyWarning(currency);
        }

        public void SpendCurrency(CurrencyType currency, int amount)
        {
            if(amount < 0)
            {
                PrintNegativeValueWarning();

                return;
            }

            if(_account.ContainsKey(currency))
            {
                if(_account[currency] - amount < 0)
                {
                    _account[currency] = 0;
                    CurrencySpent?.Invoke(currency, 0);

                    PrintWalletOperation(currency, amount);
                    return;
                }

                _account[currency] -= amount;
                CurrencySpent?.Invoke(currency, _account[currency]);

                PrintWalletOperation(currency, amount);

                return;
            }

            PrintInvalidKeyWarning(currency);
        }

        private void PrintWalletOperation(CurrencyType currency, int amount) => Debug.Log($"Added {amount} to {currency}. Total amount is {_account[currency]}");
        private void PrintNegativeValueWarning() => Debug.Log("Currency amount cannot be negative");
        private void PrintInvalidKeyWarning(CurrencyType currency) => Debug.Log("Can't find currency " + currency);
    }
}
