namespace Game.GameEngine.AI
{
    public interface IComponent_ExecuteCommand
    {
        bool IsRunning { get; }
        
        void Execute(CommandType type, object args = null);

        void ExecuteForce(CommandType type, object args = null);

        void Cancel();
    }
}