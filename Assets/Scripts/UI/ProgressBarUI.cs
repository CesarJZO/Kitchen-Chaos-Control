using CodeMonkey.KitchenCaosControl.KitchenCounters;
using UnityEngine;
using UnityEngine.UI;

namespace CodeMonkey.KitchenCaosControl.UI
{
    public class ProgressBarUI : MonoBehaviour
    {
        [SerializeField] private CuttingCounter cuttingCounter;
        [SerializeField] private Image barImage;

        private void Start()
        {
            cuttingCounter.OnCuttingProgressChanged += CuttingCounter_OnCuttingProgressChanged;

            barImage.fillAmount = 0f;
            
            gameObject.SetActive(false);
        }

        private void CuttingCounter_OnCuttingProgressChanged(object sender, CuttingCounter.OnCuttingProgressChangedEventArgs e)
        {
            barImage.fillAmount = e.progressNormalized;

            gameObject.SetActive(e.progressNormalized is > 0f and < 1f);
        }
    }
}
