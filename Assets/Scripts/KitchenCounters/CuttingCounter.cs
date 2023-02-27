using System;
using System.Linq;
using CodeMonkey.KitchenCaosControl.ScriptableObjects;
using UnityEngine;

namespace CodeMonkey.KitchenCaosControl.KitchenCounters
{
    public class CuttingCounter : Counter
    {
        public event EventHandler<OnCuttingProgressChangedEventArgs> OnCuttingProgressChanged;
        public class OnCuttingProgressChangedEventArgs : EventArgs
        {
            public float progressNormalized;
        }

        public event EventHandler OnCut;

        [SerializeField] private CuttingRecipe[] cuttingRecipes;

        private int _cuttingProgress;

        public override void Interact(Player player)
        {
            if (HasKitchenObject())
                GetKitchenObject().SetAndTeleportToParent(player);
            else if (player.HasKitchenObject() && HasRecipe(player.GetKitchenObject()))
            {
                player.GetKitchenObject().SetAndTeleportToParent(this);
                _cuttingProgress = 0;
                var cuttingRecipe = GetRecipeWithInput(GetKitchenObject().Data);
                OnCuttingProgressChanged?.Invoke(this, new OnCuttingProgressChangedEventArgs
                {
                    progressNormalized = _cuttingProgress / (float) cuttingRecipe.CuttingProgressRequired
                });
            }

            bool HasRecipe(KitchenObject input)
            {
                return cuttingRecipes.Any(recipe => recipe.Input == input.Data);
            }
        }

        public override void InteractAlternate(Player player)
        {
            if (!HasKitchenObject()) return;

            var kitchenObject = GetKitchenObject();
            var slicedObjectData = GetOutputForInput(kitchenObject.Data);
            if (!slicedObjectData) return;
            _cuttingProgress++;
            // At this point, this counter has a kitchen object, and it can be sliced

            OnCut?.Invoke(this, EventArgs.Empty);

            // Check if the cutting progress is enough to slice the object
            var cuttingRecipe = GetRecipeWithInput(kitchenObject.Data);
            OnCuttingProgressChanged?.Invoke(this, new OnCuttingProgressChangedEventArgs
            {
                progressNormalized = _cuttingProgress / (float) cuttingRecipe.CuttingProgressRequired
            });

            if (_cuttingProgress < cuttingRecipe.CuttingProgressRequired) return;

            kitchenObject.DestroySelf();
            KitchenObject.SpawnKitchenObject(slicedObjectData, this);
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
        /// Null if there is no matc</param>
        private CuttingRecipe GetRecipeWithInput(KitchenObjectData input)
        {
            // Select the possible output of the cutting recipe
            var slicedCandidates = from cuttingRecipe in cuttingRecipes
                where cuttingRecipe.Input == input
                select cuttingRecipe;
            // Get the first possible output, or null if there is none
            return slicedCandidates.FirstOrDefault();
        }
    }
}
