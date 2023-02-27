namespace CodeMonkey.KitchenCaosControl.KitchenCounters
{
    public class ClearCounter : Counter
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
