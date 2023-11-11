using UnityEngine;

namespace BattleScripts
{
    internal interface IEnemy
    {
        void Update(PlayerData playerData);
    }

    internal class Enemy : IEnemy
    {
        private const float KMoney = 2f;
        private const float KPower = 3f;
        private const float MaxHealthPlayer = 20;

        private readonly string _name;

        private int _moneyPlayer;
        private int _healthPlayer;
        private int _powerPlayer;
        private int _criminalityPlayer;
        private int _maxPeacefulCrimeValue;

        public Enemy(string name)
        {
            _name = name;
        }

        public Enemy(string name, int maxPeacefulCrimeValue)
        {
            _name = name;
            _maxPeacefulCrimeValue = maxPeacefulCrimeValue;
        }

        public void Update(PlayerData playerData)
        {
            switch (playerData.DataType)
            {
                case DataType.Money:
                    _moneyPlayer = playerData.Value;
                    break;

                case DataType.Health:
                    _healthPlayer = playerData.Value;
                    break;

                case DataType.Power:
                    _powerPlayer = playerData.Value;
                    break;

                case DataType.Criminality:
                    _criminalityPlayer = playerData.Value;
                    break;
            }

            Debug.Log($"Notified {_name} change to {playerData.DataType:F}");
        }

        public int CalcPowerOfHit()
        {
            int kHealth = CalcKHealth();
            int KCriminality = CalcKCriminality();
            float moneyRatio = _moneyPlayer / KMoney;
            float powerRatio = _powerPlayer / KPower;
            var powerOfHit = (int)(kHealth + KCriminality + moneyRatio + powerRatio);

            return powerOfHit;
        }

        private int CalcKHealth() =>
            _healthPlayer > MaxHealthPlayer ? 100 : 5;

        private int CalcKCriminality() =>
            CalcPeaceRelation() ? 1 : 2;

        public bool CalcPeaceRelation()
        {
            bool peaceRelation = _criminalityPlayer < _maxPeacefulCrimeValue; 
            return peaceRelation;
        }
    }
}