using System;
using Transprt.Data.Identity;

namespace Transprt.Managers.Base {
    public abstract class BaseManager<T> where T : new() {
        protected static IdentityDBContext DBContextIdentity => new IdentityDBContext();

        private static readonly Lazy<T> lazy = new Lazy<T>(() => new T());
        public static T Instance { get { return lazy.Value; } }
    }
}
