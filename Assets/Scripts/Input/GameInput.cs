using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace CodeMonkey.KitchenChaosControl.Input
{
    public class GameInput : MonoBehaviour
    {
        private const string PlayerPrefsBindings = "Bindings";

        public static GameInput Instance { get; private set; }

        public event EventHandler OnInteractAction;
        public event EventHandler OnInteractAlternateAction;
        public event EventHandler OnPauseAction;
        public event EventHandler OnBindingRebind;

        private PlayerInputActions _inputActions;

        private void Awake()
        {
            Instance = this;

            _inputActions = new PlayerInputActions();

            if (PlayerPrefs.HasKey(PlayerPrefsBindings))
                _inputActions.LoadBindingOverridesFromJson(PlayerPrefs.GetString(PlayerPrefsBindings));

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

        public string GetBindingName(Binding binding) => binding switch
        {
            Binding.MoveUp => _inputActions.Player.Move.bindings[1].ToDisplayString(),
            Binding.MoveDown => _inputActions.Player.Move.bindings[3].ToDisplayString(),
            Binding.MoveLeft => _inputActions.Player.Move.bindings[5].ToDisplayString(),
            Binding.MoveRight => _inputActions.Player.Move.bindings[7].ToDisplayString(),

            Binding.Interact => _inputActions.Player.Interact.bindings[0].ToDisplayString(),
            Binding.InteractAlternate => _inputActions.Player.InteractAlternate.bindings[0].ToDisplayString(),
            Binding.Pause => _inputActions.Player.Pause.bindings[0].ToDisplayString(),

            Binding.GamepadInteract => _inputActions.Player.Interact.bindings[1].ToDisplayString(),
            Binding.GamepadInteractAlternate => _inputActions.Player.InteractAlternate.bindings[1].ToDisplayString(),
            Binding.GamepadPause => _inputActions.Player.Pause.bindings[1].ToDisplayString(),
            _ => _inputActions.Player.Move.bindings[1].ToDisplayString()
        };

        public void RebindBinding(Binding binding, Action onActionRebound)
        {
            _inputActions.Player.Disable();

            (InputAction inputAction, int bindingIndex) = binding switch
            {
                Binding.MoveUp => (_inputActions.Player.Move, 1),
                Binding.MoveDown => (_inputActions.Player.Move, 3),
                Binding.MoveLeft => (_inputActions.Player.Move, 5),
                Binding.MoveRight => (_inputActions.Player.Move, 7),

                Binding.Interact => (_inputActions.Player.Interact, 0),
                Binding.InteractAlternate => (_inputActions.Player.InteractAlternate, 0),
                Binding.Pause => (_inputActions.Player.Pause, 0),

                Binding.GamepadInteract => (_inputActions.Player.Interact, 1),
                Binding.GamepadInteractAlternate => (_inputActions.Player.InteractAlternate, 1),
                Binding.GamepadPause => (_inputActions.Player.Pause, 1),
                _ => throw new ArgumentOutOfRangeException(nameof(binding), binding, null)
            };

            inputAction.PerformInteractiveRebinding(bindingIndex)
                .OnComplete(callback =>
                {
                    callback.Dispose();
                    _inputActions.Player.Enable();
                    onActionRebound();

                    string json = _inputActions.SaveBindingOverridesAsJson();
                    PlayerPrefs.SetString(PlayerPrefsBindings, json);
                    PlayerPrefs.Save();

                    OnBindingRebind?.Invoke(this, EventArgs.Empty);
                    Debug.Log(this);
                })
                .Start();
        }

        public enum Binding
        {
            MoveUp,
            MoveDown,
            MoveLeft,
            MoveRight,
            Interact,
            InteractAlternate,
            Pause,
            GamepadInteract,
            GamepadInteractAlternate,
            GamepadPause
        }
    }
}
