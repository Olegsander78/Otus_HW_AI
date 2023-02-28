using Entities;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Game.Gameplay.Hero
{
    public sealed class HeroService : IHeroService
    {
        [PropertySpace]
        [ReadOnly]
        [ShowInInspector]
        private IEntity currentHero;

        public void SetupHero(IEntity hero)
        {
            this.currentHero = hero;
        }

        public IEntity GetHero()
        {
            return this.currentHero;
        }
    }
}