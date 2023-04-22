using UnityEngine;

namespace CodeMonkey.KitchenChaosControl.Management
{
    [RequireComponent(typeof(AudioSource))]
    public class MusicManager : MonoBehaviour
    {
        private const string PlayerPrefsMusicVolume = "MusicVolume";

        public static MusicManager Instance { get; private set; }

        private AudioSource _audioSource;

        private float _volume = 0.3f;

        private void Awake()
        {
            Instance = this;

            _audioSource = GetComponent<AudioSource>();

            _volume = PlayerPrefs.GetFloat(PlayerPrefsMusicVolume, 0.3f);

            _audioSource.volume = _volume;
        }

        public void ChangeVolume()
        {
            _volume += 0.1f;

            if (_volume > 1f)
                _volume = 0f;

            _audioSource.volume = _volume;

            PlayerPrefs.SetFloat(PlayerPrefsMusicVolume, _volume);
            PlayerPrefs.Save();
        }

        public float GetVolume()
        {
            return _volume;
        }
    }
}
