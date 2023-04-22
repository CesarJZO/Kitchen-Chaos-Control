using CodeMonkey.KitchenChaosControl.ScriptableObjects;
using UnityEngine;

namespace CodeMonkey.KitchenChaosControl
{
    public class KitchenObject : MonoBehaviour
    {
        [SerializeField] private KitchenObjectData kitchenObjectData;
        public KitchenObjectData Data => kitchenObjectData;

        private IKitchenObjectParent _kitchenObjectParent;

        /// <summary>
        /// Sets the parent of this object to the given parent, and clears the old parent's kitchen object.
        /// Also teleports this object to the new parent's position.
        /// If parent already has a kitchen object, this will do nothing.
        /// </summary>
        public void SetAndTeleportToParent(IKitchenObjectParent newParent)
        {
            // If this object has a parent, clear it
            _kitchenObjectParent?.ClearKitchenObject();

            // Set new parent
            _kitchenObjectParent = newParent;

            if (newParent.HasKitchenObject())
                Debug.LogError("Parent already has a kitchen object");
            newParent.SetKitchenObject(this);

            // Parent this object to the new counter and teleport it to the new counter's position
            var t = transform;
            t.parent = newParent.GetParentFollowPoint();
            t.localPosition = Vector3.zero;
        }

        /// <summary>
        /// Clears the parent of this object, and destroys this object.
        /// </summary>
        public void DestroySelf()
        {
            _kitchenObjectParent.ClearKitchenObject();
            Destroy(gameObject);
        }

        public bool TryGetPlate(out PlateKitchenObject plate)
        {
            if (this is PlateKitchenObject plateKitchenObject)
            {
                plate = plateKitchenObject;
                return true;
            }

            plate = null;
            return false;
        }

        /// <summary>
        /// Spawns a kitchen object at the given parent.
        /// </summary>
        /// <param name="data">The ScriptableObject containing the data of a KitchenObject.</param>
        /// <param name="parent">The parent who going to hold the KitchenObject.</param>
        /// <returns></returns>
        public static KitchenObject SpawnKitchenObject(KitchenObjectData data, IKitchenObjectParent parent)
        {
            if (parent.HasKitchenObject())
                Debug.LogError("Parent already has a kitchen object");
            var kitchenObject = Instantiate(data.Prefab).GetComponent<KitchenObject>();
            kitchenObject.SetAndTeleportToParent(parent);
            return kitchenObject;
        }
    }
}
