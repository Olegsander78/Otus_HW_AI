using System;
using UnityEngine;

namespace Elementary
{
    [Serializable]
    public sealed class BoolValue : IValue<bool>
    {
        public bool Value
        {
            get { return this.value; }
        }

        [SerializeField]
        private bool value;

        public BoolValue(bool value)
        {
            this.value = value;
        }
    }
}