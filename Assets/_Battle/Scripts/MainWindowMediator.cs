using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace BattleScripts
{
    internal class MainWindowMediator : MonoBehaviour
    {
        [Header("Player Stats")]
        [SerializeField] private TMP_Text _countMoneyText;
        [SerializeField] private TMP_Text _countHealthText;
        [SerializeField] private TMP_Text _countPowerText;
        [SerializeField] private TMP_Text _countCriminalityText;
        [SerializeField] private int _minCriminalityValue;
        [SerializeField] private int _maxCriminalityValue;
        [SerializeField] private int _maxPeacefulCrimeValue;
        [SerializeField] private int _characteristicValueMultiplayer;

        [Header("Enemy Stats")]
        [SerializeField] private TMP_Text _countPowerEnemyText;

        [Header("Money Buttons")]
        [SerializeField] private Button _increaseMoneyButton;
        [SerializeField] private Button _decreaseMoneyButton;

        [Header("Health Buttons")]
        [SerializeField] private Button _increaseHealthButton;
        [SerializeField] private Button _decreaseHealthButton;

        [Header("Power Buttons")]
        [SerializeField] private Button _increasePowerButton;
        [SerializeField] private Button _decreasePowerButton;

        [Header("Criminality Button")]
        [SerializeField] private Button _makeCrimeButton;

        [Header("Other Buttons")]
        [SerializeField] private Button _peaceButton;
        [SerializeField] private Button _fightButton;

        private PlayerData _money;
        private PlayerData _heath;
        private PlayerData _power;
        private PlayerData _criminality;

        private Enemy _enemy;

        private void Start()
        {
            _enemy = new Enemy("Enemy Flappy", _maxPeacefulCrimeValue);

            _money = CreatePlayerData(DataType.Money);
            _heath = CreatePlayerData(DataType.Health);
            _power = CreatePlayerData(DataType.Power);
            _criminality = CreatePlayerData(DataType.Criminality, _maxPeacefulCrimeValue);

            Subscribe();
        }

        private void OnDestroy()
        {
            DisposePlayerData(ref _money);
            DisposePlayerData(ref _heath);
            DisposePlayerData(ref _power);
            DisposePlayerData(ref _criminality);

            Unsubscribe();
        }


        private PlayerData CreatePlayerData(DataType dataType)
        {
            PlayerData playerData = new PlayerData(dataType);
            playerData.Attach(_enemy);

            return playerData;
        }

        private PlayerData CreatePlayerData(DataType dataType, int value)
        {
            PlayerData playerData = new PlayerData(dataType);
            playerData.Value = value;
            playerData.Attach(_enemy);

            return playerData;
        }

        private void DisposePlayerData(ref PlayerData playerData)
        {
            playerData.Detach(_enemy);
            playerData = null;
        }


        private void Subscribe()
        {
            _increaseMoneyButton.onClick.AddListener(IncreaseMoney);
            _decreaseMoneyButton.onClick.AddListener(DecreaseMoney);

            _increaseHealthButton.onClick.AddListener(IncreaseHealth);
            _decreaseHealthButton.onClick.AddListener(DecreaseHealth);

            _increasePowerButton.onClick.AddListener(IncreasePower);
            _decreasePowerButton.onClick.AddListener(DecreasePower);

            _makeCrimeButton.onClick.AddListener(MakeCrime);

            _peaceButton.onClick.AddListener(Peace);
            _fightButton.onClick.AddListener(Fight);
        }

        private void Unsubscribe()
        {
            _increaseMoneyButton.onClick.RemoveListener(IncreaseMoney);
            _decreaseMoneyButton.onClick.RemoveListener(DecreaseMoney);

            _increaseHealthButton.onClick.RemoveListener(IncreaseHealth);
            _decreaseHealthButton.onClick.RemoveListener(DecreaseHealth);

            _increasePowerButton.onClick.RemoveListener(IncreasePower);
            _decreasePowerButton.onClick.RemoveListener(DecreasePower);

            _makeCrimeButton.onClick.RemoveListener(MakeCrime);

            _peaceButton.onClick.RemoveListener(Peace);
            _fightButton.onClick.RemoveListener(Fight);
        }


        private void IncreaseMoney() => IncreaseValue(_money);
        private void DecreaseMoney() => DecreaseValue(_money);

        private void IncreaseHealth() => IncreaseValue(_heath);
        private void DecreaseHealth() => DecreaseValue(_heath);

        private void IncreasePower() => IncreaseValue(_power);
        private void DecreasePower() => DecreaseValue(_power);

        private void MakeCrime() => RandomValue(_minCriminalityValue, _maxCriminalityValue, _criminality);

        private void Peace() => Debug.Log($"<color>You passed peacefully!!!</color>");

        private void IncreaseValue(PlayerData playerData) => AddToValue(_characteristicValueMultiplayer, playerData);
        private void DecreaseValue(PlayerData playerData) => AddToValue(-_characteristicValueMultiplayer, playerData);

        private void AddToValue(int addition, PlayerData playerData)
        {
            playerData.Value += addition;
            ChangeDataWindow(playerData);
        }

        private void RandomValue(int minInclusiveValue, int maxInclusiveValue, PlayerData playerData)
        {
            playerData.Value = UnityEngine.Random.Range(minInclusiveValue, maxInclusiveValue);
            ChangeDataWindow(playerData);
            SetActivityPeaceButton();
        }

        private void ChangeDataWindow(PlayerData playerData)
        {
            int value = playerData.Value;
            DataType dataType = playerData.DataType;
            TMP_Text textComponent = GetTextComponent(dataType);
            textComponent.text = $"Player {dataType:F}: {value}";

            int enemyPower = _enemy.CalcPowerOfHit();
            _countPowerEnemyText.text = $"Enemy Power: {enemyPower}";
        }

        private void SetActivityPeaceButton()
        {
            var flagPeaceButton = _enemy.CalcPeaceRelation();
            _peaceButton.gameObject.SetActive(flagPeaceButton);
        }

        private TMP_Text GetTextComponent(DataType dataType) =>
            dataType switch
            {
                DataType.Money => _countMoneyText,
                DataType.Health => _countHealthText,
                DataType.Power => _countPowerText,
                DataType.Criminality => _countCriminalityText,
                _ => throw new ArgumentException($"Wrong {nameof(DataType)}")
            };

        private void Fight()
        {
            int enemyPower = _enemy.CalcPowerOfHit();
            bool isWin = _power.Value >= enemyPower;

            string color = isWin ? "#07FF00" : "#FF0000";
            string message = isWin ? "Win" : "Lose";

            Debug.Log($"<color={color}>{message}!!!</color>");
        }
    }
}