using UnityEngine;

namespace AI.GOAP
{
    public abstract class MonoStateController : MonoBehaviour, IWorldStateInjective
    {
        [ParameterKey]
        [SerializeField]
        protected string stateName;

        protected IWorldState worldState { get; private set; }

        IWorldState IWorldStateInjective.WorldState
        {
            set { this.InjectWorldState(value); }
        }

        protected virtual void InjectWorldState(IWorldState worldState)
        {
            this.worldState = worldState;
        }
    }
}