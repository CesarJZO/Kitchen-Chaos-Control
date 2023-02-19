using UnityEngine;

namespace CodeMonkey.KitchenCaosControl.Player
{
    public class Player : MonoBehaviour
    {
        [Header("Settings")]
        [SerializeField] private float moveSpeed;
        [SerializeField] private float rotationSpeed;
        [SerializeField] private float playerRadius;

        [Header("Dependencies")]
        [SerializeField] private GameInput gameInput;

        public bool IsWalking { get; private set; }

        private void Update()
        {
            var moveDirection = gameInput.GetNormalizedMovementVector();
            var t = transform;
            var position = t.position;

            var moveDistance = moveSpeed * Time.deltaTime;
            var canMove = !Physics.CapsuleCast(position, position + Vector3.up, playerRadius, moveDirection, moveDistance);
            IsWalking = moveDirection != Vector3.zero && canMove;

            if (canMove)
                t.position += moveDirection * moveDistance;

            var forward = Vector3.Slerp(t.forward, moveDirection, Time.deltaTime * rotationSpeed);
            if (forward != Vector3.zero) t.forward = forward;
        }
    }
}
