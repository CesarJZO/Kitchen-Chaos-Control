using System;
using CodeMonkey.KitchenChaosControl.Management;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace CodeMonkey.KitchenChaosControl.UI
{
    public class OptionsUI : MonoBehaviour
    {
        public static OptionsUI Instance { get; private set; }

        [SerializeField] private Button soundEffectsButton;
        [SerializeField] private TextMeshProUGUI soundEffectsText;

        [SerializeField] private Button musicButton;
        [SerializeField] private TextMeshProUGUI musicText;

        [SerializeField] private Button closeButton;

        private void Awake()
        {
            Instance = this;

            soundEffectsButton.onClick.AddListener(() =>
            {
                SoundManager.Instance.ChangeVolume();
                UpdateVisual();
            });
            musicButton.onClick.AddListener(() =>
            {
                MusicManager.Instance.ChangeVolume();
                UpdateVisual();
            });
            closeButton.onClick.AddListener(() =>
            {
                Hide();
                GamePauseUI.Instance.Show();
            });
        }

        private void Start()
        {
            GameManager.Instance.OnGameUnpaused += GameManagerOnGameUnpaused;

            UpdateVisual();

            Hide();
        }

        private void GameManagerOnGameUnpaused(object sender, EventArgs e)
        {
            Hide();
        }

        private void UpdateVisual()
        {
            soundEffectsText.text = $"Sound effects: {Mathf.Round(SoundManager.Instance.GetVolume() * 10f)}";
            musicText.text = $"Music: {Mathf.Round(MusicManager.Instance.GetVolume() * 10f)}";
        }

        public void Show() => gameObject.SetActive(true);

        public void Hide() => gameObject.SetActive(false);
    }
}
