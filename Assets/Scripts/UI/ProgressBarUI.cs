using UnityEngine;
using UnityEngine.UI;

namespace CodeMonkey.KitchenChaosControl.UI
{
    public class ProgressBarUI : MonoBehaviour
    {
        [SerializeField] private GameObject progressBarGameObject;
        [SerializeField] private Image barImage;

        private void Start()
        {
            var hasProgress = progressBarGameObject.GetComponent<IHasProgress>();
            if (hasProgress is null)
                Debug.LogError($"No IHasProgress component found on {progressBarGameObject.name}");
            else
                hasProgress.OnProgressChanged += OnProgressChanged;

            barImage.fillAmount = 0f;

            gameObject.SetActive(false);
        }

        private void OnProgressChanged(object sender, IHasProgress.OnProgressChangedEventArgs e)
        {
            barImage.fillAmount = e.progressNormalized;
            gameObject.SetActive(e.progressNormalized is > 0f and < 1f);
        }
    }
}
