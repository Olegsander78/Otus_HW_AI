using Entities;
using UnityEngine;

namespace Lessons.Tutorial
{
    public sealed class Provider_TargetEnemy : MonoBehaviour
    {
        public UnityEntity TargetEnemy
        {
            get { return this.targetEnemy; }
        }

        [SerializeField]
        private UnityEntity targetEnemy;
    }
}