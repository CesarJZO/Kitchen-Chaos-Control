using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace CodeMonkey.KitchenCaosControl.Input
{
    public class GameInput : MonoBehaviour
    {
        public event EventHandler OnInteractAction;
        private PlayerInputActions _inputActions;

        private void Awake()
        {
            _inputActions = new PlayerInputActions();
            _inputActions.Player.Enable();
            _inputActions.Player.Interact.performed += OnInteractPerformed;
        }

        private void OnInteractPerformed(InputAction.CallbackContext obj)
        {
            OnInteractAction?.Invoke(this, EventArgs.Empty);
        }

        /// <summary>
        /// Returns a normalized movement vector on the XZ plane, based on the input
        /// </summary>
        /// <returns>Vector with the form of x: n, y: 0, z: n</returns>
        public Vector3 GetNormalizedMovementVector()
        {
            var inputVector = _inputActions.Player.Move.ReadValue<Vector2>();
            return new Vector3(inputVector.x, 0f, inputVector.y);
        }
    }
}
