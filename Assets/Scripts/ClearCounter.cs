using UnityEngine;

namespace CodeMonkey.KitchenCaosControl
{
    public class ClearCounter : MonoBehaviour
    {
        [SerializeField] private Transform tomatoPrefab;
        [SerializeField] private Transform counterTopPoint;

        public void Interact()
        {
            var tomatoTransform = Instantiate(tomatoPrefab, counterTopPoint);
            tomatoTransform.localPosition = Vector3.zero;
        }
    }
}
