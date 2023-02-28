using Sirenix.OdinInspector;
using UnityEngine;

namespace Game.GameEngine.AI
{
    public sealed class Component_ExecuteCommand : MonoBehaviour, IComponent_ExecuteCommand
    {
        public bool IsRunning
        {
            get { return this.executor; }
        }

        [SerializeField]
        private CommandExecutor executor;

        [Button]
        public void Execute(CommandType type, object args = null)
        {
            this.executor.Execute(type, args);
        }

        [Button]
        public void ExecuteForce(CommandType type, object args = null)
        {
            this.executor.ExecuteForce(type, args);
        }

        public void Cancel()
        {
            this.executor.Cancel();
        }
    }
}