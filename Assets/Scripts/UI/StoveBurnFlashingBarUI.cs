using CodeMonkey.KitchenChaosControl.KitchenCounters;
using UnityEngine;

namespace CodeMonkey.KitchenChaosControl.UI
{
    public class StoveBurnFlashingBarUI : MonoBehaviour
    {
        private static readonly int IsFlashing = Animator.StringToHash("IsFlashing");

        [SerializeField] private StoveCounter stoveCounter;

        [SerializeField, Range(0f, 1f) ]private float burnShowProgressPercentage;

        private Animator _animator;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }

        private void Start()
        {
            stoveCounter.OnProgressChanged += StoveCounterOnProgressChanged;

            _animator.SetBool(IsFlashing, false);
        }

        private void StoveCounterOnProgressChanged(object sender, IHasProgress.ProgressChangedEventArgs e)
        {
            bool show = e.progressNormalized >= burnShowProgressPercentage && stoveCounter.IsFried;
            _animator.SetBool(IsFlashing, show);
        }
    }
}
