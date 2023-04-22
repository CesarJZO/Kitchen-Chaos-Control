using System;
using CodeMonkey.KitchenChaosControl.ScriptableObjects;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace CodeMonkey.KitchenChaosControl.UI
{
    /// <summary>
    /// Single refers to a single recipe
    /// </summary>
    public class DeliveryManagerSingleUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI recipeNameText;
        [SerializeField] private Transform iconContainer;
        [SerializeField] private Image iconTemplate;

        private void Awake()
        {
            iconTemplate.gameObject.SetActive(false);
        }

        public void SetRecipe(Recipe recipe)
        {
            recipeNameText.text = recipe.recipeName;
            foreach (Transform child in iconContainer)
            {
                if (child == iconTemplate.transform) continue;
                Destroy(child.gameObject);
            }
            foreach (var ingredient in recipe.ingredients)
            {
                var ingredientIconTransform = Instantiate(iconTemplate, iconContainer);
                ingredientIconTransform.gameObject.SetActive(true);
                ingredientIconTransform.sprite = ingredient.Sprite;
            }
        }
    }
}
