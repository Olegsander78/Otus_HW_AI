using AI.GOAP;
using UnityEngine;

namespace Lessons.AI.LessonGOAP
{
    public sealed class DestroyEnemyGoal : UnityGoalBase
    {
        [SerializeField]
        private int priority;

        [ParameterKey]
        [SerializeField]
        private string targetAliveKey;

        [SerializeField]
        private UnityWorldState worldState;

        public override int EvaluatePriority()
        {
            return this.priority;
        }

        public override bool IsValid()
        {
            return this.worldState.ContainsParameter(this.targetAliveKey);
        }
    }
}