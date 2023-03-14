using System;

namespace CodeMonkey.KitchenCaosControl.KitchenCounters
{
    public class TrashCounter : Counter
    {
        public static event EventHandler OnAnyObjectTrashed;

        public override void Interact(Player player)
        {
            if (!player.HasKitchenObject()) return;
            player.GetKitchenObject().DestroySelf();
            OnAnyObjectTrashed?.Invoke(this, EventArgs.Empty);
        }
    }
}
