using System.Collections.Generic;
using UnityEngine;

namespace Game.GameEngine.AI
{
    public sealed class CommandExecutorQueue : CommandExecutor
    {
        private readonly Queue<Command> tasks = new();

        public void Enqueue(CommandType type, object args)
        {
            if (this.IsRunning)
            {
                this.tasks.Enqueue(new Command(type, args));
            }
            else
            {
                this.Execute(type, args);
            }
        }

        protected override void OnCancel()
        {
            this.tasks.Clear();
        }

        private void Update()
        {
            if (this.IsRunning)
            {
                return;
            }

            if (this.tasks.Count <= 0)
            {
                return;
            }

            var task = this.tasks.Dequeue();
            this.Execute(task.type, task.args);
        }
    }
}