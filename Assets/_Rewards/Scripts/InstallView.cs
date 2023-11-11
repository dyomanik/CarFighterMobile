using UnityEngine;

namespace Rewards
{
    internal class InstallView : MonoBehaviour
    {
        [SerializeField] private RewardView _rewardView;

        private RewardController _rewardController;

        private void Awake() => 
            CheckPeriodicityOfRewardView();

        private void Start() =>
            _rewardController.Init();

        private void OnDestroy() =>
            _rewardController.Deinit();

        private void CheckPeriodicityOfRewardView()
        {
            switch (_rewardView)
            {
                case DailyRewardView:
                    _rewardController = new DailyRewardController(_rewardView);
                    break;
                case WeeklyRewardView:
                    _rewardController = new WeeklyRewardController(_rewardView);
                    break;
            }
        }
    }
}
