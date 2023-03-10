using AI.Blackboards;
using AI.BTree;
using AI.GOAP;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Game.GameEngine.AI
{
    public sealed class GoalOrientedBehaviour : MonoBehaviour, IBehaviourCallback
    {
        [ShowInInspector, PropertyOrder(-1)]
        public bool IsActive
        {
            get { return this.currentProcess is {IsRunning: true}; }
        }

        [SerializeField, Space]
        private bool autoPlay = true;

        [SerializeField]
        private bool loop = true;

        [SerializeField, Space]
        private UnityGoalPlanner goalPlanner;

        [SerializeField, Space]
        private UnityBlackboard blackboard;

        [SerializeField]
        private UnityBlackboard whiteboard;
        
        [SerializeField, Space]
        private GoalOrientedConverter actionConverter;

        private IBehaviourNode currentProcess;
        
        private void Start()
        {
            if (this.autoPlay)
            {
                this.Activate();
            }
        }

        private void Update()
        {
            if (this.loop && !this.IsActive)
            {
                this.Activate();
            }
        }
        
        [Button]
        public void Activate()
        {
            if (this.goalPlanner.MakePlan(out var plan))
            {
                this.PrepareWhiteboard();
                this.ExecutePlan(plan);
            }
        }

        [Button]
        public void Deactivate()
        {
            if (this.currentProcess is {IsRunning: true})
            {
                this.currentProcess.Abort();
            }

            this.currentProcess = null;
            this.whiteboard.Clear();
        }

        [Button]
        public void Reactivate()
        {
            this.Deactivate();
            this.Activate();
        }

        private void PrepareWhiteboard()
        {
            this.whiteboard.Clear();
            
            var variables = this.blackboard.GetVariables();
            foreach (var (key, value) in variables)
            {
                this.whiteboard.AddVariable(key, value);
            }
        }

        private void ExecutePlan(Plan plan)
        {
            var actions = plan.actions;
            this.currentProcess = this.actionConverter.ConvertToBTSequence(actions);
            this.currentProcess.Run(callback: this);
        }

        void IBehaviourCallback.Invoke(IBehaviourNode node, bool success)
        {
            this.currentProcess = null;
            this.whiteboard.Clear();
        }
    }
}