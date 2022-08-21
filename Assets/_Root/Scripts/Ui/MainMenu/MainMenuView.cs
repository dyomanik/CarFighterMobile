using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Ui
{
    internal class MainMenuView : MonoBehaviour
    {
        [Header("Settings")]
        [SerializeField] private string _productId;

        [Header("Buttons")]
        [SerializeField] private Button _startButton;
        [SerializeField] private Button _dailyRewardButton;
        [SerializeField] private Button _settingsButton;
        [SerializeField] private Button _rewardedAdsButton;
        [SerializeField] private Button _buyProductButton;
        [SerializeField] private Button _shedButton;
        [SerializeField] private Button _exitButton;

        public void Init(UnityAction startGame, UnityAction settings, UnityAction rewardedAds,
            UnityAction<string> buyProduct, UnityAction shed, UnityAction dailyReward, UnityAction exit)
        {
            _startButton.onClick.AddListener(startGame);
            _settingsButton.onClick.AddListener(settings);
            _rewardedAdsButton.onClick.AddListener(rewardedAds);
            _buyProductButton.onClick.AddListener(() => buyProduct(_productId));
            _shedButton.onClick.AddListener(shed);
            _dailyRewardButton.onClick.AddListener(dailyReward);
            _exitButton.onClick.AddListener(exit);
        }

        public void OnDestroy()
        {
            _startButton.onClick.RemoveAllListeners();
            _settingsButton.onClick.RemoveAllListeners();
            _rewardedAdsButton.onClick.RemoveAllListeners();
            _buyProductButton.onClick.RemoveAllListeners();
            _shedButton.onClick.RemoveAllListeners();
            _dailyRewardButton.onClick.RemoveAllListeners();
            _exitButton.onClick.RemoveAllListeners();
        }
    }
}