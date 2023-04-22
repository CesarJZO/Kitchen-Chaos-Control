using System.Collections.Generic;
using UnityEngine;

namespace CodeMonkey.KitchenChaosControl.ScriptableObjects
{
    [CreateAssetMenu]
    public class Recipe : ScriptableObject
    {
        public List<KitchenObjectData> ingredients;
        public string recipeName;
    }
}
