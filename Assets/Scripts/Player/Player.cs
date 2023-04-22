using System;
using CodeMonkey.KitchenChaosControl.Input;
using CodeMonkey.KitchenChaosControl.KitchenCounters;
using CodeMonkey.KitchenChaosControl.Management;
using UnityEngine;

namespace CodeMonkey.KitchenChaosControl
{
    public class Player : MonoBehaviour, IKitchenObjectParent
    {
        /// <summary>
        /// Instance of the player
        /// </summary>
        public static Player Instance { get; private set; }

        public event EventHandler OnPickupSomething;

        public event EventHandler<SelectedCounterUpdatedEventArgs> OnSelectedCounterUpdated;
        public class SelectedCounterUpdatedEventArgs : EventArgs
        {
            public Counter selectedCounter;
        }

        [Header("Status")]
        [SerializeField] private Transform kitchenObjectHoldPoint;

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

        private Counter _selectedCounter;
        private KitchenObject _currentKitchenObject;

        public bool IsWalking { get; private set; }
        public float SpeedMultiplier { get; private set; }

        private void Awake()
        {
            if (Instance)
                Debug.LogError("There should only be one instance of Player!");
            Instance = this;
        }

        private void Start()
        {
            gameInput.OnInteractAction += GameInputOnInteractAction;
            gameInput.OnInteractAlternateAction += GameInputOnInteractAlternateAction;
        }

        private void GameInputOnInteractAlternateAction(object sender, EventArgs e)
        {
            if (!GameManager.Instance.IsGamePlaying) return;
            if (_selectedCounter)
                _selectedCounter.InteractAlternate(this);
        }

        private void GameInputOnInteractAction(object sender, EventArgs e)
        {
            if (!GameManager.Instance.IsGamePlaying) return;
            if (_selectedCounter)
                _selectedCounter.Interact(this);
        }

        private void Update()
        {
            HandleMovement();
            HandleInteractions();
        }

        private void HandleInteractions()
        {
            var moveDirection = gameInput.GetMovementDirection();

            // If we're moving, update the last move direction
            if (moveDirection != Vector3.zero)
                _lastMoveDirection = moveDirection;


            if (Physics.Raycast(transform.position, _lastMoveDirection, out var hitInfo, interactDistance, countersLayerMask))
            {
                if (hitInfo.collider.TryGetComponent(out Counter counter))
                {
                    if (_selectedCounter != counter)
                        SetSelectedCounter(counter);
                }
                else
                    SetSelectedCounter(null);
            }
            else
                SetSelectedCounter(null);

            void SetSelectedCounter(Counter counter)
            {
                _selectedCounter = counter;
                OnSelectedCounterUpdated?.Invoke(this, new SelectedCounterUpdatedEventArgs
                {
                    selectedCounter = _selectedCounter
                });
            }
        }

        private void HandleMovement()
        {
            var moveDirection = gameInput.GetMovementDirection();
            SpeedMultiplier = moveDirection.magnitude;
            var t = transform;
            var position = t.position;

            var moveDistance = moveSpeed * Time.deltaTime;
            var canMove = !CapsuleCast(moveDirection);

            if (!canMove)
            {
                // Cannot move towards the direction, try to move sideways

                // Attempt only x movement
                var moveDirectionX = new Vector3(moveDirection.x, 0f, 0f);
                var perpendicularDeadZone = 0.5f;
                canMove = Mathf.Abs(moveDirection.x) > perpendicularDeadZone && !CapsuleCast(moveDirectionX);

                if (canMove)
                {
                    // Can move only on the x
                    moveDirection = moveDirectionX;
                }
                else
                {
                    // Attempt only z movement
                    var moveDirectionZ = new Vector3(0f, 0f, moveDirection.z);
                    canMove = Mathf.Abs(moveDirection.z) > perpendicularDeadZone && !CapsuleCast(moveDirectionZ);
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

        public Transform GetParentFollowPoint() => kitchenObjectHoldPoint;

        public void SetKitchenObject(KitchenObject kitchenObject)
        {
            _currentKitchenObject = kitchenObject;

            if (kitchenObject)
                OnPickupSomething?.Invoke(this, EventArgs.Empty);
        }

        public KitchenObject GetKitchenObject() => _currentKitchenObject;

        public void ClearKitchenObject() => _currentKitchenObject = null;

        public bool HasKitchenObject() => _currentKitchenObject;
    }
}
