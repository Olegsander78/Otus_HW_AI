using GameSystem;
using UnityEngine;

namespace Lessons.Tutorial
{
    public abstract class CompleteObserver : MonoBehaviour,
        IGameConstructElement,
        IGameReadyElement,
        IGameFinishElement
    {
        private TutorialManager tutorialManager;
        
        public void ConstructGame(IGameContext context)
        {
            this.tutorialManager = context.GetService<TutorialManager>();
            this.InitGame(context, this.tutorialManager.IsCompleted);
        }

        public virtual void ReadyGame()
        {
            this.tutorialManager.OnCompleted += this.OnComplete;
        }

        public virtual void FinishGame()
        {
            this.tutorialManager.OnCompleted -= this.OnComplete;
        }
        
        protected virtual void InitGame(IGameContext context, bool isCompleted)
        {
        }

        protected virtual void OnComplete()
        {
        }
    }
}