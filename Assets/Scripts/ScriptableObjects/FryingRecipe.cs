using UnityEngine;

namespace CodeMonkey.KitchenCaosControl.ScriptableObjects
{
    [CreateAssetMenu]
    public class FryingRecipe : ScriptableObject
    {
        [field:SerializeField] public KitchenObjectData Input { get; private set; }
        [field:SerializeField] public KitchenObjectData Output { get; private set; }
        [field:SerializeField] public float FryingMaxTime { get; private set; }
    }
}
