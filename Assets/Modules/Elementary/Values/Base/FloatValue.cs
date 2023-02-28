using System;
using UnityEngine;

namespace Elementary
{
    [Serializable]
    public sealed class FloatValue : IValue<float>
    {
        public float Value
        {
            get { return this.value; }
        }

        [SerializeField]
        private float value;

        public FloatValue(float value)
        {
            this.value = value;
        }
    }
}