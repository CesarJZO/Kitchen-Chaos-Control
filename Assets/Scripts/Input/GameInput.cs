using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace CodeMonkey.KitchenCaosControl.Input
{
    public class GameInput : MonoBehaviour
    {
        public static GameInput Instance { get; private set; }

        public event EventHandler OnInteractAction;
        public event EventHandler OnInteractAlternateAction;
        public event EventHandler OnPauseAction;

        private PlayerInputActions _inputActions;

        private void Awake()
        {
            Instance = this;

            _inputActions = new PlayerInputActions();
            _inputActions.Player.Enable();
            _inputActions.Player.Interact.performed += OnInteractPerformed;
            _inputActions.Player.InteractAlternate.performed += OnInteractAlternatePerformed;
            _inputActions.Player.Pause.performed += OnPausePerformed;
        }

        private void OnDestroy()
        {
            _inputActions.Player.Disable();
            _inputActions.Player.Interact.performed -= OnInteractPerformed;
            _inputActions.Player.InteractAlternate.performed -= OnInteractAlternatePerformed;
            _inputActions.Player.Pause.performed -= OnPausePerformed;

            _inputActions.Dispose();
        }

        private void OnInteractAlternatePerformed(InputAction.CallbackContext obj)
        {
            OnInteractAlternateAction?.Invoke(this, EventArgs.Empty);
        }

        private void OnInteractPerformed(InputAction.CallbackContext obj)
        {
            OnInteractAction?.Invoke(this, EventArgs.Empty);
        }

        private void OnPausePerformed(InputAction.CallbackContext obj)
        {
            OnPauseAction?.Invoke(this, EventArgs.Empty);
        }

        /// <summary>
        /// Returns a movement vector on the XZ plane, based on the input.
        /// </summary>
        /// <param name="maxMagnitude">Maximum magnitude of the vector, 1 by default</param>
        /// <returns>Clamped vector on the XZ plane</returns>
        public Vector3 GetMovementDirection(float maxMagnitude = 1f)
        {
            var inputVector = _inputActions.Player.Move.ReadValue<Vector2>();
            var xzDirection = new Vector3(inputVector.x, 0f, inputVector.y);
            return Vector3.ClampMagnitude(xzDirection, maxMagnitude);
        }
    }
}
