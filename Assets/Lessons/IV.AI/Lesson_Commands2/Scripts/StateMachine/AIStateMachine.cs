using Elementary;
using Game.GameEngine.Mechanics;
using UnityEngine;

namespace Lessons.AI.Lesson_Commands2
{
    public sealed class AIStateMachine : MonoStateMachine<AIStateType>
    {
        [SerializeField]
        private UMeleeCombatEngine meleeCombatEngine;

        [SerializeField]
        private UMoveToPositionEngine moveToPositionEngine;

        [SerializeField]
        private UPatrolByPointsEngine patrolByPointsEngine;

        public override void Enter()
        {
            base.Enter();

            this.meleeCombatEngine.OnCombatStarted += this.OnCombatStarted;
            this.meleeCombatEngine.OnCombatStopped += this.OnCombatStopped;

            this.moveToPositionEngine.OnMoveStarted += this.OnMoveToPositionStarted;
            this.moveToPositionEngine.OnMoveStopped += this.OnMoveToPositionStopped;

            this.patrolByPointsEngine.OnPatrolStarted += this.OnPatrolByPointsStarted;
            this.patrolByPointsEngine.OnPatrolStopped += this.OnPatrolByPointsStopped;
        }

        public override void Exit()
        {
            this.meleeCombatEngine.OnCombatStarted -= this.OnCombatStarted;
            this.meleeCombatEngine.OnCombatStopped -= this.OnCombatStopped;

            this.moveToPositionEngine.OnMoveStarted -= this.OnMoveToPositionStarted;
            this.moveToPositionEngine.OnMoveStopped -= this.OnMoveToPositionStopped;

            this.patrolByPointsEngine.OnPatrolStarted -= this.OnPatrolByPointsStarted;
            this.patrolByPointsEngine.OnPatrolStopped -= this.OnPatrolByPointsStopped;
        
            base.Exit();
        }

        private void OnCombatStarted(MeleeCombatOperation operation)
        {
            this.SwitchState(AIStateType.ATTACK_TARGET);
        }

        private void OnCombatStopped(MeleeCombatOperation operation)
        {
            if (this.CurrentState == AIStateType.ATTACK_TARGET)
            {
                this.SwitchState(AIStateType.IDLE);
            }
        }

        private void OnMoveToPositionStarted(Vector3 position)
        {
            this.SwitchState(AIStateType.MOVE_TO_POSITION);
        }

        private void OnMoveToPositionStopped(Vector3 position)
        {
            if (this.CurrentState == AIStateType.MOVE_TO_POSITION)
            {
                this.SwitchState(AIStateType.IDLE);
            }
        }

        private void OnPatrolByPointsStarted(PatrolByPointsOperation operation)
        {
            this.SwitchState(AIStateType.PATROL_BY_POINTS);
        }

        private void OnPatrolByPointsStopped(PatrolByPointsOperation operation)
        {
            if (this.CurrentState == AIStateType.PATROL_BY_POINTS)
            {
                this.SwitchState(AIStateType.IDLE);
            }
        }
    }
}