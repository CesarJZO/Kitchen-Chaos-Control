using System;
using CodeMonkey.KitchenCaosControl.Input;
using UnityEngine;
using UnityEngine.InputSystem;

namespace CodeMonkey.KitchenCaosControl
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

        public Vector3 GetNormalizedMovementVector()
        {
            var inputVector = _inputActions.Player.Move.ReadValue<Vector2>();
            return new Vector3(inputVector.x, 0f, inputVector.y);
        }
    }
}
