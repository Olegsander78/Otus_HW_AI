using System;
using AI.BTree;
using AI.GOAP;
using AI.GOAP.Unity;
using UnityEngine;

namespace Game.GameEngine
{
    public sealed class SequenceComposer : MonoBehaviour, ISequenceComposer<UnityBehaviourNode>
    {
        [SerializeField]
        private NodeInfo[] nodes = new NodeInfo[0];

        public UnityBehaviourNode[] ComposeSequence(IAction[] sequence)
        {
            var count = sequence.Length;
            var result = new UnityBehaviourNode[count];
            for (var i = 0; i < count; i++)
            {
                var action = sequence[i];
                result[i] = this.FindNode(action.Name);
            }

            return result;
        }

        private UnityBehaviourNode FindNode(string actionName)
        {
            for (int i = 0, count = this.nodes.Length; i < count; i++)
            {
                var nodeInfo = this.nodes[i];
                if (nodeInfo.planAction.Name == actionName)
                {
                    return nodeInfo.node;
                }
            }

            throw new Exception($"Action {actionName} is not found!");
        }

        [Serializable]
        private sealed class NodeInfo
        {
            [Space(12)]
            [SerializeField]
            public BaseAction planAction;

            [SerializeField]
            public UnityBehaviourNode node;
        }
    }
}