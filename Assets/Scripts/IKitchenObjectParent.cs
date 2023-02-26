using UnityEngine;

namespace CodeMonkey.KitchenCaosControl
{
    public interface IKitchenObjectParent
    {
        public Transform GetFollowParentFollowPoint();

        public void SetKitchenObject(KitchenObjectBehaviour kitchenObject);

        public KitchenObjectBehaviour GetKitchenObject();

        public void ClearKitchenObject();

        public bool HasKitchenObject();
    }
}
