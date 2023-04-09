using System;
using CodeMonkey.KitchenCaosControl.Management;
using TMPro;
using UnityEngine;

namespace CodeMonkey.KitchenCaosControl.UI
{
    public class GameStartCountdownUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI countdownText;

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
            countdownText.text = Mathf.CeilToInt(GameManager.Instance.CountdownToStartTimer).ToString();
        }
    }
}
