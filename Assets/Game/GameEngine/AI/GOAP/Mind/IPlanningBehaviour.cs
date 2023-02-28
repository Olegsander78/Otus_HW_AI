namespace Game.GameEngine
{
    public interface IPlanningBehaviour
    {
        bool IsPlaying { get; }
        
        void Play();

        void Stop();
    }
}