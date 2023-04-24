using CodeMonkey.KitchenChaosControl.KitchenCounters;
using UnityEngine;

namespace CodeMonkey.KitchenChaosControl.UI
{
    public class StoveBurnWarningUI : MonoBehaviour
    {
        [SerializeField] private StoveCounter stoveCounter;

        [SerializeField, Range(0f, 1f)]private float burnShowProgressPercentage;

        private void Start()
        {
            stoveCounter.OnProgressChanged += StoveCounterOnProgressChanged;

            gameObject.SetActive(false);
        }

        private void StoveCounterOnProgressChanged(object sender, IHasProgress.ProgressChangedEventArgs e)
        {
            bool show = e.progressNormalized >= burnShowProgressPercentage && stoveCounter.IsFried;
            gameObject.SetActive(show);
        }
    }
}
