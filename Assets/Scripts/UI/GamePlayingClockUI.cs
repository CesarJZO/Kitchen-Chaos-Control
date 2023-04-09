using CodeMonkey.KitchenCaosControl.Management;
using UnityEngine;
using UnityEngine.UI;

namespace CodeMonkey.KitchenCaosControl.UI
{
    public class GamePlayingClockUI : MonoBehaviour
    {
        [SerializeField] private Image timerImage;

        private void Update()
        {
            timerImage.fillAmount = GameManager.Instance.GamePlayingTimerNormalized;
        }
    }
}
