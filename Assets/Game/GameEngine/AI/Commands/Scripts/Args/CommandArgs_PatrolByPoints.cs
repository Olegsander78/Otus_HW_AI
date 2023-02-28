using UnityEngine;

namespace Game.GameEngine.AI
{
    public sealed class CommandArgs_PatrolByPoints
    {
        public readonly Vector3[] patrolPoints;

        public CommandArgs_PatrolByPoints(Vector3[] patrolPoints)
        {
            this.patrolPoints = patrolPoints;
        }
    }
}