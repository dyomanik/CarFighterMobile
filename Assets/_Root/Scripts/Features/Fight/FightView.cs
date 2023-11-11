using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Features.Fight
{
    internal class FightView : MonoBehaviour
    {
        [field: Header("Player Stats")]
        [field: SerializeField] public TMP_Text CountMoneyText { get; private set; }
        [field: SerializeField] public TMP_Text CountHealthText { get; private set; }
        [field: SerializeField] public TMP_Text CountPowerText { get; private set; }
        [field: SerializeField] public TMP_Text CountCriminalityText { get; private set; }
        [field: SerializeField] public int MinCriminalityValue { get; private set; }
        [field: SerializeField] public int MaxCriminalityValue { get; private set; }
        [field: SerializeField] public int MinPeacefulCrimeValue { get; private set; }
        [field: SerializeField] public int CharacteristicValueMultiplayer { get; private set; }


        [field: Header("Enemy Stats")]
        [field: SerializeField] public TMP_Text CountPowerEnemyText{ get; private set; }

        [field: Header("Money Buttons")]
        [field: SerializeField] public Button IncreaseMoneyButton{ get; private set; }
        [field: SerializeField] public Button DecreaseMoneyButton{ get; private set; }

        [field: Header("Health Buttons")]
        [field: SerializeField] public Button IncreaseHealthButton{ get; private set; }
        [field: SerializeField] public Button DecreaseHealthButton{ get; private set; }

        [field: Header("Power Buttons")]
        [field: SerializeField] public Button IncreasePowerButton{ get; private set; }
        [field: SerializeField] public Button DecreasePowerButton{ get; private set; }

        [field: Header("Crime Button")]
        [field: SerializeField] public Button MakeCrimeButton { get; private set; }

        [field: Header("Other Buttons")]
        [field: SerializeField] public Button FightButton{ get; private set; }
        [field: SerializeField] public Button PeaceButton{ get; private set; }
    }
}
