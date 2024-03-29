﻿using System;
using CodeMonkey.KitchenChaosControl.Management;
using UnityEngine;
using UnityEngine.UI;

namespace CodeMonkey.KitchenChaosControl.UI
{
    public class GamePauseUI : MonoBehaviour
    {
        public static GamePauseUI Instance { get; private set; }

        [SerializeField] private Button resumeButton;
        [SerializeField] private Button optionsButton;
        [SerializeField] private Button mainMenuButton;

        private void Awake()
        {
            Instance = this;

            resumeButton.onClick.AddListener(() => GameManager.Instance.TogglePauseGame());
            optionsButton.onClick.AddListener(() =>
            {
                Hide();
                OptionsUI.Instance.Show();
            });
            mainMenuButton.onClick.AddListener(() => Loader.Load(Loader.Scene.MainMenuScene));
        }

        private void Start()
        {
            GameManager.Instance.OnGamePaused += GameManagerOnGamePaused;
            GameManager.Instance.OnGameUnpaused += GameManagerOnGameUnpaused;

            Hide();
        }

        private void GameManagerOnGamePaused(object sender, EventArgs e)
        {
            Show();
        }

        private void GameManagerOnGameUnpaused(object sender, EventArgs e)
        {
            Hide();
        }

        public void Show()
        {
            gameObject.SetActive(true);
            resumeButton.Select();
        }

        public void Hide() => gameObject.SetActive(false);
    }
}
