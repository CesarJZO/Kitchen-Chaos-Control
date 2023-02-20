using UnityEngine;

namespace CodeMonkey.KitchenCaosControl.ScriptableObjects
{
    [CreateAssetMenu]
    public class KitchenObject : ScriptableObject
    {
        [field:SerializeField] public Transform Prefab { get; private set; }
        [field:SerializeField] public Sprite Sprite { get; private set; }
        [field:SerializeField] public string Name { get; private set; }
    }
}
