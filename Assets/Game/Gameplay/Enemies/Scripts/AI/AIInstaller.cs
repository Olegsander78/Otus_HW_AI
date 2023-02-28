using AI.Blackboards;
using AI.Iterators;
using AI.Waypoints;
using Elementary;
using Entities;
using Game.GameEngine;
using Game.GameEngine.Mechanics;
using GameSystem;
using MonoOptimization;
using Polygons;
using UnityEngine;

namespace Game.Gameplay.Enemies
{
    [AddComponentMenu("Gameplay/Enemies/Enemy AI Installer")]
    public sealed class AIInstaller : MonoGameInstaller
    {
        [SerializeField]
        private MonoContextModular ai;
        
        [Header("References")]
        [SerializeField]
        private UnityEntity unit;
        
        [Space]
        [SerializeField]
        private WaypointsPath waypointsPath;

        [SerializeField]
        private IteratorMode waypointMode = IteratorMode.CIRCLE;

        [Space]
        [SerializeField]
        private MonoPolygon surfacePolygon;

        public override void ConstructGame(IGameContext context)
        {
            var blackboard = this.ai.GetModule<AIBlackboardModule>().blackboard;
            var blackboardKeys = this.ai.GetModule<AIConfigModule>().aiConfig.blackboardKeys;
            var sensor = this.ai.GetModule<AISensorModule>().sensor;
            
            this.InstallUnit(blackboard, blackboardKeys);
            this.InstallWaypoints(blackboard, blackboardKeys);
            this.InstallSurface(blackboard, blackboardKeys);
            this.InstallSensor(sensor);
        }

        private void InstallUnit(Blackboard blackboard, ScriptableEnemyAI.BlackboardKeys blackboardKeys)
        {
            blackboard.AddVariable(blackboardKeys.unitKey, this.unit);
        }

        private void InstallWaypoints(Blackboard blackboard, ScriptableEnemyAI.BlackboardKeys blackboardKeys)
        {
            var waypoints = this.waypointsPath
                .GetPositionPoints()
                .ToArray();

            var iterator = IteratorFactory.CreateIterator(this.waypointMode, waypoints);
            blackboard.AddVariable(blackboardKeys.patrolIteratorKey, iterator);
        }

        private void InstallSurface(Blackboard blackboard, ScriptableEnemyAI.BlackboardKeys blackboardKeys)
        {
            var key = blackboardKeys.surfaceKey;
            blackboard.AddVariable(key, this.surfacePolygon);
        }

        private void InstallSensor(CollidersSensorOverlapSphere sensor)
        {
            var centerPoint = this.unit.Get<IComponent_GetPivot>().Pivot;
            sensor.SetCenterPoint(centerPoint);
        }
    }
}