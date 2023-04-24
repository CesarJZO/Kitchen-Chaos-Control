using CodeMonkey.KitchenChaosControl.Management;
using UnityEngine;

namespace CodeMonkey.KitchenChaosControl.KitchenCounters
{
    [RequireComponent(typeof(AudioSource))]
    public class StoveCounterSound : MonoBehaviour
    {
        [SerializeField] private StoveCounter stoveCounter;

        [SerializeField] private float warningSoundTime;
        [SerializeField, Range(0f, 1f)] private float burnShowProgressPercentage;

        private AudioSource _audioSource;
        private float _warningSoundTimer;
        private bool _playWarningSound;

        private void Awake()
        {
            _audioSource = GetComponent<AudioSource>();
        }

        private void Start()
        {
            stoveCounter.OnStateChanged += StoveCounterOnStateChanged;
            stoveCounter.OnProgressChanged += StoveCounterOnProgressChanged;
        }

        private void StoveCounterOnStateChanged(object sender, StoveCounter.StateChangedEventArgs e)
        {
            bool playSound = e.state is StoveCounter.State.Frying or StoveCounter.State.Fried;
            if (playSound)
                _audioSource.Play();
            else
                _audioSource.Stop();
        }

        private void StoveCounterOnProgressChanged(object sender, IHasProgress.ProgressChangedEventArgs e)
        {
            _playWarningSound = e.progressNormalized >= burnShowProgressPercentage && stoveCounter.IsFried;

        }

        private void Update()
        {
            if (!_playWarningSound) return;

            if (_warningSoundTimer <= 0f)
            {
                _warningSoundTimer = warningSoundTime;

                SoundManager.Instance.PlayWarningSound(stoveCounter.transform.position);
            }

            _warningSoundTimer -= Time.deltaTime;
        }
    }
}
