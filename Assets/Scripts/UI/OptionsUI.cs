using System;
using CodeMonkey.KitchenChaosControl.Input;
using CodeMonkey.KitchenChaosControl.Management;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace CodeMonkey.KitchenChaosControl.UI
{
    public class OptionsUI : MonoBehaviour
    {
        public static OptionsUI Instance { get; private set; }

        [Header("Audio")]
        [SerializeField] private Button musicButton;
        [SerializeField] private TextMeshProUGUI musicText;

        [SerializeField] private Button soundEffectsButton;
        [SerializeField] private TextMeshProUGUI soundEffectsText;

        [Header("Key Rebinding")]
        [SerializeField] private GameObject pressToRebindKey;
        [Space]
        [SerializeField] private TextMeshProUGUI moveUpText;
        [SerializeField] private Button moveUpButton;
        [Space]
        [SerializeField] private TextMeshProUGUI moveDownText;
        [SerializeField] private Button moveDownButton;
        [Space]
        [SerializeField] private TextMeshProUGUI moveLeftText;
        [SerializeField] private Button moveLeftButton;
        [Space]
        [SerializeField] private TextMeshProUGUI moveRightText;
        [SerializeField] private Button moveRightButton;
        [Space]
        [SerializeField] private TextMeshProUGUI interactText;
        [SerializeField] private Button interactButton;
        [Space]
        [SerializeField] private TextMeshProUGUI interactAlt;
        [SerializeField] private Button interactAltButton;
        [Space]
        [SerializeField] private TextMeshProUGUI pauseText;
        [SerializeField] private Button pauseButton;
        [Space]

        [SerializeField] private Button closeButton;

        private void Awake()
        {
            Instance = this;

            musicButton.onClick.AddListener(() =>
            {
                MusicManager.Instance.ChangeVolume();
                UpdateVisual();
            });
            soundEffectsButton.onClick.AddListener(() =>
            {
                SoundManager.Instance.ChangeVolume();
                UpdateVisual();
            });
            closeButton.onClick.AddListener(() =>
            {
                Hide();
                GamePauseUI.Instance.Show();
            });

            moveUpButton.onClick.AddListener(() => RebindBinding(GameInput.Binding.MoveUp));
            moveDownButton.onClick.AddListener(() => RebindBinding(GameInput.Binding.MoveDown));
            moveLeftButton.onClick.AddListener(() => RebindBinding(GameInput.Binding.MoveLeft));
            moveRightButton.onClick.AddListener(() => RebindBinding(GameInput.Binding.MoveRight));
            interactButton.onClick.AddListener(() => RebindBinding(GameInput.Binding.Interact));
            interactAltButton.onClick.AddListener(() => RebindBinding(GameInput.Binding.InteractAlternate));
            pauseButton.onClick.AddListener(() => RebindBinding(GameInput.Binding.Pause));
        }

        private void Start()
        {
            GameManager.Instance.OnGameUnpaused += GameManagerOnGameUnpaused;

            UpdateVisual();

            Hide();
            HidePressToRebindKey();
        }

        private void GameManagerOnGameUnpaused(object sender, EventArgs e)
        {
            Hide();
        }

        private void UpdateVisual()
        {
            soundEffectsText.text = $"Sound effects: {Mathf.Round(SoundManager.Instance.GetVolume() * 10f)}";
            musicText.text = $"Music: {Mathf.Round(MusicManager.Instance.GetVolume() * 10f)}";

            moveUpText.text = GameInput.Instance.GetBindingName(GameInput.Binding.MoveUp);
            moveDownText.text = GameInput.Instance.GetBindingName(GameInput.Binding.MoveDown);
            moveLeftText.text = GameInput.Instance.GetBindingName(GameInput.Binding.MoveLeft);
            moveRightText.text = GameInput.Instance.GetBindingName(GameInput.Binding.MoveRight);
            interactText.text = GameInput.Instance.GetBindingName(GameInput.Binding.Interact);
            interactAlt.text = GameInput.Instance.GetBindingName(GameInput.Binding.InteractAlternate);
            pauseText.text = GameInput.Instance.GetBindingName(GameInput.Binding.Pause);
        }

        public void Show() => gameObject.SetActive(true);

        public void Hide() => gameObject.SetActive(false);

        private void ShowPressToRebindKey() => pressToRebindKey.SetActive(true);

        private void HidePressToRebindKey() => pressToRebindKey.SetActive(false);

        private void RebindBinding(GameInput.Binding binding)
        {
            ShowPressToRebindKey();
            GameInput.Instance.RebindBinding(binding, () =>
            {
                HidePressToRebindKey();
                UpdateVisual();
            });
        }
    }
}
