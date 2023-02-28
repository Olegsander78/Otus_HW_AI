using System;
using Elementary;
using Entities;
using Game.Gameplay.Player;
using GameSystem;
using UnityEngine;
using ICondition = Elementary.ICondition;

namespace Game.Gameplay.Hero
{
    [Serializable]
    public sealed class VendorVisitObserver : TriggerVisitor<IEntity>
    {
        private VendorSaleInteractor vendorInteractor;

        [SerializeField]
        private ScriptableEntityCondition vendorCondition;

        private readonly ICondition stayCondition;

        public VendorVisitObserver()
        {
            this.stayCondition = new Condition(true);
        }

        [GameInject]
        public void Construct(VendorSaleInteractor vendorInteractor)
        {
            this.vendorInteractor = vendorInteractor;
        }

        protected override bool IsTargetEntered(IEntity target)
        {
            return this.vendorCondition.IsTrue(target);
        }

        protected override ICondition ProvideVisitCondition(IEntity target)
        {
            return this.stayCondition;
        }

        protected override void OnHeroVisit(IEntity entity)
        {
            this.vendorInteractor.SaleResources(entity);
        }
    }
}