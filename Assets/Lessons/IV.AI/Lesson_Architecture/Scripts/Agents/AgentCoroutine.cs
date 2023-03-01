//using System;
//using System.Collections;
//using Sirenix.OdinInspector;
//using UnityEngine;

//namespace Lessons.AI.Lesson_Architecture
//{
//    public abstract class AgentCoroutine : Agent
//    {
//        //private readonly MonoBehaviour _coroutineDispatcher;

//        private YieldInstruction _framePeriod;

//        [SerializeField]
//        private float _delayInPoint = 0;

//        private Coroutine _coroutine;

//        //public AgentCoroutine(MonoBehaviour coroutineDispatcher, YieldInstruction framePeriod = null)
//        //{
//        //    _coroutineDispatcher = coroutineDispatcher;
//        //    _framePeriod = framePeriod;
//        //}

//        protected override void OnStart()
//        {
//            _coroutine = StartCoroutine(LoopCoroutine());
//            //_coroutine = _coroutineDispatcher.StartCoroutine(LoopCoroutine());
//        }

//        protected override void OnStop()
//        {
//            if (_coroutine != null)
//            {
//                StopCoroutine(_coroutine);
//                //_coroutineDispatcher.StopCoroutine(_coroutine);
//                _coroutine = null;
//            }
//        }

//        private IEnumerator LoopCoroutine()
//        {
//            while (true)
//            {
//                yield return _framePeriod;
//                Update();
//            }
//        }

//        protected abstract void Update();
//    }
//}