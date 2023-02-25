using System;
using CodeMonkey.KitchenCaosControl.Input;
using UnityEngine;

namespace CodeMonkey.KitchenCaosControl.Player
{
    public class Player : MonoBehaviour
    {
        /// <summary>
        /// Instance of the player
        /// </summary>
        public static Player Instance { get; private set; }

        public event EventHandler<OnSelectedCounterUpdatedEventArgs> OnSelectedCounterUpdated;
        public class OnSelectedCounterUpdatedEventArgs : EventArgs
        {
            public ClearCounter selectedCounter;
        }

        [Header("Settings")]
        [SerializeField] private float moveSpeed;
        [SerializeField] private float rotationSpeed;

        [Header("Physics")]
        [SerializeField] private float playerRadius;
        [SerializeField] private float interactDistance;
        [SerializeField] private LayerMask countersLayerMask;

        [Header("Dependencies")]
        [SerializeField] private GameInput gameInput;

        private Vector3 _lastMoveDirection;

        private ClearCounter _selectedCounter;

        public bool IsWalking { get; private set; }

        private void Awake()
        {
            if (Instance)
                Debug.LogError("There should only be one instance of Player!");
            Instance = this;
        }

        private void Start()
        {
            gameInput.OnInteractAction += GameInputOnInteractAction;
        }

        private void GameInputOnInteractAction(object sender, EventArgs e)
        {
            if (_selectedCounter)
                _selectedCounter.Interact();
        }

        private void Update()
        {
            HandleMovement();
            HandleInteractions();
        }

        private void HandleInteractions()
        {
            var moveDirection = gameInput.GetNormalizedMovementVector();

            // If we're moving, update the last move direction
            if (moveDirection != Vector3.zero)
                _lastMoveDirection = moveDirection;


            if (Physics.Raycast(transform.position, _lastMoveDirection, out var hitInfo, interactDistance, countersLayerMask))
            {
                if (hitInfo.collider.TryGetComponent(out ClearCounter clearCounter))
                {
                    if (_selectedCounter != clearCounter)
                        SetSelectedCounter(clearCounter);
                }
                else
                    SetSelectedCounter(null);
            }
            else
                SetSelectedCounter(null);

            void SetSelectedCounter(ClearCounter clearCounter)
            {
                _selectedCounter = clearCounter;
                OnSelectedCounterUpdated?.Invoke(this, new OnSelectedCounterUpdatedEventArgs
                {
                    selectedCounter = _selectedCounter
                });
            }
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
