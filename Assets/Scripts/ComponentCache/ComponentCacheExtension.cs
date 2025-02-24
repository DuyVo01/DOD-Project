using ComponentCache.Core;
using UnityEngine;
using UnityEngine.UI;

namespace ComponentCache
{
    public static class ComponentCacheExtensions
    {
        public static int RegisterComponent<T>(this GameObject go, T component)
            where T : Component => ComponentCacheManager.Instance.RegisterComponent(component);

        public static Transform Transform(this GameObject go, int id) =>
            ComponentCacheManager.Instance.GetTransform(id);

        public static Image Image(this GameObject go, int id) =>
            ComponentCacheManager.Instance.GetImage(id);

        public static RectTransform RectTransform(this GameObject go, int id) =>
            ComponentCacheManager.Instance.GetRectTransform(id);
    }
}
