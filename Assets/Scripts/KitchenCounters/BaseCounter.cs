using UnityEngine;

namespace CodeMonkey.KitchenCaosControl.KitchenCounters
{
    public abstract class BaseCounter : MonoBehaviour, IKitchenObjectParent
    {
        [SerializeField] private Transform counterTopPoint;

        private KitchenObject _currentKitchenObject;

        public abstract void Interact(Player player);

        public virtual void InteractAlternate(Player player) { }

        public bool HasKitchenObject() => _currentKitchenObject;

        public Transform GetParentFollowPoint() => counterTopPoint;

        public void SetKitchenObject(KitchenObject kitchenObject) => _currentKitchenObject = kitchenObject;

        public KitchenObject GetKitchenObject() => _currentKitchenObject;

        public void ClearKitchenObject() => _currentKitchenObject = null;
    }
}
