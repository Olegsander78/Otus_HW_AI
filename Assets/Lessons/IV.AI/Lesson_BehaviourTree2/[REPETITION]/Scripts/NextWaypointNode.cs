// using System.Collections.Generic;
// using AI.Blackboards;
// using Lessons.AI.Lesson_BehaviourTree1;
// using UnityEngine;
// using Blackboard = Lessons.AI.Architecture2.Blackboard;
//
// namespace Lessons.AI.Lesson_BehaviourTree2
// {
//     public sealed class NextWaypointNode : BehaviourNode
//     {
//         [SerializeField]
//         private Blackboard blackboard;
//
//         [BlackboardKey]
//         [SerializeField]
//         private string waypointsIteratorKey;
//     
//         protected override void Run()
//         {
//             if (!this.blackboard.TryGetVariable(this.waypointsIteratorKey, out IEnumerator<Vector3> iterator))
//             {
//                 this.Return(false);
//                 return;
//             }
//
//             iterator.MoveNext();
//             this.Return(true);
//         }
//     }
// }