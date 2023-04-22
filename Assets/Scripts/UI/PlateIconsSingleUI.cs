using CodeMonkey.KitchenChaosControl.ScriptableObjects;
using UnityEngine;
using UnityEngine.UI;

namespace CodeMonkey.KitchenChaosControl.UI
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
