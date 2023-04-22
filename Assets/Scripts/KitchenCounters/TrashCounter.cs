using System;

namespace CodeMonkey.KitchenChaosControl.KitchenCounters
{
    public class TrashCounter : Counter
    {
        public static event EventHandler OnAnyObjectTrashed;

        public new static void ResetStaticData()
        {
            OnAnyObjectTrashed = null;
        }

        public override void Interact(Player player)
        {
            if (!player.HasKitchenObject()) return;
            player.GetKitchenObject().DestroySelf();
            OnAnyObjectTrashed?.Invoke(this, EventArgs.Empty);
        }
    }
}
