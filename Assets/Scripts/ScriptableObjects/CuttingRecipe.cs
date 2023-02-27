using UnityEngine;

namespace CodeMonkey.KitchenCaosControl.ScriptableObjects
{
    [CreateAssetMenu]
    public class CuttingRecipe : ScriptableObject
    {
        public KitchenObjectData input;
        public KitchenObjectData output;
    }
}
