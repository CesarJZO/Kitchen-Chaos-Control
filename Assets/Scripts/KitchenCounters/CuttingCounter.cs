using System;
using System.Linq;
using CodeMonkey.KitchenCaosControl.ScriptableObjects;
using UnityEngine;

namespace CodeMonkey.KitchenCaosControl.KitchenCounters
{
    public class CuttingCounter : Counter, IHasProgress
    {
        public static event EventHandler OnAnyCut;

        public new static void ResetStaticData()
        {
            OnAnyCut = null;
        }

        public event EventHandler<IHasProgress.OnProgressChangedEventArgs> OnProgressChanged;
        public event EventHandler OnCut;

        [SerializeField] private CuttingRecipe[] cuttingRecipes;

        private int _cuttingProgress;

        public override void Interact(Player player)
        {
            if (!HasKitchenObject())
            {
                if (player.HasKitchenObject() && HasRecipe(player.GetKitchenObject()))
                {
                    player.GetKitchenObject().SetAndTeleportToParent(this);
                    _cuttingProgress = 0;
                    var cuttingRecipe = GetRecipeWithInput(GetKitchenObject().Data);
                    OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs
                    {
                        progressNormalized = _cuttingProgress / (float)cuttingRecipe.CuttingProgressRequired
                    });
                }
            }
            else
            {
                if (player.HasKitchenObject())
                {
                    if (player.GetKitchenObject().TryGetPlate(out var plateKitchenObject))
                    {
                        if (plateKitchenObject.TryAddIngredient(GetKitchenObject().Data))
                            GetKitchenObject().DestroySelf();
                    }
                }
                else
                {
                    GetKitchenObject().SetAndTeleportToParent(player);
                }
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
            OnAnyCut?.Invoke(this, EventArgs.Empty);

            // Check if the cutting progress is enough to slice the object
            var cuttingRecipe = GetRecipeWithInput(kitchenObject.Data);
            OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs
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
        /// Null if there is no match</param>
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
