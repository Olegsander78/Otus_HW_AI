using System;
using System.Collections;
using System.Collections.Generic;
using Entities;
using Game.GameEngine.Mechanics;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Lessons.AI.Lesson_Architecture
{
    public sealed class MoveByPointsAgent : Agent
    {
        //private const int POINTS_BUFFER = 16;

        public event Action OnPathFinished;
        public bool IsPathFinished
        {
            get { return _isPathFinished; }
        }

        [ShowInInspector, ReadOnly]
        private IEntity _unit;

        //[ShowInInspector, ReadOnly]
        //private float _stoppingDistance;

        //[ShowInInspector, ReadOnly]
        //private Vector3 _targetPosition;

        [SerializeField]
        private List<Transform> _currentPath;

        [SerializeField]
        private float _delayInPoint = 0;

        private int _pointer = 0;

        private bool _isPathFinished;

        [SerializeField]
        private float _stoppingDistance = 0.01f;

        private IComponent_GetPosition _positionComponent;

        private IComponent_MoveInDirection _moveComponent;

        //private Coroutine _moveCoroutine;

        private Coroutine _loopCoroutine;

        [Button]
        public void SetUnit(IEntity unit)
        {
            _unit = unit;
            _positionComponent = unit?.Get<IComponent_GetPosition>();
            _moveComponent = unit?.Get<IComponent_MoveInDirection>();
        }

        protected override void OnStart()
        {
            _loopCoroutine = StartCoroutine(LoopCoroutine());            
        }

        protected override void OnStop()
        {
            if (_loopCoroutine != null)
            {
                StopCoroutine(_loopCoroutine);
                _loopCoroutine = null;
            }
        }

        private IEnumerator LoopCoroutine()
        {
            var period = new WaitForFixedUpdate();

            while (true)
            {
                yield return period;
                UpdatePoint();
            }
        }

        //[Button]
        //public void SetPath(IEnumerable<Vector3> points)
        //{
        //    _currentPath.Clear();
        //    _currentPath.AddRange(points);

        //    _pointer = 0;
        //    _isPathFinished = false;
        //}

        //[Button]
        //public void SetPath(IEnumerable<Transform> points)
        //{
        //    _currentPath.Clear();
        //    //List<Transform> pointsTransform = new();
        //    //pointsTransform.AddRange(points);

        //    //for (int i = 0; i < pointsTransform.Count; i++)
        //    //{
        //    //    _currentPath.Add(pointsTransform[i].position);
        //    //}
        //    _currentPath.AddRange(points);

        //    _pointer = 0;
        //    _isPathFinished = false;
        //}
              

        private void UpdatePoint()
        {
            if (_isPathFinished)
            {
                //return;
                _pointer = 0;
                _isPathFinished = false;
            }

            if (_pointer >= _currentPath.Count)
            {
                _isPathFinished = true;
                OnPathFinished?.Invoke();
                return;
            }

            var targetPoint = _currentPath[_pointer];

            while (!CheckPointReached(targetPoint.position))
            {
                MoveToPoint(targetPoint.position);

                if (CheckPointReached(targetPoint.position))
                {
                    _pointer++;
                }
            }

            //if (CheckPointReached(targetPoint.position))
            //{
            //    _pointer++;
            //}
            //else
            //{
            //    MoveToPoint(targetPoint.position);                
            //}
        }

        //private void DoMove(Vector3 targetPosition)
        //{
        //    var myPosition = _positionComponent.Position;
        //    var distanceVector = targetPosition - myPosition;

        //    var isReached = distanceVector.sqrMagnitude <= _stoppingDistance * _stoppingDistance;
        //    if (!isReached)
        //    {
        //        var moveDirection = distanceVector.normalized;
        //        _moveComponent.Move(moveDirection);
        //    }
        //    else
        //    {
        //        Debug.Log("Position Reached");
        //    }
        //}

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





    //[Button]
    //    public void SetTargetPosition(Transform point)
    //    {
    //        _targetPosition = point.position;
    //    }

    //    [Button]
    //    public void SetTargetPosition(Vector3 position)
    //    {
    //        _targetPosition = position;
    //    }

    //    [Button]
    //    public void SetStoppingDistance(float stoppingDistance)
    //    {
    //        _stoppingDistance = stoppingDistance;
    //    }



    //protected override void OnStart()
    //{
    //    _moveCoroutine = StartCoroutine(MoveRoutine());
    //}

    //protected override void OnStop()
    //{
    //    if (_moveCoroutine != null)
    //    {
    //        StopCoroutine(_moveCoroutine);
    //        _moveCoroutine = null;
    //    }
    //}

    //private IEnumerator MoveRoutine()
    //{
    //    var period = new WaitForFixedUpdate();

    //    while (true)
    //    {
    //        if (_unit != null)
    //        {
    //            DoMove();
    //        }

    //        yield return period;
    //    }
    //}

    //private void DoMove()
    //{
    //    var myPosition = _positionComponent.Position;
    //    var distanceVector = _targetPosition - myPosition;

    //    var isReached = distanceVector.sqrMagnitude <= _stoppingDistance * _stoppingDistance;
    //    if (!isReached)
    //    {
    //        var moveDirection = distanceVector.normalized;
    //        _moveComponent.Move(moveDirection);
    //    }
    //    else
    //    {
    //        Debug.Log("Position Reached");
    //    }
    //}

}