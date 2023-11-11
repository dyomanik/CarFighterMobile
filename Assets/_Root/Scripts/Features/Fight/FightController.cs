using System;
using Profile;
using TMPro;
using Tool;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Features.Fight
{
    internal class FightController : BaseController
    {
        private readonly ResourcePath _resourcePath = new ResourcePath("Prefabs/Fight/FightView");
        private readonly ProfilePlayer _profilePlayer;
        private readonly FightView _view;
        private readonly Enemy _enemy;

        private PlayerData _money;
        private PlayerData _heath;
        private PlayerData _power;
        private PlayerData _criminality;


        public FightController(Transform placeForUi, ProfilePlayer profilePlayer)
        {
            _profilePlayer = profilePlayer;
            _view = LoadView(placeForUi);

            _enemy = new Enemy("Enemy Flappy");

            _money = CreatePlayerData(DataType.Money);
            _heath = CreatePlayerData(DataType.Health);
            _power = CreatePlayerData(DataType.Power);
            _criminality = CreatePlayerData(DataType.Criminality);

            Subscribe(_view);
        }

        protected override void OnDispose()
        {
            DisposePlayerData(ref _money);
            DisposePlayerData(ref _heath);
            DisposePlayerData(ref _power);
            DisposePlayerData(ref _criminality);

            Unsubscribe(_view);
        }


        private FightView LoadView(Transform placeForUi)
        {
            GameObject prefab = ResourcesLoader.LoadPrefab(_resourcePath);
            GameObject objectView = Object.Instantiate(prefab, placeForUi, false);
            AddGameObject(objectView);

            return objectView.GetComponent<FightView>();
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


        private void Subscribe(FightView view)
        {
            view.IncreaseMoneyButton.onClick.AddListener(IncreaseMoney);
            view.DecreaseMoneyButton.onClick.AddListener(DecreaseMoney);

            view.IncreaseHealthButton.onClick.AddListener(IncreaseHealth);
            view.DecreaseHealthButton.onClick.AddListener(DecreaseHealth);

            view.IncreasePowerButton.onClick.AddListener(IncreasePower);
            view.DecreasePowerButton.onClick.AddListener(DecreasePower);

            view.MakeCrimeButton.onClick.AddListener(MakeCrime);

            view.FightButton.onClick.AddListener(Fight);
            view.PeaceButton.onClick.AddListener(Escape);
        }

        private void Unsubscribe(FightView view)
        {
            view.IncreaseMoneyButton.onClick.RemoveAllListeners();
            view.DecreaseMoneyButton.onClick.RemoveAllListeners();

            view.IncreaseHealthButton.onClick.RemoveAllListeners();
            view.DecreaseHealthButton.onClick.RemoveAllListeners();

            view.IncreasePowerButton.onClick.RemoveAllListeners();
            view.DecreasePowerButton.onClick.RemoveAllListeners();

            view.MakeCrimeButton.onClick.RemoveAllListeners();

            view.FightButton.onClick.RemoveAllListeners();
            view.PeaceButton.onClick.RemoveAllListeners();
        }


        private void IncreaseMoney() => IncreaseValue(_money);
        private void DecreaseMoney() => DecreaseValue(_money);

        private void IncreaseHealth() => IncreaseValue(_heath);
        private void DecreaseHealth() => DecreaseValue(_heath);

        private void IncreasePower() => IncreaseValue(_power);
        private void DecreasePower() => DecreaseValue(_power);

        private void MakeCrime() => RandomValue(_view.MinCriminalityValue, _view.MaxCriminalityValue, _criminality);

        private void IncreaseValue(PlayerData playerData) => AddToValue(_view.CharacteristicValueMultiplayer, playerData);
        private void DecreaseValue(PlayerData playerData) => AddToValue(-_view.CharacteristicValueMultiplayer, playerData);

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
            textComponent.text = $"Player {dataType:F} {value}";

            int enemyPower = _enemy.CalcPowerOfHit();
            _view.CountPowerEnemyText.text = $"Enemy Power: {enemyPower}";
        }

        private void SetActivityPeaceButton()
        {
            bool peaceRelation = _criminality.Value < _view.MinPeacefulCrimeValue;
            _view.PeaceButton.gameObject.SetActive(peaceRelation);
        }

        private TMP_Text GetTextComponent(DataType dataType) =>
            dataType switch
            {
                DataType.Money => _view.CountMoneyText,
                DataType.Health => _view.CountHealthText,
                DataType.Power => _view.CountPowerText,
                DataType.Criminality => _view.CountCriminalityText,
                _ => throw new ArgumentException($"Wrong {nameof(DataType)}")
            };

        private void Fight()
        {
            int enemyPower = _enemy.CalcPowerOfHit();
            bool isVictory = _power.Value >= enemyPower;

            string color = isVictory ? "#07FF00" : "#FF0000";
            string message = isVictory ? "Win" : "Lose";

            Debug.Log($"<color={color}>{message}!!!</color>");

            Close();
        }

        private void Escape()
        {
            string color = "#FFB202";
            string message = "Escaped";

            Debug.Log($"<color={color}>{message}!!!</color>");

            Close();
        }

        private void Close() => _profilePlayer.CurrentState.Value = GameState.Game;
    }
}
