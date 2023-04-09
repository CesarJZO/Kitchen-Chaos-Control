using System;
using System.Collections.Generic;
using System.Linq;
using CodeMonkey.KitchenCaosControl.ScriptableObjects;
using UnityEngine;

namespace CodeMonkey.KitchenCaosControl
{
    public class DeliveryManager : MonoBehaviour
    {
        public event EventHandler OnRecipeSpawned;
        public event EventHandler OnRecipeCompleted;
        public event EventHandler OnRecipeSuccess;
        public event EventHandler OnRecipeFailed;

        public static DeliveryManager Instance { get; private set; }

        [SerializeField] private float spawnRecipeTime;
        [SerializeField] private int maxWaitingRecipes;
        private int _recipesDelivered;

        [Tooltip("Reference to the only instance of the RecipeList scriptable object. Useful if multiple objects need to access the same list of recipes.")]
        [SerializeField] private RecipeList recipeList;
        public List<Recipe> WaitingRecipeList { get; private set; }

        public int RecipesDelivered => _recipesDelivered;

        private void Awake()
        {
            Instance = this;
            WaitingRecipeList = new List<Recipe>();
        }

        private void Start()
        {
            InvokeRepeating(nameof(SpawnRecipe), spawnRecipeTime, spawnRecipeTime);
        }

        private void SpawnRecipe()
        {
            if (WaitingRecipeList.Count >= maxWaitingRecipes) return;
            var waitingRecipe = recipeList.recipes[UnityEngine.Random.Range(0, recipeList.recipes.Count)];
            WaitingRecipeList.Add(waitingRecipe);

            OnRecipeSpawned?.Invoke(this, EventArgs.Empty);
        }

        public void DeliverRecipe(PlateKitchenObject plateKitchenObject)
        {
            for (var i = 0; i < WaitingRecipeList.Count; i++)
            {
                var waitingRecipe = WaitingRecipeList[i];

                if (waitingRecipe.ingredients.Count != plateKitchenObject.Ingredients.Count) continue;

                var plateContentsMatchesRecipe = waitingRecipe.ingredients
                    .Select(ingredient => plateKitchenObject.Ingredients
                        .Any(plateIngredient => plateIngredient == ingredient))
                    .All(ingredientFound => ingredientFound);

                if (!plateContentsMatchesRecipe) continue;

                // Recipe delivered
                _recipesDelivered = RecipesDelivered + 1;
                WaitingRecipeList.RemoveAt(i);
                OnRecipeSuccess?.Invoke(this, EventArgs.Empty);
                OnRecipeCompleted?.Invoke(this, EventArgs.Empty);
                return;
            }

            OnRecipeFailed?.Invoke(this, EventArgs.Empty);
        }
    }
}
