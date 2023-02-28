using AI.BTree;
using UnityEngine;

namespace Game.GameEngine.AI
{
    public class CommandProcessor_Node : CommandProcessor, IBehaviourCallback
    {
        [SerializeField]
        private UnityBehaviourNode root;

        protected override void Execute(CommandType type, object args)
        {
            this.root.Run(callback: this);
        }

        protected sealed override void OnCancel()
        {
            this.root.Abort();
        }

        void IBehaviourCallback.Invoke(IBehaviourNode node, bool success)
        {
            this.Complete();
        }
    }
}