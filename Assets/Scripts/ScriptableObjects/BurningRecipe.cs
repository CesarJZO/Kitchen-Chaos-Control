using UnityEngine;

namespace CodeMonkey.KitchenChaosControl.ScriptableObjects
{
    [CreateAssetMenu]
    public class BurningRecipe : ScriptableObject
    {
        [field:SerializeField] public KitchenObjectData Input { get; private set; }
        [field:SerializeField] public KitchenObjectData Output { get; private set; }
        [field:SerializeField] public float BurningMaxTime { get; private set; }
    }
}
