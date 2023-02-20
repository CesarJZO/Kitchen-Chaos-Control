using UnityEngine;

namespace CodeMonkey.KitchenCaosControl
{
    public class SelectedCounterVisual : MonoBehaviour
    {
        [SerializeField] private ClearCounter clearCounter;
        [SerializeField] private GameObject visualGameObject;

        private void Start()
        {
            Player.Player.Instance.OnSelectedCounterChanged += Player_OnSelectedCounterChanged;
        }

        private void Player_OnSelectedCounterChanged(object sender, Player.Player.OnSelectedCounterChangedEventArgs e)
        {
            visualGameObject.SetActive(e.selectedCounter == clearCounter);
        }
    }
}
