using Entities;
using Game.GameEngine;
using Game.GameEngine.Mechanics;
using UnityEngine;

namespace AI.GOAP.Unity
{
    public sealed class StateController_NearEnemy : CoroutineStateController
    {
        [Space]
        [SerializeField]
        private UnityEntity myUnit;
        
        [Space]
        [SerializeField]
        private float minDistance;
        
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
            var enemyTransorm = this.targetEnemy.Get<IComponent_GetPosition>();
            var myTransform = this.myUnit.Get<IComponent_GetPosition>();

            var distanceVector = enemyTransorm.Position - myTransform.Position;
            distanceVector.y = 0;

            return distanceVector.magnitude <= this.minDistance;
        }
    }
}