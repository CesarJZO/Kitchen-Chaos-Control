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
            var kitchenObject = Instantiate(kitchenScriptableObject.Prefab);
            kitchenObject.SetAndTeleportToParent(player);
            OnPlayerGrabbedObject?.Invoke(this, EventArgs.Empty);
        }
    }
}
