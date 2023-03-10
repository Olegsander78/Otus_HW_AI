using System;
using System.Collections;
using Entities;
using Game.GameEngine.Mechanics;
using UnityEngine;

namespace Lessons.AI.LessonGOAP
{
    public sealed class RangeCombatComponent : MonoBehaviour, IComponent_RangeCombat
    {
        public event Action<RangeCombatOperation> OnCombatStarted;

        public event Action<RangeCombatOperation> OnCombatStopped;

        public bool IsCombat
        {
            get { return this.operation != null; }
        }

        private RangeCombatOperation operation;
        
        [SerializeField]
        private UnityEntity unit;
        
        [SerializeField]
        private int ammo = 10;

        private void Awake()
        {
            this.unit.Add(this);
        }

        public bool CanStartCombat(RangeCombatOperation operation)
        {
            return true;
        }

        public void StartCombat(RangeCombatOperation operation)
        {
            this.operation = operation;
            this.OnCombatStarted?.Invoke(operation);
            
            this.StartCoroutine(this.PewRoutine());
        }

        public void StopCombat()
        {
            this.StopCoroutine(nameof(this.PewRoutine));
            var operation = this.operation;
            this.operation = null;
            this.OnCombatStopped?.Invoke(operation);
        }

        private IEnumerator PewRoutine()
        {
            for (var i = 0; i < 5; i++)
            {
                Debug.Log($"Pew {this.ammo}");
                yield return new WaitForSeconds(2);
            }
            
            this.StopCombat();
        }
    }
}