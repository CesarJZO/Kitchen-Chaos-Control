using CodeMonkey.KitchenCaosControl.Input;
using UnityEngine;

namespace CodeMonkey.KitchenCaosControl
{
    public class GameInput : MonoBehaviour
    {
        private PlayerInputActions _inputActions;

        private void Awake()
        {
            _inputActions = new PlayerInputActions();
            _inputActions.Player.Enable();
        }

        public Vector3 GetNormalizedMovementVector()
        {
            var inputVector = _inputActions.Player.Move.ReadValue<Vector2>();
            return new Vector3(inputVector.x, 0f, inputVector.y);
        }
    }
}
