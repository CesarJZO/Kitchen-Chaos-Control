using System;
using UnityEngine;

namespace CodeMonkey.KitchenChaosControl.KitchenCounters
{
    [RequireComponent(typeof(AudioSource))]
    public class StoveCounterSound : MonoBehaviour
    {
        [SerializeField]private StoveCounter stoveCounter;

        private AudioSource _audioSource;

        private void Awake()
        {
            _audioSource = GetComponent<AudioSource>();
        }

        private void Start()
        {
            stoveCounter.OnStateChanged += StoveCounter_OnStateChanged;
        }

        private void StoveCounter_OnStateChanged(object sender, StoveCounter.OnStateChangedEventArgs e)
        {
            var playSound = e.state is StoveCounter.State.Frying or StoveCounter.State.Fried;
            if (playSound)
                _audioSource.Play();
            else
                _audioSource.Stop();
        }
    }
}
