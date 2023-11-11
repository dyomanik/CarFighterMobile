using UnityEngine;

namespace Features.Rewards
{
    [CreateAssetMenu(fileName = nameof(RewardConfig), menuName = "Configs/" + nameof(RewardConfig))]
    internal class RewardConfig : ScriptableObject
    {
        [field: SerializeField] public Reward Reward { get; private set; }
    }
}