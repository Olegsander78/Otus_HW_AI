using System;
using Elementary;

namespace Game.GameEngine.Mechanics
{
    [Serializable]
    public sealed class TakeDamageEngine : Emitter<TakeDamageArgs>
    {
        private IHitPointsEngine hitPointsEngine;

        private IEmitter<DestroyArgs> destroyReceiver;

        public void Construct(IHitPointsEngine hitPointsEngine, IEmitter<DestroyArgs> destroyReceiver)
        {
            this.hitPointsEngine = hitPointsEngine;
            this.destroyReceiver = destroyReceiver;
        }
        
        public override void Call(TakeDamageArgs damageArgs)
        {
            if (this.hitPointsEngine.CurrentHitPoints <= 0)
            {
                return;
            }

            this.hitPointsEngine.CurrentHitPoints -= damageArgs.damage;
            base.Call(damageArgs);

            if (this.hitPointsEngine.CurrentHitPoints <= 0)
            {
                var destroyEvent = CommonUtils.ComposeDestroyEvent(damageArgs);
                this.destroyReceiver.Call(destroyEvent);
            }
        }
    }
}