namespace WinRT
{
    internal class WeakLazy<T> where T : class, new()
    {
        private WeakReference<T> _instance = new WeakReference<T>(null);

        public T Value
        {
            get
            {
                lock (_instance)
                {
                    if (!_instance.TryGetTarget(out var target))
                    {
                        target = new T();
                        _instance.SetTarget(target);
                    }

                    return target;
                }
            }
        }
    }
}

