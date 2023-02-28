namespace AI.GOAP.Unity
{
    public sealed class StateController_SeeEnemy : MonoStateController
    {
        public void StartObserve()
        {
            this.worldState.AddParameter(this.stateName, true);
        }

        public void StopObserve()
        {
            this.worldState.RemoveParameter(this.stateName);
        }
    }
}