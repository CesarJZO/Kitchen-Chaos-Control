using System.Linq;
using CodeMonkey.KitchenCaosControl.ScriptableObjects;
using UnityEngine;

namespace CodeMonkey.KitchenCaosControl.KitchenCounters
{
    public class CuttingCounter : BaseCounter
    {
        [SerializeField] private CuttingRecipe[] cuttingRecipes;

        public override void Interact(Player player)
        {
            if (HasKitchenObject())
                GetKitchenObject().SetAndTeleportToParent(player);
            else if (player.HasKitchenObject() && HasRecipe(player.GetKitchenObject().Data))
                player.GetKitchenObject().SetAndTeleportToParent(this);

            bool HasRecipe(KitchenObjectData input)
            {
                return cuttingRecipes.Any(recipe => recipe.input == input);
            }
        }

        public override void InteractAlternate(Player player)
        {
            if (!HasKitchenObject()) return;

            var kitchenObject = GetKitchenObject();

            // Select the possible output of the cutting recipe
            var slicedCandidates = from cuttingRecipe in cuttingRecipes
                where cuttingRecipe.input == kitchenObject.Data
                select cuttingRecipe.output;
            // Get the first possible output, or null if there is none
            var slicedObjectData = slicedCandidates.FirstOrDefault();

            if (!slicedObjectData) return;
            // At this point, this counter has a kitchen object, and it can be sliced
            kitchenObject.DestroySelf();
            KitchenObject.SpawnKitchenObject(slicedObjectData, this);
        }
    }
}
