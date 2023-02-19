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

            bool CapsuleCast(Vector3 direction) => Physics.CapsuleCast(position, position + Vector3.up, playerRadius, direction, moveDistance);
        }
    }
}
