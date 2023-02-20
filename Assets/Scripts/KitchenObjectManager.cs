using CodeMonkey.KitchenCaosControl.ScriptableObjects;
using UnityEngine;

namespace CodeMonkey.KitchenCaosControl
{
    public class KitchenObjectManager : MonoBehaviour
    {
        [SerializeField] private KitchenObject kitchenObject;

        public KitchenObject KitchenObject => kitchenObject;
    }
}
