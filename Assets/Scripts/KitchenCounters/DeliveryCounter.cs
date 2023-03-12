namespace CodeMonkey.KitchenCaosControl.KitchenCounters
{
    public class DeliveryCounter : Counter
    {
        public override void Interact(Player player)
        {
            if (!player.HasKitchenObject()) return;

            if (!player.GetKitchenObject().TryGetPlate(out var plateKitchenObject)) return;

            DeliveryManager.Instance.DeliverRecipe(plateKitchenObject);

            player.GetKitchenObject().DestroySelf();
        }
    }
}
