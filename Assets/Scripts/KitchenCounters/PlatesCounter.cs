using System;
using CodeMonkey.KitchenCaosControl.ScriptableObjects;
using UnityEngine;

namespace CodeMonkey.KitchenCaosControl.KitchenCounters
{
    public class PlatesCounter : Counter
    {
        public event EventHandler OnPlateSpawned;
        public event EventHandler OnPlateRemoved;

        [SerializeField] private KitchenObjectData plateKitchenObjectData;
        [SerializeField] private float spawnTime;
        [SerializeField] private int platesSpawnAmountMax;

        private float _spawnPlateTimer;
        private int _platesSpawnAmount;

        private void Update()
        {
            _spawnPlateTimer += Time.deltaTime;
            if (_spawnPlateTimer <= spawnTime) return;

            _spawnPlateTimer = 0f;
            if (_platesSpawnAmount >= platesSpawnAmountMax) return;

            _platesSpawnAmount++;
            OnPlateSpawned?.Invoke(this, EventArgs.Empty);
        }

        public override void Interact(Player player)
        {
            if (player.HasKitchenObject()) return;

            if (_platesSpawnAmount <= 0) return;

            _platesSpawnAmount--;
            KitchenObject.SpawnKitchenObject(plateKitchenObjectData, player);
            OnPlateRemoved?.Invoke(this, EventArgs.Empty);
        }
    }
}
