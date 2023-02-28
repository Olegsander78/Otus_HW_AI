using Game.GameEngine;
using GameSystem;
using UnityEngine;

namespace Game.Gameplay.Player
{
    public abstract class VisitingWorldPlaceObserver : MonoBehaviour,
        IGameConstructElement,
        IGameReadyElement,
        IGameFinishElement
    {
        protected VisitingWorldPlaceModel placeModel;

        [SerializeField]
        private WorldPlaceType placeType;

        public virtual void ConstructGame(IGameContext context)
        {
            this.placeModel = context.GetService<VisitingWorldPlaceModel>();
        }

        public virtual void ReadyGame()
        {
            this.placeModel.OnVisitStarted += this.OnVisitStarted;
            this.placeModel.OnVisitEnded += this.OnVisitEnded;
        }

        public virtual void FinishGame()
        {
            this.placeModel.OnVisitStarted -= this.OnVisitStarted;
            this.placeModel.OnVisitEnded -= this.OnVisitEnded;
        }

        private void OnVisitStarted(WorldPlaceType visitPlaceType)
        {
            if (visitPlaceType == this.placeType)
            {
                this.OnStartVisit();
            }
        }

        private void OnVisitEnded(WorldPlaceType visitPlaceType)
        {
            if (visitPlaceType == this.placeType)
            {
                this.OnEndVisit();
            }
        }

        protected virtual void OnStartVisit()
        {
        }

        protected virtual void OnEndVisit()
        {
        }
    }
}