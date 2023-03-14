namespace CodeMonkey.KitchenCaosControl.KitchenCounters
{
    public class DeliveryCounter : Counter
    {
        public static DeliveryCounter Instance { get; private set; }

        private void Awake()
        {
            Instance = this;
        }

        public override void Interact(Player player)
        {
            if (!player.HasKitchenObject()) return;

            if (!player.GetKitchenObject().TryGetPlate(out var plateKitchenObject)) return;

            DeliveryManager.Instance.DeliverRecipe(plateKitchenObject);

            player.GetKitchenObject().DestroySelf();
        }
    }
}
