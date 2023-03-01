//using System;
//using System.Collections;
//using System.Collections.Generic;
//using Sirenix.OdinInspector;
//using UnityEngine;

//namespace Lessons.AI.Lesson_Architecture
//{
//    public abstract class Agent_MoveByPath<T> : AgentCoroutine
//    {
//        private const int POINTS_BUFFER = 16;

//        public event Action OnPathFinished;

//        public bool IsPathFinished
//        {
//            get { return _isPathFinished; }
//        }

//        private readonly List<T> _currentPath;

//        private int _pointer;

//        private bool _isPathFinished;

//        public Agent_MoveByPath(
//            MonoBehaviour coroutineDispatcher,
//            YieldInstruction framePeriod = null
//        ) : base(coroutineDispatcher, framePeriod)
//        {
//            _currentPath = new List<T>(POINTS_BUFFER);
//        }

//        public void SetPath(IEnumerable<T> points)
//        {
//            _currentPath.Clear();
//            _currentPath.AddRange(points);

//            _pointer = 0;
//            _isPathFinished = false;
//        }

//        protected override void Update()
//        {
//            if (_isPathFinished)
//            {
//                //return;
//                _pointer = 0;
//            }

//            if (_pointer >= _currentPath.Count)
//            {
//                _isPathFinished = true;
//                OnPathFinished?.Invoke();
//                return;
//            }

//            var targetPoint = _currentPath[_pointer];
//            if (CheckPointReached(targetPoint))
//            {
//                _pointer++;
//            }
//            else
//            {
//                MoveToPoint(targetPoint);
//            }
//        }

//        protected abstract bool CheckPointReached(T point);

//        protected abstract void MoveToPoint(T target);
//    }
//}