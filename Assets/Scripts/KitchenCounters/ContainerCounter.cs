using System;
using CodeMonkey.KitchenCaosControl.ScriptableObjects;
using UnityEngine;

namespace CodeMonkey.KitchenCaosControl.KitchenCounters
{
    public class ContainerCounter : BaseCounter
    {
        public event EventHandler OnPlayerGrabbedObject;

        [SerializeField] private KitchenScriptableObject kitchenScriptableObject;

        public override void Interact(Player player)
        {
            if (player.HasKitchenObject()) return;
            KitchenObject.SpawnKitchenObject(kitchenScriptableObject, player);

            OnPlayerGrabbedObject?.Invoke(this, EventArgs.Empty);
        }
    }
}
