using System;
using AI.Blackboards;
using Entities;
using UnityEngine;

namespace Lessons.AI.LessonGOAP
{
    public sealed class BlackboardInstaller : MonoBehaviour
    {
        [SerializeField]
        private UnityBlackboard blackboard;
    
        [BlackboardKey]
        [SerializeField]
        private string unitKey;

        [SerializeField]
        private UnityEntity unit;

        [Space]
        [BlackboardKey]
        [SerializeField]
        private string healingKey;

        [SerializeField]
        private Transform healingPoint;

        private void Awake()
        {
            this.blackboard.AddVariable(this.unitKey, this.unit);
            this.blackboard.AddVariable(this.healingKey, this.healingPoint);
        }
    }
}