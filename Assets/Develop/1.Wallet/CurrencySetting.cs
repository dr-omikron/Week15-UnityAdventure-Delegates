using System;
using UnityEngine;

namespace Develop._1.Wallet
{
    [Serializable]
    public struct CurrencySetting
    {
        public Sprite CurrencyIcon;
        public CurrencyType Type;
        public int StartAmount;
        public int AddAmount;
        public int SpendAmount;
    }
}
