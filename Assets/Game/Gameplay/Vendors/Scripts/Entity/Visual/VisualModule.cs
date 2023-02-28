using MonoOptimization;
using UnityEngine;

namespace Game.Gameplay.Vendors
{
    [AddComponentMenu("Gameplay/Vendors/Vendor Visual Module")]
    public sealed class VisualModule : MonoModule
    {
        [SerializeField]
        private Animator vendorAnimator;

        [Space]
        [SerializeField]
        public VendorVisual vendorVisual = new();

        public override void ConstructSensor(MonoContextModular context)
        {
            this.vendorVisual.Animator = this.vendorAnimator;
        }
    }
}