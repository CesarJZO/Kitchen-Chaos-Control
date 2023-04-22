using UnityEngine;

namespace CodeMonkey.KitchenChaosControl.KitchenCounters
{
    [RequireComponent(typeof(Animator))]
    public class CuttingCounterVisual : MonoBehaviour
    {
        private readonly int _cut = Animator.StringToHash("Cut");

        [SerializeField] private CuttingCounter cuttingCounter;

        private Animator _animator;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }

        private void Start()
        {
            cuttingCounter.OnCut += CuttingCounter_OnCut;
        }

        private void CuttingCounter_OnCut(object sender, System.EventArgs e)
        {
            _animator.SetTrigger(_cut);
        }
    }
}
