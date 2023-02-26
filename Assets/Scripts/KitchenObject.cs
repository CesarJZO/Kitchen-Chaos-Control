﻿using CodeMonkey.KitchenCaosControl.ScriptableObjects;
using UnityEngine;

namespace CodeMonkey.KitchenCaosControl
{
    public class KitchenObject : MonoBehaviour
    {
        [SerializeField] private KitchenScriptableObject kitchenScriptableObject;
        public KitchenScriptableObject KitchenScriptableObject => kitchenScriptableObject;

        private IKitchenObjectParent _kitchenObjectParent;

        /// <summary>
        /// Sets the parent of this object to the given parent, and clears the old parent's kitchen object.
        /// Also teleports this object to the new parent's position.
        /// If parent already has a kitchen object, this will do nothing.
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

        /// <summary>
        /// Clears the parent of this object, and destroys this object.
        /// </summary>
        public void DestroySelf()
        {
            _kitchenObjectParent.ClearKitchenObject();
            Destroy(gameObject);
        }

        /// <summary>
        /// Spawns a kitchen object at the given parent.
        /// </summary>
        /// <param name="kitchenScriptableObject">The ScriptableObject containing the data of a KitchenObject.</param>
        /// <param name="parent">The parent who going to hold the KitchenObject.</param>
        /// <returns></returns>
        public static KitchenObject SpawnKitchenObject(KitchenScriptableObject kitchenScriptableObject, IKitchenObjectParent parent)
        {
            var kitchenObject = Instantiate(kitchenScriptableObject.Prefab);
            kitchenObject.SetAndTeleportToParent(parent);
            return kitchenObject;
        }
    }
}
