using System.Linq;
using Entities;
using Sirenix.OdinInspector;
using UnityEngine;
using static Game.GameEngine.AI.CommandType;

namespace Game.GameEngine.AI
{
    public sealed class CommandExecutorDebug : MonoBehaviour
    {
        [SerializeField]
        private CommandExecutor commandExecutor;

        [Title("Methods")]
        [Button]
        [GUIColor(0, 1, 0)]
        private void MoveToPosition(Transform point)
        {
            this.commandExecutor.Execute(MOVE_TO_POSITION, new CommandArgs_MoveToPosition(point.position));
        }

        [Button]
        [GUIColor(0, 1, 0)]
        private void AttackTarget(UnityEntity target)
        {
            this.commandExecutor.Execute(ATTACK_TARGET, new CommandArgs_AttackTarget(target));
        }

        [Button]
        [GUIColor(0, 1, 0)]
        private void HarvestTarget(UnityEntity target)
        {
            this.commandExecutor.Execute(HARVEST_RESOURCE, new CommandArgs_HarvestTarget(target));
        }

        [Button]
        [GUIColor(0, 1, 0)]
        private void PatrolByPoints(Transform[] points)
        {
            var positions = points.Select(it => it.position).ToArray();
            this.commandExecutor.Execute(PATROL_BY_POINTS, new CommandArgs_PatrolByPoints(positions));
        }

        [Button]
        [GUIColor(0, 1, 0)]
        private void Cancel()
        {
            if (this.commandExecutor.IsRunning)
            {
                this.commandExecutor.Cancel();
            }
        }
    }
}