using CodeMonkey.KitchenCaosControl.KitchenCounters;
using UnityEngine;

namespace CodeMonkey.KitchenCaosControl.Management
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
