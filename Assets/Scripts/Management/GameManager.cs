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
        private float _waitingToStartTimer = 1f;
        private float _countdownToStartTimer = 3f;
        private float _gamePlayingTimer = 10f;

        public bool IsGamePlaying => _state == State.GamePlaying;
        public bool IsCountdownToStartActive => _state == State.CountdownToStart;

        public float CountdownToStartTimer => _countdownToStartTimer;

        private void Awake()
        {
            if (Instance)
                Debug.LogError("There should only be one instance of GameManager!");
            else
                Instance = this;
            _state = State.WaitingToStart;
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
