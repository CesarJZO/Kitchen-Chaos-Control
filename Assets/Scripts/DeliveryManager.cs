using System.Collections.Generic;
using System.Linq;
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
            for (var i = 0; i < _waitingRecipeList.Count; i++)
            {
                var waitingRecipe = _waitingRecipeList[i];

                if (waitingRecipe.ingredients.Count != plateKitchenObject.Ingredients.Count) continue;

                var plateContentsMatchesRecipe = waitingRecipe.ingredients
                    .Select(ingredient => plateKitchenObject.Ingredients
                        .Any(plateIngredient => plateIngredient == ingredient))
                    .All(ingredientFound => ingredientFound);

                if (!plateContentsMatchesRecipe) continue;

                // Recipe delivered
                Debug.Log("Recipe delivered: " + waitingRecipe.recipeName);
                _waitingRecipeList.RemoveAt(i);
                return;
            }

            // No matches found
            Debug.Log("Player did not deliver a correct recipe!");
        }
    }
}
