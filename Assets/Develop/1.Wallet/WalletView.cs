using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Develop._1.Wallet
{
    public class WalletView : MonoBehaviour
    {
        [SerializeField] private TMP_Text _coinsCountText;
        [SerializeField] private TMP_Text _diamondsCountText;
        [SerializeField] private TMP_Text _energyCountText;

        [SerializeField] private Button _addCoinButton;
        [SerializeField] private Button _spendCoinButton;

        [SerializeField] private Button _addDiamondButton;
        [SerializeField] private Button _spendDiamondButton;

        [SerializeField] private Button _addEnergyButton;
        [SerializeField] private Button _spendEnergyButton;

        private int _addCurrencyAmount;
        private int _spendCurrencyAmount;

        private Wallet _wallet;
        private WalletManager _walletManager;

        public void Initialize(Wallet wallet, WalletManager walletManager, int addCurrencyAmount, int spendCurrencyAmount)
        {
            _wallet = wallet;
            _walletManager = walletManager;

            _addCurrencyAmount = addCurrencyAmount;
            _spendCurrencyAmount = spendCurrencyAmount;

            _wallet.CurrencyAdded += OnCurrencyChanged;
            _wallet.CurrencySpent += OnCurrencyChanged;

            _addCoinButton.onClick.AddListener(OnAddCoinButtonClick);
            _spendCoinButton.onClick.AddListener(OnSpendCoinButtonClick);

            _addDiamondButton.onClick.AddListener(OnAddDiamondButtonClick);
            _spendDiamondButton.onClick.AddListener(OnSpendDiamondButtonClick);

            _addEnergyButton.onClick.AddListener(OnAddEnergyButtonClick);
            _spendEnergyButton.onClick.AddListener(OnSpendEnergyButtonClick);
        }

        private void OnDestroy()
        {
            _wallet.CurrencyAdded -= OnCurrencyChanged;
            _wallet.CurrencySpent -= OnCurrencyChanged;

            _addCoinButton.onClick.RemoveListener(OnAddCoinButtonClick);
            _spendCoinButton.onClick.RemoveListener(OnSpendCoinButtonClick);

            _addDiamondButton.onClick.RemoveListener(OnAddDiamondButtonClick);
            _spendDiamondButton.onClick.RemoveListener(OnSpendDiamondButtonClick);

            _addEnergyButton.onClick.RemoveListener(OnAddEnergyButtonClick);
            _spendEnergyButton.onClick.RemoveListener(OnSpendEnergyButtonClick);
        }

        private void OnAddCoinButtonClick() => _walletManager.AddCoins(_addCurrencyAmount);
        private void OnSpendCoinButtonClick() => _walletManager.SpendCoins(_spendCurrencyAmount);

        private void OnAddDiamondButtonClick() => _walletManager.AddDiamonds(_addCurrencyAmount);
        private void OnSpendDiamondButtonClick() => _walletManager.SpendDiamonds(_spendCurrencyAmount);

        private void OnAddEnergyButtonClick() => _walletManager.AddEnergy(_addCurrencyAmount);
        private void OnSpendEnergyButtonClick() => _walletManager.SpendEnergy(_spendCurrencyAmount);

        private void OnCurrencyChanged(CurrencyType currencyType, int amount)
        {
            switch (currencyType)
            {
                case CurrencyType.Coin:
                    _coinsCountText.text = amount.ToString();
                    break;

                case CurrencyType.Diamond:
                    _diamondsCountText.text = amount.ToString();
                    break;

                case CurrencyType.Energy:
                    _energyCountText.text = amount.ToString();
                    break;

                default:
                    throw new ArgumentOutOfRangeException(nameof(currencyType), currencyType, null);
            }
        }

    }
}
