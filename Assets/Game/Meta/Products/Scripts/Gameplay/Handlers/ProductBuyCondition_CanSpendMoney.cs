using Game.GameEngine.Mechanics;
using Game.GameEngine.Products;
using Game.Gameplay.Player;
using UnityEngine;

namespace Game.Meta
{
    public sealed class ProductBuyCondition_CanSpendMoney : IProductBuyCondition 
    {
        private readonly MoneyStorage moneyStorage;

        public ProductBuyCondition_CanSpendMoney(MoneyStorage moneyStorage)
        {
            this.moneyStorage = moneyStorage;
        }

        public bool CanBuy(Product product)
        {
            if (product.TryGetComponent(out IComponent_MoneyPrice component))
            {
                return this.moneyStorage.Money >= component.Price;
            }

            return true;
        }
    }
}