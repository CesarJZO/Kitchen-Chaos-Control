using System;
using System.Collections.Generic;
using CodeMonkey.KitchenCaosControl.KitchenCounters;
using CodeMonkey.KitchenCaosControl.ScriptableObjects;
using UnityEngine;

namespace CodeMonkey.KitchenCaosControl.Management
{
    public class SoundManager : MonoBehaviour
    {
        private const string PlayerPrefsSoundEffectsVolume = "SoundEffectsVolume";

        public static SoundManager Instance { get; private set; }

        [SerializeField] private AudioClipRefs audioClipRefs;

        private float _volume = 1f;

        private void Awake()
        {
            Instance = this;

            _volume = PlayerPrefs.GetFloat(PlayerPrefsSoundEffectsVolume, 1f);
        }

        private void Start()
        {
            DeliveryManager.Instance.OnRecipeSuccess += DeliveryManager_OnRecipeSuccess;
            DeliveryManager.Instance.OnRecipeFailed += DeliveryManager_OnRecipeFailed;
            CuttingCounter.OnAnyCut += CuttingCounter_OnAnyCut;
            Player.Instance.OnPickupSomething += Player_OnPickupSomething;
            Counter.OnAnyObjectPlacedOnCounter += Counter_OnAnyObjectPlacedOnCounter;
            TrashCounter.OnAnyObjectTrashed += TrashCounter_OnAnyObjectTrashed;
        }

        private void DeliveryManager_OnRecipeSuccess(object sender, EventArgs e)
        {
            var deliveryCounter = DeliveryCounter.Instance;
            PlaySound(audioClipRefs.deliverySuccess, deliveryCounter.transform.position);
        }

        private void DeliveryManager_OnRecipeFailed(object sender, EventArgs e)
        {
            var deliveryCounter = DeliveryCounter.Instance;
            PlaySound(audioClipRefs.deliveryFail, deliveryCounter.transform.position);
        }

        private void CuttingCounter_OnAnyCut(object sender, EventArgs e)
        {
            var cuttingCounter = sender as CuttingCounter;
            PlaySound(audioClipRefs.chop, cuttingCounter!.transform.position);
        }

        private void Player_OnPickupSomething(object sender, EventArgs e)
        {
            PlaySound(audioClipRefs.objectPickup, Player.Instance.transform.position);
        }

        private void Counter_OnAnyObjectPlacedOnCounter(object sender, EventArgs e)
        {
            var counter = sender as Counter;
            PlaySound(audioClipRefs.objectDrop, counter!.transform.position);
        }

        private void TrashCounter_OnAnyObjectTrashed(object sender, EventArgs e)
        {
            var trashCounter = sender as TrashCounter;
            PlaySound(audioClipRefs.trash, trashCounter!.transform.position);
        }

        public void PlayFootstepsSound(Vector3 position, float volume)
        {
            PlaySound(audioClipRefs.footstep, position, volume);
        }

        private void PlaySound(AudioClip audioClip, Vector3 position, float volumeMultiplayer = 1f)
        {
            AudioSource.PlayClipAtPoint(audioClip, position, volumeMultiplayer * _volume);
        }

        private void PlaySound(IReadOnlyList<AudioClip> audioClips, Vector3 position, float volumeMultiplayer = 1f)
        {
            PlaySound(audioClips[UnityEngine.Random.Range(0, audioClips.Count)], position, volumeMultiplayer * _volume);
        }

        public void ChangeVolume()
        {
            _volume += 0.1f;

            if (_volume > 1f)
                _volume = 0f;

            PlayerPrefs.SetFloat(PlayerPrefsSoundEffectsVolume, _volume);
            PlayerPrefs.Save();
        }

        public float GetVolume()
        {
            return _volume;
        }
    }
}
