using System;
using UnityEngine;

namespace Develop._1.Wallet
{
    public class PlayerInput
    {
        public event Action AddCurrencyKeyDown;
        public event Action SpendCurrencyKeyDown;
        public event Action SwitchCurrencyKeyDown;

        private const KeyCode AddCurrencyKey = KeyCode.D;
        private const KeyCode SpendCurrencyKey = KeyCode.A;
        private const KeyCode SwitchCurrencyKey = KeyCode.S;

        public void UpdateInput()
        {
            if(Input.GetKeyDown(AddCurrencyKey))
                AddCurrencyKeyDown?.Invoke();

            if(Input.GetKeyDown(SpendCurrencyKey))
                SpendCurrencyKeyDown?.Invoke();

            if(Input.GetKeyDown(SwitchCurrencyKey))
                SwitchCurrencyKeyDown?.Invoke();
        }
    }
}
