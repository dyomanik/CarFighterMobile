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
        [SerializeField] private Button _buttonStart;
        [SerializeField] private Button _buttonSettings;
        [SerializeField] private Button _buttonRewardedAds;
        [SerializeField] private Button _buttonBuyProduct;
        [SerializeField] private Button _buttonShed;

        public void Init(UnityAction startGame, UnityAction settings, UnityAction rewardedAds, UnityAction<string> buyProduct, UnityAction shed)
        {
            _buttonStart.onClick.AddListener(startGame);
            _buttonSettings.onClick.AddListener(settings);
            _buttonRewardedAds.onClick.AddListener(rewardedAds);
            _buttonBuyProduct.onClick.AddListener(() => buyProduct(_productId));
            _buttonShed.onClick.AddListener(shed);
        }

        public void OnDestroy()
        {
            _buttonStart.onClick.RemoveAllListeners();
            _buttonSettings.onClick.RemoveAllListeners();
            _buttonRewardedAds.onClick.RemoveAllListeners();
            _buttonBuyProduct.onClick.RemoveAllListeners();
            _buttonShed.onClick.RemoveAllListeners();
        }
    }
}