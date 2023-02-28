using Game.Gameplay.Player;
using GameSystem;

namespace Game.Gameplay.Hero
{
    public sealed class ConveyorVisitObserver : AbstractTriggerObserver<ConveyorTrigger>
    {
        private VisitingConveyorService conveyorService;

        [GameInject]
        public void Construct(VisitingConveyorService conveyorService)
        {
            this.conveyorService = conveyorService;
        }

        protected override void OnHeroEntered(ConveyorTrigger target)
        {
            var zoneType = target.Zone;
            if (zoneType == ConveyorTrigger.ZoneType.LOAD)
            {
                this.EnterLoadZone(target);
            }

            if (zoneType == ConveyorTrigger.ZoneType.UNLOAD)
            {
                this.EnterUnloadZone(target);
            }
        }

        protected override void OnHeroExited(ConveyorTrigger target)
        {
            var zoneType = target.Zone;
            if (zoneType == ConveyorTrigger.ZoneType.LOAD)
            {
                this.conveyorService.InputZone.Exit();
            }

            if (zoneType == ConveyorTrigger.ZoneType.UNLOAD)
            {
                this.conveyorService.OutputZone.Exit();
            }
        }

        private void EnterLoadZone(ConveyorTrigger trigger)
        {
            var inputZone = this.conveyorService.InputZone;
            if (inputZone.IsEntered)
            {
                inputZone.Exit();
            }
            
            inputZone.Enter(trigger.Conveyor);
        }

        private void EnterUnloadZone(ConveyorTrigger trigger)
        {
            var outputZone = this.conveyorService.OutputZone;
            if (outputZone.IsEntered)
            {
                outputZone.Exit();
            }
            
            outputZone.Enter(trigger.Conveyor);
        }
    }
}