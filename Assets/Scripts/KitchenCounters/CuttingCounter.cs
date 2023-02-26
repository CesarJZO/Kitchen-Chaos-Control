using UnityEngine;

namespace CodeMonkey.KitchenCaosControl.KitchenCounters
{
    public class CuttingCounter : BaseCounter
    {
        public override void Interact(Player player)
        {
            if (HasKitchenObject())
                GetKitchenObject().SetAndTeleportToParent(player);
            else if (player.HasKitchenObject())
                player.GetKitchenObject().SetAndTeleportToParent(this);
        }
    }
}
