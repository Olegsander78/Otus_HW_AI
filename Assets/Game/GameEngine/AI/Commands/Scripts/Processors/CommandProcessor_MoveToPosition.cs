using AI.Blackboards;
using UnityEngine;

namespace Game.GameEngine.AI
{
    public sealed class CommandProcessor_MoveToPosition : CommandProcessor_Node, IBlackboardInjective
    {
        public IBlackboard Blackboard { private get; set; }

        [BlackboardKey]
        [SerializeField]
        private string movePositionKey;

        protected override void Execute(CommandType type, object args)
        {
            if (type != CommandType.MOVE_TO_POSITION)
            {
                this.Complete();
                return;
            }

            var moveArgs = (CommandArgs_MoveToPosition) args;
            var movePosition = moveArgs.targetPosition;
            this.Blackboard.AddVariable(this.movePositionKey, movePosition);
        
            base.Execute(type, args);
        }

        protected override void End()
        {
            this.Blackboard.RemoveVariable(this.movePositionKey);
        }
    }
}