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
        private List<KitchenObjectData> _ingredients;

        private void Awake()
        {
            _ingredients = new List<KitchenObjectData>();
        }

        public bool TryAddIngredient(KitchenObjectData ingredientData)
        {
            if (!validIngredients.Contains(ingredientData)) return false;

            if (_ingredients.Contains(ingredientData)) return false;

            _ingredients.Add(ingredientData);

            OnIngredientAdded?.Invoke(this, new OnIngredientAddedEventArgs
            {
                ingredientData = ingredientData
            });

            return true;
        }
    }
}
