using CodeMonkey.KitchenCaosControl.ScriptableObjects;
using UnityEngine;

namespace CodeMonkey.KitchenCaosControl
{
    public class KitchenObjectBehaviour : MonoBehaviour
    {
        [SerializeField] private KitchenScriptableObject kitchenScriptableObject;
        public KitchenScriptableObject KitchenScriptableObject => kitchenScriptableObject;

        private ClearCounter _clearCounter;

        /// <summary>
        /// When a new counter is set, this object will be parented to the counter and teleported to the new counter's position.
        /// Otherwise, it will just return the current counter.
        /// </summary>
        public ClearCounter ClearCounter
        {
            get => _clearCounter;
            set
            {
                var newClearCounter = value;
                // If this object has a counter, clear it
                if (_clearCounter)
                    _clearCounter.ClearKitchenObject();

                // Set the new counter
                _clearCounter = newClearCounter;

                if (newClearCounter.HasKitchenObject)
                    Debug.LogError("New counter already has a kitchen object! This should never happen!");
                newClearCounter.KitchenObject = this;

                // Parent this object to the new counter and teleport it to the new counter's position
                var t = transform;
                t.parent = newClearCounter.CounterTopPoint;
                t.localPosition = Vector3.zero;
            }
        }
    }
}
