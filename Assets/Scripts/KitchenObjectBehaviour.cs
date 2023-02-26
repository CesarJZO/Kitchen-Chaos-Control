using CodeMonkey.KitchenCaosControl.ScriptableObjects;
using UnityEngine;

namespace CodeMonkey.KitchenCaosControl
{
    public class KitchenObjectBehaviour : MonoBehaviour
    {
        [SerializeField] private KitchenScriptableObject kitchenScriptableObject;
        public KitchenScriptableObject KitchenScriptableObject => kitchenScriptableObject;

        private IKitchenObjectParent _kitchenObjectParent;

        /// <summary>
        /// Sets the parent of this object to the given parent, and clears the old parent's kitchen object.
        /// Also teleports this object to the new parent's position.
        /// </summary>
        public void SetAndTeleportToParent(IKitchenObjectParent parent)
        {
            if (parent.HasKitchenObject()) return;
            // If this object has a counter, clear it
            _kitchenObjectParent?.ClearKitchenObject();

            // Set the new counter
            _kitchenObjectParent = parent;

            if (parent.HasKitchenObject())
                Debug.LogError("Parent already has a kitchen object! This should never happen!");
            parent.SetKitchenObject(this);

            // Parent this object to the new counter and teleport it to the new counter's position
            var t = transform;
            t.parent = parent.GetParentFollowPoint();
            t.localPosition = Vector3.zero;
        }
    }
}
