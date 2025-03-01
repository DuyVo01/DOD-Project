using ComponentCache.Core;
using UnityEngine;
using UnityEngine.UI;

namespace ComponentCache
{
    public static class ComponentCacheExtensions
    {
        public static int RegisterComponent<T>(this GameObject go, T component)
            where T : Component => ComponentCacheManager.Instance.RegisterComponent(go, component);

        /// <summary>
        /// Unregister all components of a GameObject from the cache
        /// </summary>
        public static void UnregisterFromCache(this GameObject gameObject)
        {
            ComponentCacheManager.Instance.UnregisterGameObject(gameObject);
        }

        public static Transform Transform(this GameObject go) =>
            ComponentCacheManager.Instance.GetTransform(go);

        public static Image Image(this GameObject go) =>
            ComponentCacheManager.Instance.GetImage(go);

        public static RectTransform RectTransform(this GameObject go) =>
            ComponentCacheManager.Instance.GetRectTransform(go);

        public static RawImage RawImage(this GameObject go) =>
            ComponentCacheManager.Instance.GetRawImage(go);

        public static Text Text(this GameObject go) => ComponentCacheManager.Instance.GetText(go);
    }
}
