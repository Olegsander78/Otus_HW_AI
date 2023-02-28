using Entities;
using Game.GameEngine.GameResources;

namespace Game.Gameplay.Player
{
    public struct VendorSaleResult
    {
        public IEntity vendor;
        public ResourceType resourceType;
        public int resourceAmount;
        public int moneyIncome;
    }
}