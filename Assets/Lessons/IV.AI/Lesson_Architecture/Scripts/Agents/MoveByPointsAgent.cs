using System;
using System.Collections;
using System.Collections.Generic;
using Entities;
using Game.GameEngine.Mechanics;
using Sirenix.OdinInspector;
using UnityEngine;
using AI.Waypoints;

namespace Lessons.AI.Lesson_Architecture
{
    public sealed class MoveByPointsAgent : Agent
    {
        public event Action OnPathFinished;
        public bool IsPathFinished
        {
            get { return _isPathFinished; }
        }

        [ShowInInspector, ReadOnly]
        private IEntity _unit;

        [ShowInInspector, ReadOnly]
        private List<Transform> _currentPath;

        [SerializeField]
        private WaypointsPath _pathPoints;

        [SerializeField]
        private bool _isLooping;

        [SerializeField]
        private float _delayInPoint = 0;

        [SerializeField]
        private float _stoppingDistance = 0.01f;

        private int _pointer = 0;

        private bool _isPathFinished;

        private IComponent_GetPosition _positionComponent;

        private IComponent_MoveInDirection _moveComponent;

        private Coroutine _moveCoroutine;

        private Coroutine _loopCoroutine;

        [Button]
        public void SetUnit(IEntity unit)
        {
            _unit = unit;
            _positionComponent = unit?.Get<IComponent_GetPosition>();
            _moveComponent = unit?.Get<IComponent_MoveInDirection>();

            SetPath();
        }
                
        public void SetPath()
        {
            _currentPath = _pathPoints.GetTransformPoints();
        }

        protected override void OnStart()
        {
            _loopCoroutine = StartCoroutine(LoopPointsCoroutine());            
        }

        protected override void OnStop()
        {
            if (_loopCoroutine != null)
            {
                StopCoroutine(_loopCoroutine);
                _loopCoroutine = null;
            }

            if (_moveCoroutine != null)
            {
                StopCoroutine(_moveCoroutine);
                _moveCoroutine = null;
            }
        }

        private IEnumerator LoopPointsCoroutine()
        {
            var period = new WaitForSeconds(_delayInPoint);

            while (true)
            {
                yield return period;
                UpdateMoveToPoint();
            }
        }
                      

        private void UpdateMoveToPoint()
        {
            if (_isPathFinished && _isLooping)
            {                
                _pointer = 0;
                _isPathFinished = false;
            }

            if (_isPathFinished && !_isLooping)
            {
                return;
            }

            if (_pointer >= _currentPath.Count)
            {
                _isPathFinished = true;
                OnPathFinished?.Invoke();
                return;
            }

            var targetPoint = _currentPath[_pointer];


            if (CheckPointReached(targetPoint.position))
            {
                _pointer++;
            }
            else
            {
                _moveCoroutine = StartCoroutine(MoveRoutine(targetPoint.position));  
            }
        }

        private IEnumerator MoveRoutine(Vector3 point)
        {
            var period = new WaitForFixedUpdate();

            while (!CheckPointReached(point))
            {
                yield return period;
                MoveToPoint(point);
            }
        }

        private bool CheckPointReached(Vector3 point)
        {
            var moveVector = EvaluateMoveVector(point);
            return moveVector.sqrMagnitude <= Mathf.Pow(_stoppingDistance, 2);
        }

        private void MoveToPoint(Vector3 target)
        {
            var moveVector = EvaluateMoveVector(target);
            _moveComponent.Move(moveVector.normalized);
        }

        private Vector3 EvaluateMoveVector(Vector3 targetPosition)
        {
            var currentPosition = _positionComponent.Position;
            var moveVector = targetPosition - currentPosition;
            moveVector.y = 0;
            return moveVector;
        }
    }
}