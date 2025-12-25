using System;
using System.Collections.Generic;
using UnityEngine;

namespace Develop._1.Wallet
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private WalletView _walletViewPrefab;
        [SerializeField] private RectTransform _canvasTransform;

        [SerializeField] private int _addCurrencyAmount;
        [SerializeField] private int _spendCurrencyAmount;

        private WalletManager _walletManager;
        private WalletView _walletView;
        private PlayerInput _playerInput;

        private void Awake()
        {
            Dictionary<CurrencyType, int> currencies = new Dictionary<CurrencyType, int>();
            int currenciesCount = Enum.GetValues(typeof(CurrencyType)).Length;

            for (int i = 0; i < currenciesCount; i++)
                currencies.Add((CurrencyType)i, 0);

            Wallet wallet = new Wallet(currencies);
            _walletManager = new WalletManager(wallet);

            if(_walletViewPrefab != null)
            {
                _walletView = Instantiate(_walletViewPrefab, _canvasTransform);
                _walletView.Initialize(wallet, _walletManager, _addCurrencyAmount, _spendCurrencyAmount);
            }

            _playerInput = new PlayerInput();

            _playerInput.AddCoinKeyDown += OnAddCoinKeyDown;
            _playerInput.SpendCoinKeyDown += OnSpendCoinKeyDown;
            _playerInput.AddDiamondKeyDown += OnAddDiamondKeyDown;
            _playerInput.SpendDiamondKeyDown += OnSpendDiamondKeyDown;
            _playerInput.AddEnergyKeyDown += OnAddEnergyKeyDown;
            _playerInput.SpendEnergyKeyDown += OnSpendEnergyKeyDown;
        }

        private void Update()
        {
            _playerInput.UpdateInput();
        }

        private void OnDestroy()
        {
            _playerInput.AddCoinKeyDown -= OnAddCoinKeyDown;
            _playerInput.SpendCoinKeyDown -= OnSpendCoinKeyDown;
            _playerInput.AddDiamondKeyDown -= OnAddDiamondKeyDown;
            _playerInput.SpendDiamondKeyDown -= OnSpendDiamondKeyDown;
            _playerInput.AddEnergyKeyDown -= OnAddEnergyKeyDown;
            _playerInput.SpendEnergyKeyDown -= OnSpendEnergyKeyDown;
        }

        private void OnAddCoinKeyDown() => _walletManager.AddCoins(_addCurrencyAmount);
        private void OnSpendCoinKeyDown() => _walletManager.SpendCoins(_spendCurrencyAmount);

        private void OnAddDiamondKeyDown() => _walletManager.AddDiamonds(_addCurrencyAmount);
        private void OnSpendDiamondKeyDown() => _walletManager.SpendDiamonds(_spendCurrencyAmount);

        private void OnAddEnergyKeyDown() => _walletManager.AddEnergy(_addCurrencyAmount);
        private void OnSpendEnergyKeyDown() => _walletManager.SpendEnergy(_spendCurrencyAmount);
    }
}
