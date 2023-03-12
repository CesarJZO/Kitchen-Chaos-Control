using CodeMonkey.KitchenCaosControl.ScriptableObjects;
using UnityEngine;
using UnityEngine.UI;

namespace CodeMonkey.KitchenCaosControl.UI
{
    public class PlateIconsSingleUI : MonoBehaviour
    {
        [SerializeField] private Image image;
        public void SetKitchenObjectData(KitchenObjectData kitchenObjectData)
        {
            image.sprite = kitchenObjectData.Sprite;
        }
    }
}
