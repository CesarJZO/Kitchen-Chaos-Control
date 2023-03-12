using System;
using System.Collections.Generic;
using CodeMonkey.KitchenCaosControl.ScriptableObjects;
using UnityEngine;

namespace CodeMonkey.KitchenCaosControl
{
    public class PlateKitchenObject : KitchenObject
    {
        public event EventHandler<OnIngredientAddedEventArgs> OnIngredientAdded;

        public class OnIngredientAddedEventArgs : EventArgs
        {
            public KitchenObjectData ingredientData;
        }

        [SerializeField] private List<KitchenObjectData> validIngredients;
        public List<KitchenObjectData> Ingredients { get; private set; }

        private void Awake()
        {
            Ingredients = new List<KitchenObjectData>();
        }

        public bool TryAddIngredient(KitchenObjectData ingredientData)
        {
            if (!validIngredients.Contains(ingredientData)) return false;

            if (Ingredients.Contains(ingredientData)) return false;

            Ingredients.Add(ingredientData);

            OnIngredientAdded?.Invoke(this, new OnIngredientAddedEventArgs
            {
                ingredientData = ingredientData
            });

            return true;
        }
    }
}
