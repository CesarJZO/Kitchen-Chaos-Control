using System;
using CodeMonkey.KitchenChaosControl.ScriptableObjects;
using UnityEngine;

namespace CodeMonkey.KitchenChaosControl.KitchenCounters
{
    public class ContainerCounter : Counter
    {
        public event EventHandler OnPlayerGrabbedObject;

        [SerializeField] private KitchenObjectData kitchenObjectData;

        public override void Interact(Player player)
        {
            if (player.HasKitchenObject()) return;
            KitchenObject.SpawnKitchenObject(kitchenObjectData, player);

            OnPlayerGrabbedObject?.Invoke(this, EventArgs.Empty);
        }
    }
}
