using UnityEngine;

namespace CodeMonkey.KitchenChaosControl
{
    public interface IKitchenObjectParent
    {
        public Transform GetParentFollowPoint();

        public void SetKitchenObject(KitchenObject kitchenObject);

        public KitchenObject GetKitchenObject();

        public void ClearKitchenObject();

        public bool HasKitchenObject();
    }
}
