using System;
using UnityEngine;

namespace CodeMonkey.KitchenChaosControl.UI
{
    public class PlateIconsUI : MonoBehaviour
    {
        [SerializeField] private PlateKitchenObject plateKitchenObject;
        [SerializeField] private PlateIconsSingleUI iconTemplate;

        private void Awake()
        {
            iconTemplate.gameObject.SetActive(false);
        }

        private void Start()
        {
            plateKitchenObject.OnIngredientAdded += PlateKitchenObject_OnIngredientAdded;
        }

        private void PlateKitchenObject_OnIngredientAdded(object sender, PlateKitchenObject.OnIngredientAddedEventArgs e)
        {
            UpdateVisual();
        }

        private void UpdateVisual()
        {
            foreach (Transform child in transform)
            {
                if (child == iconTemplate.transform) continue;
                Destroy(child.gameObject);
            }
            foreach (var ingredient in plateKitchenObject.Ingredients)
            {
                var icon = Instantiate(iconTemplate, transform);
                icon.gameObject.SetActive(true);
                icon.SetKitchenObjectData(ingredient);
            }
        }
    }
}
