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
                    if (player.GetKitchenObject() is PlateKitchenObject)
                    {
                        var plateKitchenObject = player.GetKitchenObject() as PlateKitchenObject;
                        plateKitchenObject.AddIngredient(GetKitchenObject().Data);
                        GetKitchenObject().DestroySelf();
                    }
                }
                else
                    GetKitchenObject().SetAndTeleportToParent(player);
            }
        }
    }
}
