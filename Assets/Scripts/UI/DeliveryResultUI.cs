using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace CodeMonkey.KitchenChaosControl.UI
{
    public class DeliveryResultUI : MonoBehaviour
    {
        private static readonly int Popup = Animator.StringToHash("DeliveryResultUI_Popup");

        [SerializeField] private Image backgroundImage;
        [SerializeField] private Image iconImage;
        [SerializeField] private TextMeshProUGUI messageText;

        [Header("Visuals")]
        [SerializeField] private Color successColor;
        [SerializeField] private Sprite successSprite;
        [SerializeField] private Color failColor;
        [SerializeField] private Sprite failSprite;

        private Animator _animator;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }

        private void Start()
        {
            DeliveryManager.Instance.OnRecipeSuccess += DeliveryManagerOnRecipeSuccess;
            DeliveryManager.Instance.OnRecipeFailed += DeliveryManagerOnRecipeFailed;

            gameObject.SetActive(false);
        }

        private void DeliveryManagerOnRecipeSuccess(object sender, System.EventArgs e)
        {
            gameObject.SetActive(true);
            _animator.CrossFade(Popup, 0f);
            backgroundImage.color = successColor;
            iconImage.sprite = successSprite;
            messageText.text = "DELIVERY\nSUCCESS";
        }

        private void DeliveryManagerOnRecipeFailed(object sender, System.EventArgs e)
        {
            gameObject.SetActive(true);
            _animator.CrossFade(Popup, 0f);
            backgroundImage.color = failColor;
            iconImage.sprite = failSprite;
            messageText.text = "DELIVERY\nFAILED";
        }
    }
}
