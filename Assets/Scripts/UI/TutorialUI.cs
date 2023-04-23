using System;
using CodeMonkey.KitchenChaosControl.Input;
using CodeMonkey.KitchenChaosControl.Management;
using TMPro;
using UnityEngine;

namespace CodeMonkey.KitchenChaosControl.UI
{
    public class TutorialUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI keyMoveUpText;
        [SerializeField] private TextMeshProUGUI keyMoveLeftText;
        [SerializeField] private TextMeshProUGUI keyMoveDownText;
        [SerializeField] private TextMeshProUGUI keyMoveRightText;
        [SerializeField] private TextMeshProUGUI keyInteractText;
        [SerializeField] private TextMeshProUGUI keyInteractAltText;
        [SerializeField] private TextMeshProUGUI keyPauseText;
        [SerializeField] private TextMeshProUGUI keyGamepadInteractText;
        [SerializeField] private TextMeshProUGUI keyGamepadInteractAltText;
        [SerializeField] private TextMeshProUGUI keyGamepadPauseText;

        private void Start()
        {
            GameInput.Instance.OnBindingRebind += GameInputOnBindingRebind;
            GameManager.Instance.OnStateChanged += GameManagerOnStateChanged;

            UpdateVisual();

            gameObject.SetActive(true);
        }

        private void GameManagerOnStateChanged(object sender, EventArgs e)
        {
            if (GameManager.Instance.IsCountdownToStartActive)
                gameObject.SetActive(false);
        }

        private void GameInputOnBindingRebind(object sender, EventArgs e)
        {
            UpdateVisual();
        }

        private void UpdateVisual()
        {
            keyMoveUpText.text = GameInput.Instance.GetBindingName(GameInput.Binding.MoveUp);
            keyMoveLeftText.text = GameInput.Instance.GetBindingName(GameInput.Binding.MoveLeft);
            keyMoveDownText.text = GameInput.Instance.GetBindingName(GameInput.Binding.MoveDown);
            keyMoveRightText.text = GameInput.Instance.GetBindingName(GameInput.Binding.MoveRight);
            keyInteractText.text = GameInput.Instance.GetBindingName(GameInput.Binding.Interact);
            keyInteractAltText.text = GameInput.Instance.GetBindingName(GameInput.Binding.InteractAlternate);
            keyPauseText.text = GameInput.Instance.GetBindingName(GameInput.Binding.Pause);
            keyGamepadInteractText.text = GameInput.Instance.GetBindingName(GameInput.Binding.GamepadInteract);
            keyGamepadInteractAltText.text = GameInput.Instance.GetBindingName(GameInput.Binding.GamepadInteractAlternate);
            keyGamepadPauseText.text = GameInput.Instance.GetBindingName(GameInput.Binding.GamepadPause);
        }
    }
}
