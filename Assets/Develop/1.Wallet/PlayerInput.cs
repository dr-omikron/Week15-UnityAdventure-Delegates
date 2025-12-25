using System;
using UnityEngine;

namespace Develop._1.Wallet
{
    public class PlayerInput
    {
        public event Action AddCoinKeyDown;
        public event Action SpendCoinKeyDown;

        public event Action AddDiamondKeyDown;
        public event Action SpendDiamondKeyDown;

        public event Action AddEnergyKeyDown;
        public event Action SpendEnergyKeyDown;

        private const KeyCode AddCoin = KeyCode.E;
        private const KeyCode SpendCoin = KeyCode.Q;

        private const KeyCode AddDiamond = KeyCode.D;
        private const KeyCode SpendDiamond = KeyCode.A;

        private const KeyCode AddEnergy = KeyCode.C;
        private const KeyCode SpendEnergy = KeyCode.Z;

        public void UpdateInput()
        {
            if(Input.GetKeyDown(AddCoin))
                AddCoinKeyDown?.Invoke();

            if(Input.GetKeyDown(SpendCoin))
                SpendCoinKeyDown?.Invoke();

            if(Input.GetKeyDown(AddDiamond))
                AddDiamondKeyDown?.Invoke();

            if(Input.GetKeyDown(SpendDiamond))
                SpendDiamondKeyDown?.Invoke();

            if(Input.GetKeyDown(AddEnergy))
                AddEnergyKeyDown?.Invoke();

            if(Input.GetKeyDown(SpendEnergy))
                SpendEnergyKeyDown?.Invoke();
        }
    }
}
