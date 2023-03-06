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
    [RequireComponent(typeof(MoveAgent))]
    public sealed class PatrolAgent : Agent
    {

        [SerializeField]
        private MoveAgent _moveAgent;

        [ShowInInspector, ReadOnly]
        private List<Vector3> _currentPath;

        [SerializeField]
        private WaypointsPath _pathPoints;

        [SerializeField]
        private bool _isLooping;

        [SerializeField]
        private float _delayInPoint = 1f;

        [SerializeField]
        private float _stoppingDistance = 0.1f;

        private Coroutine _moveCoroutine;
        
        [ShowInInspector, ReadOnly]
        private IEnumerator<Vector3> _waypointIterator;

        [Button]
        public void SetPath()
        {            
            _currentPath = _pathPoints.GetPositionPoints();
            _waypointIterator = _currentPath.GetEnumerator();
            _waypointIterator.MoveNext();
            foreach (var item in _currentPath)
            {
                Debug.Log($"{item}");
            }
        }

        [Button]
        public void SetUnit(IEntity unit)
        {
            _moveAgent.SetUnit(unit);
            _moveAgent.SetStoppingDistance(_stoppingDistance);
            _moveAgent.SetTargetPosition(_waypointIterator.Current);
            Debug.Log($"{_waypointIterator.Current}");
        }        

        protected override void OnStart()
        {   
            _moveAgent.Play();
            _moveCoroutine = StartCoroutine(MoveToPoint());
        }

        protected override void OnStop()
        {
            if (_moveAgent.IsPlaying)
            {
                _moveAgent.Stop();
                StopCoroutine(_moveCoroutine);
                _moveCoroutine = null;
            }
        }

        private IEnumerator MoveToPoint()
        {
            var period = new WaitForFixedUpdate();

            while (true)
            { 
                if (_moveAgent.IsPositionReached)
                {
                    yield return new WaitForSeconds(_delayInPoint);
                    NextPosition();
                }

                yield return period;
            }
        }

        private void NextPosition()
        {
            if (_waypointIterator.MoveNext())
            {
                var nextPosition = _waypointIterator.Current;
                _moveAgent.SetTargetPosition(nextPosition);
            }
            else if (!_waypointIterator.MoveNext() && _isLooping)
            {                
                _waypointIterator.Reset();
                SetPath();
                var nextPosition = _waypointIterator.Current;
                _moveAgent.SetTargetPosition(nextPosition);
            }
        }
    }
}