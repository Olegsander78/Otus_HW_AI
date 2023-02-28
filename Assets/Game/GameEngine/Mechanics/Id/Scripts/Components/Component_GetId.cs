using Elementary;

namespace Game.GameEngine.Mechanics
{
    public sealed class Component_GetId : IComponent_GetId
    {
        public string Id
        {
            get { return this.id.Value; }
        }

        private readonly IValue<string> id;

        public Component_GetId(IValue<string> id)
        {
            this.id = id;
        }
    }
}