using CodeMonkey.KitchenCaosControl.ScriptableObjects;
using UnityEngine;

namespace CodeMonkey.KitchenCaosControl
{
    public class ClearCounter : MonoBehaviour, IKitchenObjectParent
    {
        [SerializeField] private KitchenScriptableObject kitchenScriptableObject;
        [SerializeField] private Transform counterTopPoint;

        private KitchenObjectBehaviour _currentKitchenObject;

        public void Interact(Player player)
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

        public bool HasKitchenObject() => _currentKitchenObject;

        public Transform GetFollowParentFollowPoint() => counterTopPoint;

        public void SetKitchenObject(KitchenObjectBehaviour kitchenObject) => _currentKitchenObject = kitchenObject;

        public KitchenObjectBehaviour GetKitchenObject() => _currentKitchenObject;

        public void ClearKitchenObject() => _currentKitchenObject = null;
    }
}
