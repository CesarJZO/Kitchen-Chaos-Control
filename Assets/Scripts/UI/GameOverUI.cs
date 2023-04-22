using System;
using CodeMonkey.KitchenChaosControl.Management;
using TMPro;
using UnityEngine;

namespace CodeMonkey.KitchenChaosControl.UI
{
    public class GameOverUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI recipesDeliveredText;

        private void Start()
        {
            GameManager.Instance.OnStateChanged += GameManagerOnStateChanged;
            gameObject.SetActive(false);
        }

        private void GameManagerOnStateChanged(object sender, EventArgs e)
        {
            recipesDeliveredText.text = DeliveryManager.Instance.RecipesDelivered.ToString();
            gameObject.SetActive(GameManager.Instance.IsGameOver);
        }
    }
}
