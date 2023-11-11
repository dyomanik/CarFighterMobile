using System;
using UnityEngine;
using UnityEngine.Advertisements;
namespace Tools
{
    internal class UnityAdsTools : MonoBehaviour, IAdsShower, IUnityAdsListener
    {
        private string _gameId = "4828263";
        private string _rewardedPlace = "Rewarded_Android";
        private string _interstitialPlace = "Interstitial_Android";
        private Action _callbackSuccessShowVideo;

        private void Start()
        {
            Advertisement.Initialize(_gameId, true);
        }

        public void ShowInterstitial()
        {
            _callbackSuccessShowVideo = null;
            Advertisement.Show(_interstitialPlace);
            Advertisement.Load(_interstitialPlace);
        }

        public void ShowRewarded(Action successShow)
        {
            _callbackSuccessShowVideo = successShow;
            Advertisement.Show(_rewardedPlace);
            Debug.Log("You have received 5 coins");
            Advertisement.Load(_rewardedPlace);
        }

        public void OnUnityAdsReady(string placementId)
        {
        }

        public void OnUnityAdsDidError(string message)
        {
        }

        public void OnUnityAdsDidStart(string placementId)
        {
        }

        public void OnUnityAdsDidFinish(string placementId, ShowResult showResult)
        {
            if (showResult == ShowResult.Finished)
                _callbackSuccessShowVideo?.Invoke();
        }
    }
}
