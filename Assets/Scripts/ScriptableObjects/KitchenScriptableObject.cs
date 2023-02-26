using UnityEngine;

namespace CodeMonkey.KitchenCaosControl.ScriptableObjects
{
    [CreateAssetMenu]
    public class KitchenScriptableObject : ScriptableObject
    {
        [field:SerializeField] public KitchenObject Prefab { get; private set; }
        [field:SerializeField] public Sprite Sprite { get; private set; }
        [field:SerializeField] public string Name { get; private set; }
    }
}
