using CodeMonkey.KitchenChaosControl.KitchenCounters;
using UnityEngine;

namespace CodeMonkey.KitchenChaosControl.Management
{
    public class ResetStaticDataManager : MonoBehaviour
    {
        private void Awake()
        {
            Counter.ResetStaticData();
            CuttingCounter.ResetStaticData();
            TrashCounter.ResetStaticData();
        }
    }
}
