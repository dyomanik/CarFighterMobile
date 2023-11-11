using System;

namespace Tools
{
    internal interface IAdsShower
    {
        void ShowInterstitial();
        void ShowRewarded(Action successShow);
    }
}