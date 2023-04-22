using System;
using CodeMonkey.KitchenChaosControl.Input;
using UnityEngine;

namespace CodeMonkey.KitchenChaosControl.Management
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance { get; private set; }

        public event EventHandler OnStateChanged;
        public event EventHandler OnGamePaused;
        public event EventHandler OnGameUnpaused;

        [SerializeField] private float waitToStartTime;
        [SerializeField] private float countdownToStartTime;
        [SerializeField] private float gamePlayTime;

        private State _state;

        private float _waitingToStartTimer;
        private float _countdownToStartTimer;
        private float _gamePlayingTimer;

        private bool _isGamePaused;

        public bool IsGamePlaying => _state == State.GamePlaying;
        public bool IsCountdownToStartActive => _state == State.CountdownToStart;
        public bool IsGameOver => _state == State.GameOver;

        public float CountdownToStartTimer => _countdownToStartTimer;

        // Since the timer is counting down, we need to invert the value
        public float GamePlayingTimerNormalized => 1 - (_gamePlayingTimer / gamePlayTime);

        private void Awake()
        {
            if (Instance)
                Debug.LogError("There should only be one instance of GameManager!");
            else
                Instance = this;
            _state = State.WaitingToStart;

            _waitingToStartTimer = waitToStartTime;
            _countdownToStartTimer = countdownToStartTime;
            _gamePlayingTimer = gamePlayTime;
        }

        private void Start()
        {
            GameInput.Instance.OnPauseAction += GameInputOnPauseAction;
        }

        private void GameInputOnPauseAction(object sender, EventArgs e)
        {
            TogglePauseGame();
        }

        private void Update()
        {
            switch (_state)
            {
                case State.WaitingToStart:
                    _waitingToStartTimer -= Time.deltaTime;
                    if (_waitingToStartTimer < 0f)
                        _state = State.CountdownToStart;
                    OnStateChanged?.Invoke(this, EventArgs.Empty);
                    break;
                case State.CountdownToStart:
                    _countdownToStartTimer -= Time.deltaTime;
                    if (_countdownToStartTimer < 0f)
                        _state = State.GamePlaying;
                    OnStateChanged?.Invoke(this, EventArgs.Empty);
                    break;
                case State.GamePlaying:
                    _gamePlayingTimer -= Time.deltaTime;
                    if (_gamePlayingTimer < 0f)
                        _state = State.GameOver;
                    OnStateChanged?.Invoke(this, EventArgs.Empty);
                    break;
                case State.GameOver:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public void TogglePauseGame()
        {
            _isGamePaused = !_isGamePaused;

            Time.timeScale = _isGamePaused ? 0f : 1f;

            if (_isGamePaused)
                OnGamePaused?.Invoke(this, EventArgs.Empty);
            else
                OnGameUnpaused?.Invoke(this, EventArgs.Empty);
        }

        private enum State
        {
            WaitingToStart,
            CountdownToStart,
            GamePlaying,
            GameOver
        }
    }
}
