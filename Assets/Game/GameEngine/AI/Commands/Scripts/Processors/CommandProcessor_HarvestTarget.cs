using AI.Blackboards;
using Entities;
using UnityEngine;

namespace Game.GameEngine.AI
{
    public sealed class CommandProcessor_HarvestTarget : CommandProcessor_Node, IBlackboardInjective
    {
        public IBlackboard Blackboard { private get; set; }
        
        [Header("Target")]
        [BlackboardKey]
        [SerializeField]
        private string targetKey;

        protected override void Execute(CommandType type, object args)
        {
            if (type != CommandType.HARVEST_RESOURCE)
            {
                this.Complete();
                return;
            }

            var harvestArgs = (CommandArgs_HarvestTarget) args;
            var target = harvestArgs.target;
            this.Blackboard.AddVariable(this.targetKey, target);
        
            base.Execute(type, args);
        }

        protected override void End()
        {
            this.Blackboard.RemoveVariable(this.targetKey);
        }
    }
}