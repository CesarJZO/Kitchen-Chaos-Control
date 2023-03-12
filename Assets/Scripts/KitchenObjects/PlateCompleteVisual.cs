using System;
using CodeMonkey.KitchenCaosControl.ScriptableObjects;
using UnityEngine;

namespace CodeMonkey.KitchenCaosControl
{
    public class PlateCompleteVisual : MonoBehaviour
    {
        [Serializable]
        public struct KitchenObjectDataGameObject
        {
            public KitchenObjectData kitchenObjectData;
            public GameObject gameObject;
        }

        [SerializeField] private PlateKitchenObject plateKitchenObject;
        [SerializeField] private KitchenObjectDataGameObject[] kitchenObjectDataGameObjectArray;
        private void Start()
        {
            plateKitchenObject.OnIngredientAdded += PlateKitchenObject_OnIngredientAdded;
            foreach (var o in kitchenObjectDataGameObjectArray)
                o.gameObject.SetActive(false);
        }

        private void PlateKitchenObject_OnIngredientAdded(object sender, PlateKitchenObject.OnIngredientAddedEventArgs e)
        {
            foreach (var o in kitchenObjectDataGameObjectArray)
            {
                if (o.kitchenObjectData == e.ingredientData)
                    o.gameObject.SetActive(true);
            }
        }
    }
}
