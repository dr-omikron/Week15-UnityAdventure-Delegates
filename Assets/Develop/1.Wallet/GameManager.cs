using System.Collections.Generic;
using UnityEngine;

namespace Develop._1.Wallet
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private WalletView _walletViewPrefab;
        [SerializeField] private RectTransform _canvasTransform;
        [SerializeField] private CurrencyView _currencyViewPrefab;

        [SerializeField] private List<CurrencySetting> _currencySettings;

        private Wallet _wallet;
        private WalletView _walletView;
        private PlayerInput _playerInput;

        private Queue<CurrencySetting> _currencySettingsQueue;
        private CurrencySetting _currentCurrency;

        private void Awake()
        {
            Dictionary<CurrencyType, int> currencies = new Dictionary<CurrencyType, int>();

            foreach (CurrencySetting currencySetting in _currencySettings)
                currencies.Add(currencySetting.Type, currencySetting.StartAmount);

            _wallet = new Wallet(currencies);

            if(_walletViewPrefab != null)
            {
                CreateWalletView();
            }

            _currencySettingsQueue = new Queue<CurrencySetting>(_currencySettings);
            _currentCurrency = SwitchCurrency();

            _playerInput = new PlayerInput();

            _playerInput.AddCurrencyKeyDown += OnAddCurrencyKeyDown;
            _playerInput.SpendCurrencyKeyDown+= OnSpendCurrencyKeyDown;
            _playerInput.SwitchCurrencyKeyDown += OnSwitchCurrencyKeyDown;
        }

        private void Update()
        {
            _playerInput.UpdateInput();
        }

        private void OnDestroy()
        {
            _playerInput.AddCurrencyKeyDown -= OnAddCurrencyKeyDown;
            _playerInput.SpendCurrencyKeyDown -= OnSpendCurrencyKeyDown;
            _playerInput.SwitchCurrencyKeyDown -= OnSwitchCurrencyKeyDown;
        }

        private void CreateWalletView()
        {
            _walletView = Instantiate(_walletViewPrefab, _canvasTransform);
            List<CurrencyView> currencyViews  = new List<CurrencyView>();

            foreach (CurrencySetting currencySetting in _currencySettings)
            {
                CurrencyView currencyView = Instantiate(_currencyViewPrefab);

                currencyView.Initialize(currencySetting.Type, currencySetting.CurrencyIcon, currencySetting.AddAmount, currencySetting.SpendAmount);
                currencyViews.Add(currencyView);
            }

            _walletView.Initialize(_wallet, currencyViews);
        }

        private CurrencySetting SwitchCurrency()
        {
            CurrencySetting setting = _currencySettingsQueue.Dequeue();
            _currencySettingsQueue.Enqueue(setting);
            return setting;
        }

        private void OnSwitchCurrencyKeyDown() => _currentCurrency = SwitchCurrency();
        private void OnAddCurrencyKeyDown() => _wallet.AddCurrency(_currentCurrency.Type, _currentCurrency.AddAmount);
        private void OnSpendCurrencyKeyDown() => _wallet.TrySpendCurrency(_currentCurrency.Type, _currentCurrency.SpendAmount);

    }
}
