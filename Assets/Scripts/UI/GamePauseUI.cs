using System;
using CodeMonkey.KitchenCaosControl.Management;
using UnityEngine;
using UnityEngine.UI;

namespace CodeMonkey.KitchenCaosControl.UI
{
    public class GamePauseUI : MonoBehaviour
    {
        [SerializeField] private Button resumeButton;
        [SerializeField] private Button mainMenuButton;

        private void Awake()
        {
            resumeButton.onClick.AddListener(() => GameManager.Instance.TogglePauseGame());
            mainMenuButton.onClick.AddListener(() => Loader.Load(Loader.Scene.MainMenuScene));
        }

        private void Start()
        {
            GameManager.Instance.OnGamePaused += GameManagerOnGamePaused;
            GameManager.Instance.OnGameUnpaused += GameManagerOnGameUnpaused;

            gameObject.SetActive(false);
        }

        private void GameManagerOnGamePaused(object sender, EventArgs e)
        {
            gameObject.SetActive(true);
        }

        private void GameManagerOnGameUnpaused(object sender, EventArgs e)
        {
            gameObject.SetActive(false);
        }
    }
}
