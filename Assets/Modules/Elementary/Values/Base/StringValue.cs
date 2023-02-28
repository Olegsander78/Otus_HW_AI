using System;
using UnityEngine;

namespace Elementary
{
    [Serializable]
    public sealed class StringValue : IValue<string>
    {
        public string Value
        {
            get { return this.value; }
        }

        [SerializeField]
        private string value;

        public StringValue(string value)
        {
            this.value = value;
        }
    }
}