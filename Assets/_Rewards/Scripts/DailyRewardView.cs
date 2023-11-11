namespace Rewards
{
    internal sealed class DailyRewardView : RewardView
    {
        private const string CurrentDailySlotInActiveKey = nameof(CurrentDailySlotInActiveKey);
        private const string TimeGetDailyRewardKey = nameof(TimeGetDailyRewardKey);

        public DailyRewardView()
        {
            TimeCooldown = 86400;
            TimeDeadline = 172800;
        }

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
