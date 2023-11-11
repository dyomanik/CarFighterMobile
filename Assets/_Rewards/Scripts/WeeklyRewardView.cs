namespace Rewards
{
    internal sealed class WeeklyRewardView : RewardView
    {
        private const string CurrentWeeklySlotInActiveKey = nameof(CurrentWeeklySlotInActiveKey);
        private const string TimeGetWeeklyRewardKey = nameof(TimeGetWeeklyRewardKey);

        protected override string SetCurrentSlotInActiveKey()
        {
            return CurrentWeeklySlotInActiveKey;
        }

        protected override string SetTimeGetRewardKey()
        {
            return TimeGetWeeklyRewardKey;
        }
    }
}