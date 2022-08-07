namespace Rewards
{
    internal class WeeklyRewardView : RewardView
    {
        private const string CurrentWeeklySlotInActiveKey = nameof(CurrentWeeklySlotInActiveKey);
        private const string TimeGetWeeklyRewardKey = nameof(TimeGetWeeklyRewardKey);

        public WeeklyRewardView()
        {
            TimeCooldown = 604800;
            TimeDeadline = 1209600;
        }

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