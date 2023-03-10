using UnityEngine;

namespace Elementary
{
    [AddComponentMenu("Elementary/States/State «Control Particles»")]
    public sealed class MonoState_ControlParticles : MonoState
    {
        [SerializeField]
        private ParticleSystem[] particleSystems;

        public override void Enter()
        {
            for (int i = 0, count = this.particleSystems.Length; i < count; i++)
            {
                var particleSystem = this.particleSystems[i];
                particleSystem.Play(withChildren: true);
            }
        }

        public override void Exit()
        {
            for (int i = 0, count = this.particleSystems.Length; i < count; i++)
            {
                var particleSystem = this.particleSystems[i];
                particleSystem.Stop(withChildren: true);
            }
        }
    }
}