using UnityEngine;

namespace CodeMonkey.KitchenChaosControl.Management
{
    public class LoaderCallback : MonoBehaviour
    {
        private bool _isFirstUpdate = true;

        private void Update()
        {
            if (!_isFirstUpdate) return;

            _isFirstUpdate = false;

            Loader.LoaderCallback();
        }
    }
}
