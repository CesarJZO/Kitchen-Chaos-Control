using CodeMonkey.KitchenCaosControl.ScriptableObjects;
using UnityEngine;

namespace CodeMonkey.KitchenCaosControl.KitchenCounters
{
    public class CuttingCounter : BaseCounter
    {
        [SerializeField] private KitchenObjectData cutKitchenObjectData;

        public override void Interact(Player player)
        {
            if (HasKitchenObject())
                GetKitchenObject().SetAndTeleportToParent(player);
            else if (player.HasKitchenObject())
                player.GetKitchenObject().SetAndTeleportToParent(this);
        }

        public override void InteractAlternate(Player player)
        {
            if (!HasKitchenObject()) return;

            GetKitchenObject().DestroySelf();
            KitchenObject.SpawnKitchenObject(cutKitchenObjectData, this);
        }
    }
}
