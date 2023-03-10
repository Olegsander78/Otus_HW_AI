using AI.Blackboards;
using Elementary;
using Game.GameEngine.Mechanics;
using Lessons.AI.Lesson_BehaviourTree1;
using UnityEngine;
using UnityEngine.Serialization;
using Blackboard = Lessons.AI.Architecture2.Blackboard;

namespace Lessons.AI.Lesson_Commands2
{
    public sealed class AIMeleeCombatState : MonoState, BehaviourNode.ICallback
    {
        [SerializeField]
        private UMeleeCombatEngine meleeCombatEngine;

        [SerializeField]
        private FloatAdapter stoppingDistance;

        [Space]
        [SerializeField]
        private Blackboard blackboard;

        [BlackboardKey]
        [SerializeField]
        private string targetKey;

        [BlackboardKey]
        [SerializeField]
        private string stoppingDistanceKey;

        [FormerlySerializedAs("moveNode")]
        [Space]
        [SerializeField]
        private BehaviourNode attackNode;

        public override void Enter()
        {
            this.blackboard.AddVariable(this.targetKey, this.meleeCombatEngine.CurrentOperation.targetEntity);
            this.blackboard.AddVariable(this.stoppingDistanceKey, this.stoppingDistance.Value);
            this.attackNode.Run(callback: this);
        }

        public override void Exit()
        {
            this.attackNode.Abort();
            this.blackboard.RemoveVariable(this.targetKey);
            this.blackboard.RemoveVariable(this.stoppingDistanceKey);
        }

        void BehaviourNode.ICallback.Invoke(BehaviourNode node, bool success)
        {
            //TODO: TARGET DESTROYED BLACKBOARD KEY...
            this.meleeCombatEngine.StopCombat();
        }
    }
}