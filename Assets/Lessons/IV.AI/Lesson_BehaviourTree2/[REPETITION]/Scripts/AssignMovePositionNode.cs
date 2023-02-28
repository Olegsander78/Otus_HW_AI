// using System.Collections.Generic;
// using AI.Blackboards;
// using Lessons.AI.Lesson_BehaviourTree1;
// using UnityEngine;
// using Blackboard = Lessons.AI.Architecture2.Blackboard;
//
// namespace Lessons.AI.Lesson_BehaviourTree2
// {
//     public sealed class AssignMovePositionNode : BehaviourNode
//     {
//         [SerializeField]
//         private Blackboard blackboard;
//
//         [BlackboardKey]
//         [SerializeField]
//         private string waypointsKey;
//
//         [BlackboardKey]
//         [SerializeField]
//         private string movePositionKey;
//     
//         protected override void Run()
//         {
//             if (!this.blackboard.TryGetVariable(this.waypointsKey, out IEnumerator<Vector3> waypoints))
//             {
//                 this.Return(false);
//                 return;
//             }
//
//             if (this.blackboard.HasVariable(this.movePositionKey))
//             {
//                 this.blackboard.ChangeVariable(this.movePositionKey, waypoints.Current);
//             }
//             else
//             {
//                 this.blackboard.AddVariable(this.movePositionKey, waypoints.Current);
//             }
//             
//             this.Return(true);
//         }
//     }
// }