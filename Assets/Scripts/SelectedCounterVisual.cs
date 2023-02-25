using UnityEngine;

namespace CodeMonkey.KitchenCaosControl
{
    public class SelectedCounterVisual : MonoBehaviour
    {
        [SerializeField] private ClearCounter clearCounter;
        [SerializeField] private GameObject visualGameObject;

        private void Start()
        {
            Player.Player.Instance.OnSelectedCounterUpdated += PlayerOnSelectedCounterUpdated;
        }

        private void PlayerOnSelectedCounterUpdated(object sender, Player.Player.OnSelectedCounterUpdatedEventArgs e)
        {
            visualGameObject.SetActive(e.selectedCounter == clearCounter);
        }
    }
}
