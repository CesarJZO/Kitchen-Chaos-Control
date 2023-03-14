using UnityEngine;

namespace CodeMonkey.KitchenCaosControl
{
    [RequireComponent(typeof(Player))]
    public class PlayerSound : MonoBehaviour
    {
        [SerializeField] private float soundInterval;

        private Player _player;

        private void Awake()
        {
            _player = GetComponent<Player>();
        }

        private void Start()
        {
            InvokeRepeating(nameof(PlaySound), time: 0f, soundInterval);
        }

        private void PlaySound()
        {
            if (_player.IsWalking)
                SoundManager.Instance.PlayFootstepsSound(_player.transform.position, volume: 1f);
        }
    }
}
