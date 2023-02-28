using AI.GOAP;

namespace Game.GameEngine
{
    public interface ISequenceComposer<out T>
    {
        T[] ComposeSequence(IAction[] sequence);
    }
}