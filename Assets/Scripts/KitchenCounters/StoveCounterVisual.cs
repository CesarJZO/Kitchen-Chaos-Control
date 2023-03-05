using UnityEngine;

namespace CodeMonkey.KitchenCaosControl.KitchenCounters
{
    public class StoveCounterVisual : MonoBehaviour
    {
        [SerializeField] private StoveCounter stoveCounter;

        [SerializeField] private GameObject stoveOnGameObject;
        [SerializeField] private GameObject particlesGameObject;

        private void Start()
        {
            stoveCounter.OnStateChanged += StoveCounter_OnStateChanged;
        }

        private void StoveCounter_OnStateChanged(object sender, StoveCounter.OnStateChangedEventArgs e)
        {
            var showVisual = e.state is StoveCounter.State.Frying or StoveCounter.State.Fried;
            stoveOnGameObject.SetActive(showVisual);
            particlesGameObject.SetActive(showVisual);
        }
    }
}
