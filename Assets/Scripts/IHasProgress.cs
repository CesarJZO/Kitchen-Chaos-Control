using System;

namespace CodeMonkey.KitchenCaosControl
{
    public interface IHasProgress
    {
        public event EventHandler<OnProgressChangedEventArgs> OnProgressChanged;
        public class OnProgressChangedEventArgs : EventArgs
        {
            public float progressNormalized;
        }
    }
}
