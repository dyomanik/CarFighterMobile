using System;

namespace Rewards
{
    internal sealed class DailyRewardController : RewardController
    {
        public DailyRewardController(RewardView view) : base(view) { }

        protected override string GetTimerNewRewardText()
        {
            if (_isGetReward)
                return "The reward is ready to be received!";

            if (_view.TimeGetReward.HasValue)
            {
                DateTime nextClaimTime = _view.TimeGetReward.Value.AddSeconds(_view.TimeCooldown);
                TimeSpan currentClaimCooldown = nextClaimTime - DateTime.UtcNow;

                string timeGetReward =
                    $"{currentClaimCooldown.Hours:D2}:{currentClaimCooldown.Minutes:D2}:" +
                    $"{currentClaimCooldown.Seconds:D2}";

                return $"Time to get the next reward: {timeGetReward}";
            }

            return string.Empty;
        }
    }
}
