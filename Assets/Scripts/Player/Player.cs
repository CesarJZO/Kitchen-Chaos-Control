using System;
using UnityEngine;

namespace CodeMonkey.KitchenCaosControl.Player
{
    public class Player : MonoBehaviour
    {
        [Header("Settings")]
        [SerializeField] private float moveSpeed;
        [SerializeField] private float rotationSpeed;

        [Header("Physics")]
        [SerializeField] private float playerRadius;
        [SerializeField] private LayerMask countersLayerMask;

        [Header("Dependencies")]
        [SerializeField] private GameInput gameInput;

        private Vector3 _lastMoveDirection;

        public bool IsWalking { get; private set; }

        private void Start()
        {
            gameInput.OnInteractAction += GameInputOnInteractAction;
        }

        private void GameInputOnInteractAction(object sender, EventArgs e)
        {
            var moveDirection = gameInput.GetNormalizedMovementVector();

            if (moveDirection != Vector3.zero)
                _lastMoveDirection = moveDirection;

            const float interactDistance = 2f;
            if (!Physics.Raycast(transform.position, _lastMoveDirection, out var hitInfo, interactDistance,
                    countersLayerMask)) return;

            if (hitInfo.collider.TryGetComponent(out ClearCounter clearCounter))
                clearCounter.Interact();
        }

        private void Update()
        {
            HandleMovement();
            HandleInteractions();
        }

        private void HandleInteractions()
        {
            var moveDirection = gameInput.GetNormalizedMovementVector();

            if (moveDirection != Vector3.zero)
                _lastMoveDirection = moveDirection;
        }

        private void HandleMovement()
        {
            var moveDirection = gameInput.GetNormalizedMovementVector();
            var t = transform;
            var position = t.position;

            var moveDistance = moveSpeed * Time.deltaTime;
            var canMove = !CapsuleCast(moveDirection);

            if (!canMove)
            {
                // Cannot move towards the direction, try to move sideways

                // Attempt only x movement
                var moveDirectionX = new Vector3(moveDirection.x, 0f, 0f).normalized;
                canMove = !CapsuleCast(moveDirectionX);

                if (canMove)
                {
                    // Can move only on the x
                    moveDirection = moveDirectionX;
                }
                else
                {
                    // Attempt only z movement
                    var moveDirectionZ = new Vector3(0f, 0f, moveDirection.z).normalized;
                    canMove = !CapsuleCast(moveDirectionZ);
                    if (canMove) // Can move only on the z
                        moveDirection = moveDirectionZ;
                    // else cannot move at all
                }
            }

            IsWalking = moveDirection != Vector3.zero && canMove;

            if (canMove)
                t.position += moveDirection * moveDistance;

            var forward = Vector3.Slerp(t.forward, moveDirection, Time.deltaTime * rotationSpeed);
            if (forward != Vector3.zero) t.forward = forward;

            bool CapsuleCast(Vector3 direction)
            {
                return Physics.CapsuleCast(position, position + Vector3.up, playerRadius, direction, moveDistance);
            }
        }
    }
}
