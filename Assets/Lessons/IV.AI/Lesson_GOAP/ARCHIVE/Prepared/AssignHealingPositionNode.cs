using AI.Blackboards;
using AI.BTree;
using UnityEngine;

namespace Lessons.AI.LessonGOAP
{
    public sealed class AssignHealingPositionNode : UnityBehaviourNode, IBlackboardInjective
    {
        public IBlackboard Blackboard { private get; set; }
        
        [BlackboardKey]
        [SerializeField]
        private string healingPointKey;

        [BlackboardKey]
        [SerializeField]
        private string movePositionKey;
    
        protected override void Run()
        {
            if (this.Blackboard.TryGetVariable(this.healingPointKey, out Transform healingPoint))
            {
                this.Blackboard.ReplaceVariable(this.movePositionKey, healingPoint.position);
            }
        }
    }
}