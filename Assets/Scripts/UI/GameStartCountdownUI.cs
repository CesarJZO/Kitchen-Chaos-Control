using System;
using CodeMonkey.KitchenChaosControl.Management;
using TMPro;
using UnityEngine;

namespace CodeMonkey.KitchenChaosControl.UI
{
    public class GameStartCountdownUI : MonoBehaviour
    {
        private static readonly int Popup = Animator.StringToHash("CountdownUI_Popup");

        [SerializeField] private TextMeshProUGUI countdownText;

        private Animator _animator;
        private int _previousCountdownNumber;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }

        private void Start()
        {
            GameManager.Instance.OnStateChanged += GameManagerOnStateChanged;

            gameObject.SetActive(false);
        }

        private void GameManagerOnStateChanged(object sender, EventArgs e)
        {
            gameObject.SetActive(GameManager.Instance.IsCountdownToStartActive);
        }

        private void Update()
        {
            int countdownNumber = Mathf.CeilToInt(GameManager.Instance.CountdownToStartTimer);
            countdownText.text = countdownNumber.ToString();

            if (_previousCountdownNumber == countdownNumber) return;

            _previousCountdownNumber = countdownNumber;
            _animator.CrossFade(Popup, 0f);
            SoundManager.Instance.PlayCountdownSound();
        }
    }
}
