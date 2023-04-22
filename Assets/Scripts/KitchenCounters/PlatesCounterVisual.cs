using System;
using System.Collections.Generic;
using UnityEngine;

namespace CodeMonkey.KitchenChaosControl.KitchenCounters
{
    public class PlatesCounterVisual : MonoBehaviour
    {
        [SerializeField] private PlatesCounter platesCounter;
        [SerializeField] private Transform counterTopPoint;
        [SerializeField] private Transform plateVisualPrefab;

        private Stack<GameObject> _plateVisualStack;

        private void Awake()
        {
            _plateVisualStack = new Stack<GameObject>();
        }

        private void Start()
        {
            platesCounter.OnPlateSpawned += PlatesCounter_OnPlateSpawned;
            platesCounter.OnPlateRemoved += PlatesCounter_OnPlateRemoved;
        }

        private void PlatesCounter_OnPlateRemoved(object sender, EventArgs e)
        {
            var plateVisual = _plateVisualStack.Pop();
            Destroy(plateVisual);
        }

        private void PlatesCounter_OnPlateSpawned(object sender, EventArgs e)
        {
            var plateVisual = Instantiate(plateVisualPrefab, counterTopPoint);

            const float plateOffsetY = 0.1f;
            plateVisual.localPosition = new Vector3(0, plateOffsetY * _plateVisualStack.Count, 0);
            _plateVisualStack.Push(plateVisual.gameObject);
        }
    }
}
