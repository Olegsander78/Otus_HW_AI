using AI.Blackboards;
using AI.GOAP.Unity;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Game.GameEngine
{
    public sealed class PlanningBehaviour : MonoBehaviour, IPlanningBehaviour, ISerializationCallbackReceiver
    {
        [PropertyOrder(-1)]
        [ShowInInspector]
        public bool IsPlaying
        {
            get { return this.behaviour.IsPlaying; }
        }

        [Space]
        [SerializeField]
        private bool autoPlay = true;

        [SerializeField]
        private bool loop = true;

        [Space]
        [SerializeField]
        private BaseGoalPlanner goalPlanner;

        [SerializeField]
        private SequenceComposer actionsComposer;

        [Space]
        [SerializeField]
        private UnityBlackboard blackboard;

        [SerializeField]
        private UnityBlackboard whiteboard;

        private IPlanningBehaviour behaviour;

#if UNITY_EDITOR
        [ContextMenu("Play")]
#endif
        public void Play()
        {
            this.behaviour.Play();
        }

#if UNITY_EDITOR
        [ContextMenu("Stop")]
#endif
        public void Stop()
        {
            this.behaviour.Stop();
        }
        
        private void Start()
        {
            if (this.autoPlay)
            {
                this.behaviour.Play();
            }
        }

        private void Update()
        {
            if (this.loop && !this.behaviour.IsPlaying)
            {
                this.behaviour.Play();
            }
        }

        void ISerializationCallbackReceiver.OnAfterDeserialize()
        {
            this.behaviour = new _PlanningBehaviour(
                this.goalPlanner,
                this.actionsComposer,
                this.blackboard,
                this.whiteboard
            );
        }

        void ISerializationCallbackReceiver.OnBeforeSerialize()
        {
        }
    }
}