using AI.Blackboards;
using AI.BTree;
using Entities;
using Game.GameEngine.Mechanics;
using UnityEngine;

namespace Lessons.AI.LessonGOAP
{
    public sealed class UseHealingNode : UnityBehaviourNode, IBlackboardInjective
    {
        public IBlackboard Blackboard { private get; set; }

        [Space]
        [BlackboardKey]
        [SerializeField]
        private string unitKey;

        protected override void Run()
        {
            if (!this.Blackboard.TryGetVariable(this.unitKey, out IEntity unit))
            {
                this.Return(false);
                return;   
            }
            
            Debug.Log("HEALING");
            var component = unit.Get<UComponent_HitPoints>();
            var previousHitPoints = component.HitPoints;
            var newHitPoints = previousHitPoints + 5;
            component.SetHitPoints(newHitPoints);
            
            this.Return(true);
        }
    }
}