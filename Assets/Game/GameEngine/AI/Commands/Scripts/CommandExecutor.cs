using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Game.GameEngine.AI
{
    public class CommandExecutor : MonoBehaviour, CommandProcessor.ICallback
    {
        public event Action<Command> OnStarted;

        public event Action<Command> OnFinished;

        public event Action<Command> OnCanceled;

        [ShowInInspector, ReadOnly]
        public bool IsRunning
        {
            get { return this.isRunning; }
        }

        [ShowInInspector, ReadOnly]
        public Command CurrentCommand
        {
            get { return this.currentCommand; }
        }

        [SerializeField]
        private ProcessorInfo[] processors = new ProcessorInfo[0];

        private bool isRunning;

        private Command currentCommand;

        private CommandProcessor currentProcessor;

        [Button]
        public void Execute(CommandType type, object args = null)
        {
            if (this.isRunning)
            {
                Debug.LogWarning("Other command is already run!");
                return;
            }

            if (!this.TryGetProcessor(type, out this.currentProcessor))
            {
                return;
            }

            this.isRunning = true;
            this.currentCommand = new Command(type, args);
            this.OnStarted?.Invoke(this.currentCommand);
            this.currentProcessor.Execute(type, args, callback: this);
        }

        [Button]
        public void ExecuteForce(CommandType type, object args = null)
        {
            if (this.isRunning)
            {
                this.currentProcessor.Cancel();
                this.currentProcessor = null;
            }

            if (!this.TryGetProcessor(type, out this.currentProcessor))
            {
                return;
            }

            this.isRunning = true;
            this.currentCommand = new Command(type, args);
            this.OnStarted?.Invoke(this.currentCommand);
            this.currentProcessor.Execute(type, args, this);
        }

        [Button]
        public void Cancel()
        {
            if (!this.isRunning)
            {
                return;
            }

            this.OnCancel();

            var command = this.currentCommand;
            this.currentCommand = default;

            this.currentProcessor.Cancel();
            this.currentProcessor = null;
            this.OnCanceled?.Invoke(command);
        }

        protected virtual void OnCancel()
        {
        }

        void CommandProcessor.ICallback.Invoke(CommandProcessor processor)
        {
            var command = this.currentCommand;
            this.currentCommand = default;

            this.currentProcessor = null;
            this.OnFinished?.Invoke(command);
        }

        private bool TryGetProcessor(CommandType type, out CommandProcessor processor)
        {
            for (int i = 0, count = this.processors.Length; i < count; i++)
            {
                var info = this.processors[i];
                if (info.type == type)
                {
                    processor = info.processor;
                    return true;
                }
            }

            processor = default;
            return false;
        }

        public readonly struct Command
        {
            public readonly CommandType type;

            public readonly object args;

            public Command(CommandType type, object args)
            {
                this.type = type;
                this.args = args;
            }
        }

        [Serializable]
        private sealed class ProcessorInfo
        {
            [SerializeField]
            public CommandType type;

            [SerializeField]
            public CommandProcessor processor;
        }
    }
}