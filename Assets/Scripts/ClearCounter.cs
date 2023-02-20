using CodeMonkey.KitchenCaosControl.ScriptableObjects;
using UnityEngine;

namespace CodeMonkey.KitchenCaosControl
{
    public class ClearCounter : MonoBehaviour
    {
        [SerializeField] private KitchenObject kitchenObject;
        [SerializeField] private Transform counterTopPoint;

        public void Interact()
        {
            var kitchenObjectTransform = Instantiate(kitchenObject.Prefab, counterTopPoint);
            kitchenObjectTransform.localPosition = Vector3.zero;
            Debug.Log(kitchenObjectTransform.GetComponent<KitchenObjectManager>().KitchenObject.Name);
        }
    }
}
