using CodeMonkey.KitchenCaosControl.ScriptableObjects;
using UnityEngine;

namespace CodeMonkey.KitchenCaosControl
{
    public class ClearCounter : MonoBehaviour
    {
        [SerializeField] private KitchenObjectSO kitchenObjectSo;
        [SerializeField] private Transform counterTopPoint;

        private KitchenObject _kitchenObject;

        public void Interact()
        {
            if (!kitchenObjectSo) return;
            var kitchenObjectTransform = Instantiate(kitchenObjectSo.Prefab, counterTopPoint);
            kitchenObjectTransform.localPosition = Vector3.zero;

            _kitchenObject = kitchenObjectTransform.GetComponent<KitchenObject>();
        }
    }
}
