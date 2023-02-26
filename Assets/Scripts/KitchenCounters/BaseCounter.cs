using CodeMonkey.KitchenCaosControl.ScriptableObjects;
using UnityEngine;

namespace CodeMonkey.KitchenCaosControl.KitchenCounters
{
    public abstract class BaseCounter : MonoBehaviour, IKitchenObjectParent
    {
        [SerializeField] private Transform counterTopPoint;

        private KitchenObjectBehaviour _currentKitchenObject;

        public abstract void Interact(Player player);

        public bool HasKitchenObject() => _currentKitchenObject;

        public Transform GetParentFollowPoint() => counterTopPoint;

        public void SetKitchenObject(KitchenObjectBehaviour kitchenObject) => _currentKitchenObject = kitchenObject;

        public KitchenObjectBehaviour GetKitchenObject() => _currentKitchenObject;

        public void ClearKitchenObject() => _currentKitchenObject = null;
    }
}
