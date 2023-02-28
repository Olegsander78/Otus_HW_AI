using Entities;
using Game.GameEngine;
using Game.GameEngine.Mechanics;
using UnityEngine;

namespace AI.GOAP.Unity
{
    public sealed class StateController_Injured : MonoStateController
    {
        [Space]
        [SerializeField]
        private UnityEntity myUnit;

        [SerializeField]
        private int maxHitPoints = 5;
        
        private UComponent_HitPoints component;

        private void OnEnable()
        {
            this.component = this.myUnit.Get<UComponent_HitPoints>();
            this.component.OnHitPointsChanged += this.OnHitPointsChanged;
        }
        
        private void Start()
        {
            this.worldState.AddParameter(this.stateName, this.IsInjured());
        }

        private void OnDisable()
        {
            this.component.OnHitPointsChanged -= this.OnHitPointsChanged;
        }

        private bool IsInjured()
        {
            return this.component.HitPoints < this.maxHitPoints;
        }

        private void OnHitPointsChanged(int hitPoints)
        {
            this.worldState.ChangeParameter(this.stateName, this.IsInjured());
        }
    }
}