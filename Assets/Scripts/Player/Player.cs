using UnityEngine;

namespace CodeMonkey.KitchenCaosControl.Player
{
    public class Player : MonoBehaviour
    {
        [SerializeField] private float moveSpeed;
        [SerializeField] private float rotationSpeed;

        [Header("Dependencies")]
        [SerializeField] private GameInput gameInput;

        public bool IsWalking { get; private set; }

        private void Update()
        {
            var moveDirection = gameInput.GetNormalizedMovementVector();
            var t = transform;
            t.position += moveDirection * (moveSpeed * Time.deltaTime);
            IsWalking = moveDirection != Vector3.zero;

            var forward = Vector3.Slerp(t.forward, moveDirection, Time.deltaTime * rotationSpeed);
            if (forward != Vector3.zero) t.forward = forward;
        }
    }
}
