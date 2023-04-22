using UnityEngine;

namespace CodeMonkey.KitchenChaosControl.ScriptableObjects
{
    [CreateAssetMenu]
    public class CuttingRecipe : ScriptableObject
    {
        [field:SerializeField] public KitchenObjectData Input { get; private set; }
        [field:SerializeField] public KitchenObjectData Output { get; private set; }
        [field:SerializeField] public int CuttingProgressRequired { get; private set; }
    }
}
