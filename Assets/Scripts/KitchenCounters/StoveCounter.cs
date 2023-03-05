using System;
using System.Linq;
using CodeMonkey.KitchenCaosControl.ScriptableObjects;
using UnityEngine;

namespace CodeMonkey.KitchenCaosControl.KitchenCounters
{
    public class StoveCounter : Counter
    {
        [SerializeField] private FryingRecipe[] fryingRecipes;

        private float _fryingTimer;
        private FryingRecipe _currentFryingRecipe;

        private void Update()
        {
            if (HasKitchenObject())
            {
                _fryingTimer += Time.deltaTime;
                if (_fryingTimer >= _currentFryingRecipe.FryingMaxTime)
                {
                    _fryingTimer = 0f;
                    GetKitchenObject().DestroySelf();
                    KitchenObject.SpawnKitchenObject(_currentFryingRecipe.Output, this);
                }
            }
        }

        public override void Interact(Player player)
        {
            if (HasKitchenObject())
                GetKitchenObject().SetAndTeleportToParent(player);
            else if (player.HasKitchenObject() && HasRecipe(player.GetKitchenObject()))
            {
                player.GetKitchenObject().SetAndTeleportToParent(this);
                _currentFryingRecipe = GetRecipeWithInput(GetKitchenObject().Data);
            }

            bool HasRecipe(KitchenObject input)
            {
                return fryingRecipes.Any(recipe => recipe.Input == input.Data);
            }
        }

        /// <summary>
        /// Returns the KitchenObjectData for the given input, or null if there is none
        /// </summary>
        /// <param name="input">If it is referenced as input in a recipe,returns its defined
        /// output KitchenObjectData. Null if don't have a match</param>
        private KitchenObjectData GetOutputForInput(KitchenObjectData input)
        {
            return GetRecipeWithInput(input)?.Output;
        }

        /// <summary>
        /// Returns the cutting recipe for the given input, or null if there is none
        /// </summary>
        /// <param name="input">If it is referenced as input in a recipe, returns the recipe.
        /// Null if there is no match</param>
        private FryingRecipe GetRecipeWithInput(KitchenObjectData input)
        {
            // Select the possible output of the cutting recipe
            var slicedCandidates = from cuttingRecipe in fryingRecipes
                where cuttingRecipe.Input == input
                select cuttingRecipe;
            // Get the first possible output, or null if there is none
            return slicedCandidates.FirstOrDefault();
        }
    }
}
