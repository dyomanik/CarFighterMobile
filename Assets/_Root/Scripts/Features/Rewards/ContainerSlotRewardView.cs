using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Features.Rewards
{
    internal class ContainerSlotRewardView : MonoBehaviour
    {
        [SerializeField] private Image _originalBackground;
        [SerializeField] private Image _selectBackground;
        [SerializeField] private Image _iconCurrency;
        [SerializeField] private TMP_Text _textTime;
        [SerializeField] private TMP_Text _countReward;


        public void SetData(Reward reward, int countTime, bool isSelected)
        {
            _iconCurrency.sprite = reward.IconCurrency;
            string periodicityReward = string.Empty;

            switch (reward.PeriodicityRewardType)
            {
                case PeriodicityRewardType.Day:
                    periodicityReward = PeriodicityRewardType.Day.ToString();
                    break;
                case PeriodicityRewardType.Week:
                    periodicityReward = PeriodicityRewardType.Week.ToString();
                    break;
            }

            _textTime.text = $"{periodicityReward} {countTime}";
            _countReward.text = reward.CountCurrency.ToString();
            UpdateBackground(isSelected);
        }

        private void UpdateBackground(bool isSelect)
        {
            _originalBackground.gameObject.SetActive(!isSelect);
            _selectBackground.gameObject.SetActive(isSelect);
        }
    }
}
