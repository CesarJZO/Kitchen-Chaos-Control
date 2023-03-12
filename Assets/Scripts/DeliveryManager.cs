using System.Collections.Generic;
using CodeMonkey.KitchenCaosControl.ScriptableObjects;
using UnityEngine;

namespace CodeMonkey.KitchenCaosControl
{
    public class DeliveryManager : MonoBehaviour
    {
        public static DeliveryManager Instance { get; private set; }

        [SerializeField] private float spawnRecipeTime;
        [SerializeField] private int maxWaitingRecipes;

        [Tooltip("Reference to the only instance of the RecipeList scriptable object. Useful if multiple objects need to access the same list of recipes.")]
        [SerializeField] private RecipeList recipeList;
        private List<Recipe> _waitingRecipeList;

        private void Awake()
        {
            Instance = this;
            _waitingRecipeList = new List<Recipe>();
        }

        private void Start()
        {
            InvokeRepeating(nameof(SpawnRecipe), spawnRecipeTime, spawnRecipeTime);
        }

        private void SpawnRecipe()
        {
            if (_waitingRecipeList.Count >= maxWaitingRecipes) return;
            var waitingRecipe = recipeList.recipes[Random.Range(0, recipeList.recipes.Count -1)];
            Debug.Log(waitingRecipe.recipeName);
            _waitingRecipeList.Add(waitingRecipe);
        }

        public void DeliverRecipe(PlateKitchenObject plateKitchenObject)
        {
            for (int i = 0; i < _waitingRecipeList.Count; i++)
            {
                var waitingRecipe = _waitingRecipeList[i];

                if (waitingRecipe.ingredients.Count != plateKitchenObject.Ingredients.Count) continue;

                var plateContentsMatchesRecipe = true;
                foreach (var ingredient in waitingRecipe.ingredients)
                {
                    var ingredientFound = false;
                    // Cycling through all ingredients in the recipe
                    foreach (var plateIngredient in plateKitchenObject.Ingredients)
                    {
                        // Cycling through all ingredients on the plate
                        if (plateIngredient == ingredient)
                        {
                            // Ingredients match
                            ingredientFound = true;
                            break;
                        }
                    }
                    if (!ingredientFound)
                    {
                        // Ingredient not found on the plate
                        plateContentsMatchesRecipe = false;
                        break;
                    }
                }

                if (plateContentsMatchesRecipe)
                {
                    // Recipe delivered
                    Debug.Log("Recipe delivered: " + waitingRecipe.recipeName);
                    _waitingRecipeList.RemoveAt(i);
                    return;
                }
            }

            // No matches found
            Debug.Log("Player delivered the wrong recipe");
        }
    }
}
