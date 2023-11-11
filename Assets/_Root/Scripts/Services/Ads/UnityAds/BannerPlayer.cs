using UnityEngine.Advertisements;

namespace Services.Ads.UnityAds
{
    internal class BannerPlayer : UnityAdsPlayer
    {
        public BannerPlayer(string id) : base(id) { }

        protected override void OnPlaying() => Advertisement.Banner.Show(Id);
        protected override void Load() => Advertisement.Banner.Load(Id);
    }
}
