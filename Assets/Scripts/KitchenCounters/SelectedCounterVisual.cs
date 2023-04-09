using UnityEngine;

namespace CodeMonkey.KitchenCaosControl.KitchenCounters
{
    public class SelectedCounterVisual : MonoBehaviour
    {
        [SerializeField] private Counter counter;
        [SerializeField] private GameObject[] visualGameObjects;

        private void Start()
        {
            Player.Instance.OnSelectedCounterUpdated += PlayerOnSelectedCounterUpdated;
        }

        private void PlayerOnSelectedCounterUpdated(object sender, Player.SelectedCounterUpdatedEventArgs e)
        {
            foreach (var visualGameObject in visualGameObjects)
                visualGameObject.SetActive(e.selectedCounter == counter);
        }
    }
}
