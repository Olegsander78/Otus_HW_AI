//using System;
//using System.Collections;
//using System.Collections.Generic;
//using Entities;
//using Game.GameEngine.Mechanics;
//using Sirenix.OdinInspector;
//using UnityEngine;

//namespace Lessons.AI.Lesson_Architecture
//{
//    public abstract class Agent_Entity_MoveByPoints: Agent_MoveByPath<Vector3>
//    {
//        private IComponent_MoveInDirection _moveComponent;

//        private IComponent_GetPosition _positionComponent;

//        private float _sqrStoppingDistance = 0.01f;

//        public Agent_Entity_MoveByPoints(MonoBehaviour coroutineDispatcher, float delay) :
//            base(coroutineDispatcher, new WaitForSeconds(delay))
//        {
//        }
//        //public Agent_Entity_MoveByPoints(MonoBehaviour coroutineDispatcher) :
//        //   base(coroutineDispatcher, new WaitForFixedUpdate())
//        //{
//        //}

//        public void SetMovingEntity(IEntity movingEntity)
//        {
//            _moveComponent = movingEntity.Get<IComponent_MoveInDirection>();
//            _positionComponent = movingEntity.Get<IComponent_GetPosition>();
//        }

//        public void SetStoppingDistance(float stoppingDistance)
//        {
//            _sqrStoppingDistance = Mathf.Pow(stoppingDistance, 2);
//        }

//        protected override bool CheckPointReached(Vector3 point)
//        {
//            var moveVector = EvaluateMoveVector(point);
//            return moveVector.sqrMagnitude <= _sqrStoppingDistance;
//        }

//        protected override void MoveToPoint(Vector3 target)
//        {
//            var moveVector = this.EvaluateMoveVector(target);
//            _moveComponent.Move(moveVector.normalized);
//        }

//        private Vector3 EvaluateMoveVector(Vector3 targetPosition)
//        {
//            var currentPosition = _positionComponent.Position;
//            var moveVector = targetPosition - currentPosition;
//            moveVector.y = 0;
//            return moveVector;
//        }
//    }
//}