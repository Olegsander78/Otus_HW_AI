using UnityEngine;

namespace Game.GameEngine.AI
{
    public abstract class CommandProcessor : MonoBehaviour
    {
        public bool IsRunning
        {
            get { return this.isRunning; }
        }

        private bool isRunning;
        
        private ICallback currentCallback;

        public void Execute(CommandType type, object args, ICallback callback)
        {
            if (this.isRunning)
            {
                Debug.LogWarning("Processor is already running!");
                return;
            }
            
            this.isRunning = true;
            this.currentCallback = callback;
            this.Execute(type, args);
        }

        public void Cancel()
        {
            if (!this.isRunning)
            {
                return;
            }

            this.isRunning = false;
            this.currentCallback = null;
            this.OnCancel();
            this.End();
        }

        protected abstract void Execute(CommandType type, object args);

        protected void Complete()
        {
            if (!this.isRunning)
            {
                return;
            }

            this.isRunning = false;
            this.OnComplete();
            this.End();
            this.InvokeCallback();
        }
        
        protected virtual void OnComplete()
        {
        }

        protected virtual void OnCancel()
        {
        }

        protected virtual void End()
        {
        }

        private void InvokeCallback()
        {
            if (this.currentCallback == null)
            {
                return;
            }

            var callback = this.currentCallback;
            this.currentCallback = null;
            callback.Invoke(this);
        }

        public interface ICallback
        {
            void Invoke(CommandProcessor processor);
        }
    }
}