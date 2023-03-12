namespace CodeMonkey.KitchenCaosControl.KitchenCounters
{
    public class DeliveryCounter : Counter
    {
        public override void Interact(Player player)
        {
            if (!player.HasKitchenObject()) return;

            if (player.GetKitchenObject().TryGetPlate(out _))
                player.GetKitchenObject().DestroySelf();
        }
    }
}
