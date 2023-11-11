namespace Rewards
{
    internal sealed class DailyRewardView : RewardView
    {
        private const string CurrentDailySlotInActiveKey = nameof(CurrentDailySlotInActiveKey);
        private const string TimeGetDailyRewardKey = nameof(TimeGetDailyRewardKey);

        protected override string SetCurrentSlotInActiveKey()
        {
            return CurrentDailySlotInActiveKey;
        }

        protected override string SetTimeGetRewardKey()
        {
            return TimeGetDailyRewardKey;
        }
    }
}
