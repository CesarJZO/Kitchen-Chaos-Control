using System;
using UnityEngine;

namespace CodeMonkey.KitchenCaosControl.Management
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance { get; private set; }

        public event EventHandler OnStateChanged;

        private enum State
        {
            WaitingToStart,
            CountdownToStart,
            GamePlaying,
            GameOver
        }

        private State _state;
        [SerializeField] private float waitToStartTime;
        private float _waitingToStartTimer;
        [SerializeField] private float countdownToStartTime;
        private float _countdownToStartTimer;
        [SerializeField] private float gamePlayTime;
        private float _gamePlayingTimer;

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
    }
}
