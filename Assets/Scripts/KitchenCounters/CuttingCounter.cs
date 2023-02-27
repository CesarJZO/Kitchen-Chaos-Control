using System.Linq;
using CodeMonkey.KitchenCaosControl.ScriptableObjects;
using UnityEngine;

namespace CodeMonkey.KitchenCaosControl.KitchenCounters
{
    public class CuttingCounter : Counter
    {
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
            if (slicedObjectData == null) return;
            _cuttingProgress++;
            // At this point, this counter has a kitchen object, and it can be sliced

            // Check if the cutting progress is enough to slice the object
            var cuttingRecipe = GetRecipeForInput(kitchenObject.Data);
            if (_cuttingProgress < cuttingRecipe.CuttingProgressRequired) return;

            kitchenObject.DestroySelf();
            KitchenObject.SpawnKitchenObject(slicedObjectData, this);
        }

        /// <summary>
        /// Returns the KitchenObjectData for the given input, or null if there is none
        /// </summary>
        /// <param name="input"></param>
        private KitchenObjectData GetOutputForInput(KitchenObjectData input)
        {
            return GetRecipeForInput(input)?.Output;
        }

        /// <summary>
        /// Returns the cutting recipe for the given input, or null if there is none
        /// </summary>
        /// <param name="input">if it is referenced as input in a recipe, returns its defined output</param>
        private CuttingRecipe GetRecipeForInput(KitchenObjectData input)
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
