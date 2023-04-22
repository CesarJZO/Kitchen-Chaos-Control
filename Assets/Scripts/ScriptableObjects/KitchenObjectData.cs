using UnityEngine;

namespace CodeMonkey.KitchenChaosControl.ScriptableObjects
{
    [CreateAssetMenu]
    public class KitchenObjectData : ScriptableObject
    {
        [field:SerializeField] public Transform Prefab { get; private set; }
        [field:SerializeField] public Sprite Sprite { get; private set; }
        [field:SerializeField] public string Name { get; private set; }
    }
}
