using CodeMonkey.KitchenCaosControl.ScriptableObjects;
using UnityEngine;

namespace CodeMonkey.KitchenCaosControl
{
    public class KitchenObjectBehaviour : MonoBehaviour
    {
        [SerializeField] private KitchenScriptableObject kitchenScriptableObject;
        public KitchenScriptableObject KitchenScriptableObject => kitchenScriptableObject;

        private ClearCounter _clearCounter;
        public ClearCounter ClearCounter
        {
            get => _clearCounter;
            set
            {
                _clearCounter = value;
                var t = transform;
                t.parent = _clearCounter.CounterTopPoint;
                t.localPosition = Vector3.zero;
            }
        }
    }
}
