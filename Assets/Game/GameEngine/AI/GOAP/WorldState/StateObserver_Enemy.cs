using Entities;
using UnityEngine;

namespace AI.GOAP.Unity
{
    public sealed class StateObserver_Enemy : MonoBehaviour
    {
        [SerializeField]
        private StateController_SeeEnemy controllerSee;
        
        [SerializeField]
        private StateController_AliveEnemy controllerAlive;

        [SerializeField]
        private StateController_NearEnemy controllerNear;

        public void EnemyEntered(IEntity enemy)
        {
            this.controllerSee.StartObserve();
            this.controllerAlive.StartObserve(enemy);
            this.controllerNear.StartObserve(enemy);
        }

        public void EnemyExited()
        {
            this.controllerSee.StopObserve();
            this.controllerAlive.StopObserve();
            this.controllerNear.StopObserve();
        }
    }
}