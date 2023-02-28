using AI.Agents;
using Entities;
using Game.GameEngine.Mechanics;
using UnityEngine;

namespace Game.GameEngine.AI
{
    public class Agent_Entity_MoveToPositionByNavMesh : Agent_MoveByNavMesh
    {
        protected override Agent_MoveByPath<Vector3> MoveAgent
        {
            get { return this.moveAgent; }
        }

        protected override Vector3 CurrentPosition
        {
            get { return this.positionComponent.Position; }
        }

        private readonly Agent_Entity_MoveByPoints moveAgent;

        private IComponent_GetPosition positionComponent;

        public Agent_Entity_MoveToPositionByNavMesh(MonoBehaviour coroutineDispatcher)
        {
            this.moveAgent = new Agent_Entity_MoveByPoints(coroutineDispatcher);
        }

        public void SetMovingEntity(IEntity movingEntity)
        {
            this.positionComponent = movingEntity.Get<IComponent_GetPosition>();
            this.moveAgent.SetMovingEntity(movingEntity);
        }

        public void SetStoppingDistance(float stoppingDistance)
        {
            this.moveAgent.SetStoppingDistance(stoppingDistance);
        }
    }
}