using CodeMonkey.KitchenCaosControl.ScriptableObjects;
using UnityEngine;

namespace CodeMonkey.KitchenCaosControl.KitchenCounters
{
    public class ContainerCounter : BaseCounter, IKitchenObjectParent
    {
        [SerializeField] private KitchenScriptableObject kitchenScriptableObject;
        [SerializeField] private Transform counterTopPoint;

        private KitchenObjectBehaviour _currentKitchenObject;

        public override void Interact(Player player)
        {
            // If there is no current kitchen object, add one
            if (!_currentKitchenObject)
            {
                var kitchenObject = Instantiate(kitchenScriptableObject.Prefab, counterTopPoint);
                /* There's no need to set the current kitchen object's clear counter to this, because the kitchen object's
                clear counter will be set by the kitchen object */
                kitchenObject.SetAndTeleportToParent(this);
            }
            else
            {
                _currentKitchenObject.SetAndTeleportToParent(player);
            }
        }

        public Transform GetParentFollowPoint() => counterTopPoint;

        public void SetKitchenObject(KitchenObjectBehaviour kitchenObject) => _currentKitchenObject = kitchenObject;

        public KitchenObjectBehaviour GetKitchenObject() => _currentKitchenObject;

        public void ClearKitchenObject() => _currentKitchenObject = null;

        public bool HasKitchenObject() => _currentKitchenObject;
    }
}
