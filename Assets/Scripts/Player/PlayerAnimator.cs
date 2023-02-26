using UnityEngine;

namespace CodeMonkey.KitchenCaosControl
{
    public class PlayerAnimator : MonoBehaviour
    {
        private static readonly int Walk = Animator.StringToHash("IsWalking");

        [SerializeField] private Player player;
        [SerializeField] private Animator animator;

        private void Awake()
        {
            if (!player) player = GetComponentInParent<Player>();
            if (!animator) animator = GetComponent<Animator>();
        }

        private void LateUpdate()
        {
            animator.speed = player.IsWalking ? player.SpeedMultiplier : 1f;
            animator.SetBool(Walk, player.IsWalking);
        }
    }
}
