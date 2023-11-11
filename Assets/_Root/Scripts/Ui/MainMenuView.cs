using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Ui
{
    internal class MainMenuView : MonoBehaviour
    {
        [SerializeField] private Button _buttonStart;
        [SerializeField] private Button _buttonSettings;
        [SerializeField] private Button _buttonRewardedAds;
        [SerializeField] private Button _buttonBuy;

        public void Init(UnityAction startGame, UnityAction settings, UnityAction rewardedAds, UnityAction buy)
        {
            _buttonStart.onClick.AddListener(startGame);
            _buttonSettings.onClick.AddListener(settings);
            _buttonRewardedAds.onClick.AddListener(rewardedAds);
            _buttonBuy.onClick.AddListener(buy);
        }

        public void OnDestroy()
        {
            _buttonStart.onClick.RemoveAllListeners();
            _buttonSettings.onClick.RemoveAllListeners();
            _buttonRewardedAds.onClick.RemoveAllListeners();
            _buttonBuy.onClick.RemoveAllListeners();
        }
    }
}