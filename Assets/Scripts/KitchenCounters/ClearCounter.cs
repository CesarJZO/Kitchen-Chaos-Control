namespace CodeMonkey.KitchenCaosControl.KitchenCounters
{
    public class ClearCounter : Counter
    {
        public override void Interact(Player player)
        {
            if (!HasKitchenObject())
            {
                if (player.HasKitchenObject())
                    player.GetKitchenObject().SetAndTeleportToParent(this);
            }
            else
            {
                if (player.HasKitchenObject())
                {
                    if (player.GetKitchenObject().TryGetPlate(out var plateKitchenObject))
                    {
                        if (plateKitchenObject.TryAddIngredient(GetKitchenObject().Data))
                            GetKitchenObject().DestroySelf();
                    }
                    else
                    {
                        if (GetKitchenObject().TryGetPlate(out plateKitchenObject))
                        {
                            if (plateKitchenObject.TryAddIngredient(player.GetKitchenObject().Data))
                                player.GetKitchenObject().DestroySelf();
                        }
                    }
                }
                else
                    GetKitchenObject().SetAndTeleportToParent(player);
            }
        }
    }
}
