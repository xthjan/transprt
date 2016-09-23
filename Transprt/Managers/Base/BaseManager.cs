using System;

namespace Transprt.Managers.Base {
    public abstract class BaseManager<T> where T : new() {
        private static readonly Lazy<T> lazy = new Lazy<T>(() => new T());
        public static T Instance { get { return lazy.Value; } }
    }
}
