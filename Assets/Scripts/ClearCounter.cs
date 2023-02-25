using CodeMonkey.KitchenCaosControl.ScriptableObjects;
using UnityEngine;

namespace CodeMonkey.KitchenCaosControl
{
    public class ClearCounter : MonoBehaviour
    {
        [SerializeField] private KitchenScriptableObject kitchenScriptableObject;
        [SerializeField] private Transform counterTopPoint;
        public Transform CounterTopPoint => counterTopPoint;

        private KitchenObjectBehaviour _currentKitchenObject;

        public KitchenObjectBehaviour KitchenObject
        {
            get => _currentKitchenObject;
            set => _currentKitchenObject = value;
        }

        public void ClearKitchenObject() => _currentKitchenObject = null;

        public bool HasKitchenObject => _currentKitchenObject;

        public void Interact()
        {
            // If there is no current kitchen object, add one
            if (_currentKitchenObject) return;
            var kitchenObject = Instantiate(kitchenScriptableObject.Prefab, counterTopPoint);
            /* There's no need to set the current kitchen object's clear counter to this, because the kitchen object's
                clear counter will be set by the kitchen object */
            kitchenObject.ClearCounter = this;
        }
    }
}
