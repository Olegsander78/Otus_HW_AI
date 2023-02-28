using AI.Blackboards;
using UnityEngine;

namespace Game.GameEngine.AI
{
    public sealed class CommandProcessor_AttackTarget : CommandProcessor_Node, IBlackboardInjective
    {
        public IBlackboard Blackboard { private get; set; }
        
        [Header("Target")]
        [BlackboardKey]
        [SerializeField]
        private string targetKey;

        protected override void Execute(CommandType type, object args)
        {
            if (type != CommandType.ATTACK_TARGET)
            {
                this.Complete();
                return;
            }

            var attackArgs = (CommandArgs_AttackTarget) args;
            var target = attackArgs.target;
            this.Blackboard.AddVariable(this.targetKey, target);
        
            base.Execute(type, args);
        }

        protected override void End()
        {
            this.Blackboard.RemoveVariable(this.targetKey);
        }
    }
}