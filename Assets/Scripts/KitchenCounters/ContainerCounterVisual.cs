using System;
using UnityEngine;

namespace CodeMonkey.KitchenChaosControl.KitchenCounters
{
    [RequireComponent(typeof(Animator))]
    public class ContainerCounterVisual : MonoBehaviour
    {
        [SerializeField] private ContainerCounter containerCounter;

        private Animator _animator;
        private readonly int _openClose = Animator.StringToHash("OpenClose");

        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }

        private void Start()
        {
            containerCounter.OnPlayerGrabbedObject += ContainerCounter_OnPlayerGrabbedObject;
        }

        private void ContainerCounter_OnPlayerGrabbedObject(object sender, EventArgs e)
        {
            _animator.SetTrigger(_openClose);
        }
    }
}
