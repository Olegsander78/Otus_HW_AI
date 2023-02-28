using Game.GameEngine.Mechanics;
using UnityEngine;

namespace AI.GOAP.Unity
{
    public sealed class HealingGoal : AbstractGoal
    {
        [Space]
        [SerializeField]
        private int maxHitPoints;

        [SerializeField]
        private int maxPriority;

        [Space]
        [SerializeField]
        private UHitPointsEngine hitPointsMechanics;

        public override int EvaluatePriority()
        {
            var currentHitPoints = Mathf.Min(this.hitPointsMechanics.CurrentHitPoints, this.maxHitPoints);
            var percent = (float) currentHitPoints / this.maxHitPoints;
            var priority = Mathf.RoundToInt(this.maxPriority * (1.0f - percent));
            return priority;
        }
    }
}