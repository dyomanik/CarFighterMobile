namespace Rewards
{
    internal sealed class WeeklyRewardController : RewardController
    {
        public WeeklyRewardController(RewardView view) : base (view) {}

        protected override string GetTimerNewRewardText()
        {
            if (_isGetReward)
                return "The reward is ready to be received!";

            if (_view.TimeGetReward.HasValue)
            {
                System.DateTime nextClaimTime = _view.TimeGetReward.Value.AddSeconds(_view.TimeCooldown);
                System.TimeSpan currentClaimCooldown = nextClaimTime - System.DateTime.UtcNow;

                string timeGetReward =
                    $"{currentClaimCooldown.Days:D2} Day(s) {currentClaimCooldown.Hours:D2} Hour(s)" +
                    $" {currentClaimCooldown.Minutes:D2} Minute(s)";

                return $"Time to get the next reward: {timeGetReward}";
            }

            return string.Empty;
        }
    }
}