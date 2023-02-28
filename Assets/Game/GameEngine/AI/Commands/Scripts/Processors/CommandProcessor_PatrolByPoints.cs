using AI.Blackboards;
using AI.Iterators;
using UnityEngine;

namespace Game.GameEngine.AI
{
    public sealed class CommandProcessor_PatrolByPoints : CommandProcessor_Node, IBlackboardInjective
    {
        public IBlackboard Blackboard { private get; set; }

        [BlackboardKey]
        [SerializeField]
        private string patrolIteratorKey;

        protected override void Execute(CommandType type, object args)
        {
            if (type != CommandType.PATROL_BY_POINTS)
            {
                this.Complete();
                return;
            }

            var patrolArgs = (CommandArgs_PatrolByPoints) args;
            var movePositions = patrolArgs.patrolPoints;
            var patrolIterator = new CircleIterator<Vector3>(movePositions);
            this.Blackboard.AddVariable(this.patrolIteratorKey, patrolIterator);
        
            base.Execute(type, args);
        }

        protected override void End()
        {
            this.Blackboard.RemoveVariable(this.patrolIteratorKey);
        }
    }
}