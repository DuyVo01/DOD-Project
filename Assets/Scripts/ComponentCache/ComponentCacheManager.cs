using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace ComponentCache.Core
{
    public class ComponentCacheManager : PersistentSingleton<ComponentCacheManager>
    {
        // Store caches for different component types
        private readonly DynamicComponentCache<Transform> transformCache = new();
        private readonly DynamicComponentCache<Image> imageCache = new();
        private readonly DynamicComponentCache<RectTransform> rectTransformCache = new();

        // Add more component types as needed

        protected override void OnAwake()
        {
            base.OnAwake();

            SceneManager.sceneUnloaded += OnSceneUnloaded;

            // // Find all objects that need caching
            // ICachedID[] cachedObjects = FindObjectsByType<MonoBehaviour>(FindObjectsSortMode.None)
            //     .OfType<ICachedID>()
            //     .ToArray();

            // // Assign IDs and cache initial components
            // for (int i = 0; i < cachedObjects.Length; i++)
            // {
            //     cachedObjects[i].Id = i;
            // }
        }

        private void OnSceneUnloaded(Scene scene)
        {
            // Clean up references for destroyed objects
            // This is important to prevent memory leaks
            CleanupDestroyedReferences();
        }

        private void CleanupDestroyedReferences()
        {
            // Implementation to check and clean up destroyed objects
            // This could be called periodically or on scene changes
        }

        // Generic registration method
        public int RegisterComponent<T>(T component)
            where T : Component
        {
            if (typeof(T) == typeof(Transform))
                return transformCache.Set(component as Transform);
            else if (typeof(T) == typeof(Image))
                return imageCache.Set(component as Image);
            else if (typeof(T) == typeof(RectTransform))
                return rectTransformCache.Set(component as RectTransform);
            // Add more types as needed

            return -1;
        }

        public void UnregisterComponent<T>(int id)
            where T : Component
        {
            if (typeof(T) == typeof(Transform))
                transformCache.Unregister(id);
            else if (typeof(T) == typeof(Image))
                imageCache.Unregister(id);
            else if (typeof(T) == typeof(RectTransform))
                rectTransformCache.Unregister(id);
            // Add more types as needed
        }

        // Getter methods
        public Transform GetTransform(int id) => transformCache.Get(id);

        public Image GetImage(int id) => imageCache.Get(id);

        public RectTransform GetRectTransform(int id) => rectTransformCache.Get(id);

        // Optional: Scene transition cleanup
        public void OnSceneUnloaded()
        {
            transformCache.TrimExcess();
            imageCache.TrimExcess();
            rectTransformCache.TrimExcess();
        }
    }
}
