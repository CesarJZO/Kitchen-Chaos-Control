using System;
using UnityEngine;

namespace CodeMonkey.KitchenCaosControl.KitchenCounters
{
    public abstract class Counter : MonoBehaviour, IKitchenObjectParent
    {
        public static event EventHandler OnAnyObjectPlacedOnCounter;

        [SerializeField] private Transform counterTopPoint;

        private KitchenObject _currentKitchenObject;

        public abstract void Interact(Player player);

        public virtual void InteractAlternate(Player player) { }

        public bool HasKitchenObject() => _currentKitchenObject;

        public Transform GetParentFollowPoint() => counterTopPoint;

        public void SetKitchenObject(KitchenObject kitchenObject)
        {
            _currentKitchenObject = kitchenObject;

            if (kitchenObject)
                OnAnyObjectPlacedOnCounter?.Invoke(this, EventArgs.Empty);
        }

        public KitchenObject GetKitchenObject() => _currentKitchenObject;

        public void ClearKitchenObject() => _currentKitchenObject = null;
    }
}
