using GameSystem;
using UnityEngine;
using static GameSystem.GameComponentType;

namespace Game.Gameplay.Player
{
    public sealed class PlayerVendorSystemInstaller : MonoGameInstaller
    {
        [GameComponent(SERVICE)]
        [SerializeField]
        private VendorSaleInteractor vendorInteractor = new();
    }
}