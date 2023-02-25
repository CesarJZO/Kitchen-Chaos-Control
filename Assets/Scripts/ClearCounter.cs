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

        public void Interact()
        {
            // If there is no current kitchen object, add one
            if (!_currentKitchenObject)
            {
                var kitchenObject = Instantiate(kitchenScriptableObject.Prefab, counterTopPoint);
                kitchenObject.transform.localPosition = Vector3.zero;

                _currentKitchenObject = kitchenObject;
                _currentKitchenObject.ClearCounter = this;
            }
            else
            {
                Debug.Log(_currentKitchenObject.ClearCounter.name);
            }
        }
    }
}
