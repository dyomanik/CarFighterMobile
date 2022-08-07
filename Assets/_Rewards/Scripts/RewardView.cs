using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Rewards
{
    internal abstract class RewardView : MonoBehaviour
    {
        private const string CurrentSlotInActiveKey = nameof(CurrentSlotInActiveKey);
        private const string TimeGetRewardKey = nameof(TimeGetRewardKey);

        [field: Header("Settings Time Get Reward")]
        [field: SerializeField] public float TimeCooldown { get; protected set; }
        [field: SerializeField] public float TimeDeadline { get; protected set; }

        [field: Header("Settings Rewards")]
        [field: SerializeField] public List<RewardConfig> RewardConfigs { get; protected set; }

        [field: Header("Ui Elements")]
        [field: SerializeField] public TMP_Text TimerNewReward { get; protected set; }
        [field: SerializeField] public Transform MountRootSlotsReward { get; protected set; }
        [field: SerializeField] public ContainerSlotRewardView ContainerSlotRewardPrefab { get; protected set; }
        [field: SerializeField] public Button GetRewardButton { get; protected set; }
        [field: SerializeField] public Button ResetButton { get; protected set; }

        public int CurrentSlotInActive
        {
            get => PlayerPrefs.GetInt(SetCurrentSlotInActiveKey());
            set => PlayerPrefs.SetInt(SetCurrentSlotInActiveKey(), value);
        }

        public DateTime? TimeGetReward
        {
            get
            {
                string data = PlayerPrefs.GetString(SetTimeGetRewardKey());
                return !string.IsNullOrEmpty(data) ? DateTime.Parse(data) : null;
            }
            set
            {
                if (value != null)
                    PlayerPrefs.SetString(SetTimeGetRewardKey(), value.ToString());
                else
                    PlayerPrefs.DeleteKey(SetTimeGetRewardKey());
            }
        }

        protected abstract string SetCurrentSlotInActiveKey();

        protected abstract string SetTimeGetRewardKey();
    }
}