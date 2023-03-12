using System.Collections.Generic;
using CodeMonkey.KitchenCaosControl.ScriptableObjects;

namespace CodeMonkey.KitchenCaosControl
{
    public class PlateKitchenObject : KitchenObject
    {
        private List<KitchenObjectData> _ingredients;

        private void Awake()
        {
            _ingredients = new List<KitchenObjectData>();
        }

        public void AddIngredient(KitchenObjectData ingredientData)
        {
            _ingredients.Add(ingredientData);
        }
    }
}
