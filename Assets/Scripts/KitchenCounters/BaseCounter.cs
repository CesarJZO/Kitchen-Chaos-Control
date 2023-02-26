using UnityEngine;

namespace CodeMonkey.KitchenCaosControl.KitchenCounters
{
    public abstract class BaseCounter : MonoBehaviour
    {
        public abstract void Interact(Player player);
    }
}
