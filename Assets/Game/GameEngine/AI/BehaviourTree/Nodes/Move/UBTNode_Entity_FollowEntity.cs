using AI.Blackboards;
using AI.BTree;
using Elementary;
using Entities;
using UnityEngine;

namespace Game.GameEngine.AI
{
    [AddComponentMenu(BehaviourTreePaths.MENU_PATH + "BTNode «Follow Entity» (Entity)")]
    public sealed class UBTNode_Entity_FollowEntity : UnityBehaviourNode, IBlackboardInjective
    {
        public IBlackboard Blackboard { private get; set; }

        [Space]
        [SerializeField]
        private FloatAdapter stoppingDistance; //0.15f

        [Space]
        [BlackboardKey]
        [SerializeField]
        private string unitKey;

        [BlackboardKey]
        [SerializeField]
        private string targetKey;

        private Agent_Entity_FollowEntity followAgent;
        
        protected override void Run()
        {
            if (!this.Blackboard.TryGetVariable(this.unitKey, out IEntity unit))
            {
                this.Return(false);
                return;
            }

            if (!this.Blackboard.TryGetVariable(this.targetKey, out IEntity target))
            {
                this.Return(false);
                return;
            }

            this.followAgent.OnTargetReached += this.OnTargetReached;
            this.followAgent.SetFollowingEntity(unit);
            this.followAgent.SetTargetEntity(target);
            this.followAgent.Play();
        }

        private void OnTargetReached(bool isReached)
        {
            if (isReached)
            {
                this.Return(true);
            }
        }

        private void Awake()
        {
            this.followAgent = new Agent_Entity_FollowEntity(coroutineDispatcher: this);
            this.followAgent.SetStoppingDistance(this.stoppingDistance.Value);
        }

        protected override void OnEnd()
        {
            this.followAgent.OnTargetReached -= this.OnTargetReached;
            this.followAgent.Stop();
        }
    }
}