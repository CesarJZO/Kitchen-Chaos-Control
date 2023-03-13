using System;
using UnityEngine;

namespace CodeMonkey.KitchenCaosControl.UI
{
    public class DeliveryManagerUI : MonoBehaviour
    {
        [SerializeField] private Transform container;
        [SerializeField] private DeliveryManagerSingleUI recipeTemplate;

        private void Awake()
        {
            recipeTemplate.gameObject.SetActive(false);
        }

        private void Start()
        {
            DeliveryManager.Instance.OnRecipeSpawned += DeliveryManager_OnRecipeSpawned;
            DeliveryManager.Instance.OnRecipeDelivered += DeliveryManager_OnRecipeDelivered;

            UpdateVisual();
        }

        private void DeliveryManager_OnRecipeSpawned(object sender, EventArgs e)
        {
            UpdateVisual();
        }

        private void DeliveryManager_OnRecipeDelivered(object sender, EventArgs e)
        {
            UpdateVisual();
        }

        private void UpdateVisual()
        {
            foreach (Transform child in container)
            {
                if (child == recipeTemplate) continue;
                Destroy(child.gameObject);
            }

            foreach (var waitingRecipe in DeliveryManager.Instance.WaitingRecipeList)
            {
                var recipeTransform = Instantiate(recipeTemplate, container);
                recipeTransform.gameObject.SetActive(true);
                recipeTransform.SetRecipe(waitingRecipe);
            }
        }
    }
}
