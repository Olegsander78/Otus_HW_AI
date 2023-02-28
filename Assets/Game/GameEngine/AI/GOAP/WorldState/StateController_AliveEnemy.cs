using Entities;
using Game.GameEngine;
using Game.GameEngine.Mechanics;

namespace AI.GOAP.Unity
{
    public sealed class StateController_AliveEnemy : CoroutineStateController
    {
        private IEntity targetEnemy;

        public void StartObserve(IEntity enemy)
        {
            this.targetEnemy = enemy;
            this.worldState.AddParameter(this.stateName, this.EvaluateValue());
            this.Activate();
        }

        public void StopObserve()
        {
            this.Deactivate();
            this.targetEnemy = null;
            this.worldState.RemoveParameter(this.stateName);
        }

        protected override bool EvaluateValue()
        {
            var aliveComponent = this.targetEnemy.Get<IComponent_IsAlive>();
            return aliveComponent.IsAlive;
        }
    }
}