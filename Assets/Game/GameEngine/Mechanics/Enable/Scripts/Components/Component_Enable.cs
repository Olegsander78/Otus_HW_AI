using System;
using Elementary;

namespace Game.GameEngine.Mechanics
{
    public sealed class Component_Enable : IComponent_Enable
    {
        public event Action<bool> OnEnabled
        {
            add { this.isEnable.OnValueChanged += value; }
            remove { this.isEnable.OnValueChanged -= value; }
        }

        public bool IsEnable
        {
            get { return this.isEnable.Value; }
        }

        private readonly IVariable<bool> isEnable;

        public Component_Enable(IVariable<bool> isEnable)
        {
            this.isEnable = isEnable;
        }

        public void SetEnable(bool isEnable)
        {
            this.isEnable.Value = isEnable;
        }
    }
}