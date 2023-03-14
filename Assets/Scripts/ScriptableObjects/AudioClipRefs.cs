using UnityEngine;

namespace CodeMonkey.KitchenCaosControl.ScriptableObjects
{
    [CreateAssetMenu]
    public class AudioClipRefs : ScriptableObject
    {
        public AudioClip[] chop;
        public AudioClip[] deliveryFail;
        public AudioClip[] deliverySuccess;
        public AudioClip[] footstep;
        public AudioClip[] objectDrop;
        public AudioClip[] objectPickup;
        public AudioClip stoveSizzle;
        public AudioClip[] trash;
        public AudioClip[] warning;
    }
}
