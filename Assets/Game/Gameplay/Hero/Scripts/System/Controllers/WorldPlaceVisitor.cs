using System;
using Elementary;
using Game.GameEngine;
using Game.GameEngine.Mechanics;
using Game.Gameplay.Player;
using GameSystem;
using UnityEngine;

namespace Game.Gameplay.Hero
{
    [Serializable]
    public sealed class WorldPlaceVisitor : TriggerVisitor<WorldPlaceTrigger>
    {
        [Space]
        [SerializeField]
        private float visitDelay = 0.2f;

        private VisitingWorldPlaceModel placeModel;

        [GameInject]
        public void Construct(VisitingWorldPlaceModel placeModel)
        {
            this.placeModel = placeModel;
        }

        protected override bool IsTargetEntered(WorldPlaceTrigger entity)
        {
            return true;
        }

        protected override ICondition ProvideVisitCondition(WorldPlaceTrigger target)
        {
            return new ConditionComposite(
                ConditionComposite.Mode.AND,
                new ConditionCountdown(this.monoContext, seconds: this.visitDelay, startInstantly: true),
                new Condition_Entity_IsNotMoving(this.HeroService.GetHero())
            );
        }

        protected override void OnHeroVisit(WorldPlaceTrigger target)
        {
            var placeType = target.PlaceType;
            if (this.placeModel.IsVisiting && this.placeModel.CurrentPlace != placeType)
            {
                this.placeModel.EndVisit();
            }

            this.placeModel.StartVisit(placeType);
        }

        protected override void OnHeroQuit(WorldPlaceTrigger target)
        {
            var placeType = target.PlaceType;
            if (this.placeModel.IsVisiting && placeType == target.PlaceType)
            {
                this.placeModel.EndVisit();
            }
        }
    }
}