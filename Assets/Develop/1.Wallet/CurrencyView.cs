using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Develop._1.Wallet
{
    public class CurrencyView : MonoBehaviour
    {
        public Action<CurrencyType, int> OnAddButtonClicked;
        public Action<CurrencyType, int> OnSpendButtonClicked;

        [SerializeField] private Image _currencyIcon;
        [SerializeField] private TMP_Text _currencyText;
        [SerializeField] private Button _spendButton;
        [SerializeField] private Button _addButton;

        private CurrencyType _currencyType;
        private int _addAmount;
        private int _spendAmount;

        public CurrencyType CurrencyType => _currencyType;

        public void Initialize(CurrencyType currencyType, Sprite currencyIcon, int addAmount, int spendAmount)
        {
            _currencyType = currencyType;
            _addAmount = addAmount;
            _spendAmount = spendAmount;
            _currencyIcon.sprite = currencyIcon;

            _addButton.onClick.AddListener(OnAddCurrencyButtonClick);
            _spendButton.onClick.AddListener(OnSpendCurrencyButtonClick);
        }

        private void OnDestroy()
        {
            _addButton.onClick.RemoveListener(OnAddCurrencyButtonClick);
            _spendButton.onClick.RemoveListener(OnSpendCurrencyButtonClick);
        }

        public void SetCurrencyText(string text) => _currencyText.text = text;
        private void OnAddCurrencyButtonClick() => OnAddButtonClicked?.Invoke(_currencyType, _addAmount);
        private void OnSpendCurrencyButtonClick() => OnSpendButtonClicked?.Invoke(_currencyType, _spendAmount);
    }
}
