using AI.Blackboards;
using AI.BTree;
using AI.GOAP;

namespace Game.GameEngine
{
    public sealed class _PlanningBehaviour : IPlanningBehaviour, IBehaviourCallback
    {
        public bool IsPlaying
        {
            get { return this.currentProcess is {IsRunning: true}; }
        }
        
        private readonly IGoalPlanner goalPlanner;

        private readonly ISequenceComposer<IBehaviourNode> actionsComposer;

        private readonly IBlackboard blackboard;

        private readonly IBlackboard whiteboard;
        
        private IBehaviourNode currentProcess;

        public _PlanningBehaviour(
            IGoalPlanner goalPlanner,
            ISequenceComposer<IBehaviourNode> actionsComposer,
            IBlackboard blackboard,
            IBlackboard whiteboard
        )
        {
            this.goalPlanner = goalPlanner;
            this.actionsComposer = actionsComposer;
            this.blackboard = blackboard;
            this.whiteboard = whiteboard;
        }

        public void Play()
        {
            this.Stop();

            if (this.goalPlanner.MakePlan(out var plan))
            {
                this.PrepareWhiteboard();
                this.ExecutePlan(plan);
            }
        }

        public void Stop()
        {
            if (this.currentProcess is {IsRunning: true})
            {
                this.currentProcess.Abort();
            }

            this.currentProcess = null;
            this.whiteboard.Clear();
        }

        private void PrepareWhiteboard()
        {
            var variables = this.blackboard.GetVariables();
            foreach (var variable in variables)
            {
                this.whiteboard.AddVariable(variable.Key, variable.Value);
            }
        }

        private void ExecutePlan(Plan plan)
        {
            var actions = plan.actions;
            var nodes = this.actionsComposer.ComposeSequence(actions);
            this.currentProcess = new BehaviourNode_Sequence(nodes);
            this.currentProcess.Run(callback: this);
        }

        void IBehaviourCallback.Invoke(IBehaviourNode node, bool success)
        {
            this.currentProcess = null;
            this.whiteboard.Clear();
        }
    }
}