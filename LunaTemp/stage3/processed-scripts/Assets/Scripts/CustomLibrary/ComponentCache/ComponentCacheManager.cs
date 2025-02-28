using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace ComponentCache.Core
{
    public class ComponentCacheManager : PersistentSingleton<ComponentCacheManager>
    {
        private readonly Dictionary<GameObject, int> gameObjectToId =
            new Dictionary<GameObject, int>();

        private Queue<int> freedIds = new Queue<int>();

        private int highestUsedId = -1;

        // Store caches for different component types
        private readonly DynamicComponentCache<Transform> transformCache =
            new DynamicComponentCache<Transform>();
        private readonly DynamicComponentCache<Image> imageCache =
            new DynamicComponentCache<Image>();
        private readonly DynamicComponentCache<RawImage> rawImageCache =
            new DynamicComponentCache<RawImage>();
        private readonly DynamicComponentCache<RectTransform> rectTransformCache =
            new DynamicComponentCache<RectTransform>();
        private readonly DynamicComponentCache<Button> buttonCache =
            new DynamicComponentCache<Button>();

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
            List<GameObject> objectsToRemove = new List<GameObject>();

            // Find all destroyed GameObjects
            foreach (var entry in gameObjectToId)
            {
                if (entry.Key == null)
                {
                    objectsToRemove.Add(entry.Key);
                    ClearComponentsAtIndex(entry.Value);
                    freedIds.Enqueue(entry.Value);
                }
            }

            // Remove them from the dictionary
            foreach (var obj in objectsToRemove)
            {
                gameObjectToId.Remove(obj);
            }
        }

        /// <summary>
        /// Get or create ID for a GameObject
        /// </summary>
        private int GetOrCreateIdForGameObject(GameObject gameObject)
        {
            if (gameObject == null)
                return -1;

            // If already registered, return existing ID
            if (gameObjectToId.TryGetValue(gameObject, out int existingId))
                return existingId;

            int newId;
            if (freedIds.Count > 0)
            {
                newId = freedIds.Dequeue();
            }
            else
            {
                newId = ++highestUsedId;
            }

            gameObjectToId[gameObject] = newId;
            return newId;
        }

        /// <summary>
        /// Register a component in the cache
        /// </summary>
        public int RegisterComponent<T>(GameObject gameObject, T component)
            where T : Component
        {
            if (gameObject == null || component == null)
                return -1;

            int id = GetOrCreateIdForGameObject(gameObject);

            if (typeof(T) == typeof(Transform))
                transformCache.Set(id, component as Transform);
            else if (typeof(T) == typeof(RectTransform))
                rectTransformCache.Set(id, component as RectTransform);
            else if (typeof(T) == typeof(Image))
                imageCache.Set(id, component as Image);
            else if (typeof(T) == typeof(Button))
                buttonCache.Set(id, component as Button);
            else if (typeof(T) == typeof(RawImage))
                rawImageCache.Set(id, component as RawImage);
            else
                Debug.LogWarning(
                    $"Component type {typeof(T)} is not supported for caching. Add it to ComponentCacheManager."
                );

            return id;
        }

        /// <summary>
        /// Unregister all components for a GameObject
        /// </summary>
        public void UnregisterGameObject(GameObject gameObject)
        {
            if (gameObject == null || !gameObjectToId.TryGetValue(gameObject, out int id))
                return;

            ClearComponentsAtIndex(id);
            gameObjectToId.Remove(gameObject);
            freedIds.Enqueue(id);
        }

        private void ClearComponentsAtIndex(int id)
        {
            transformCache.Clear(id);
            rectTransformCache.Clear(id);
            imageCache.Clear(id);
            buttonCache.Clear(id);
            rawImageCache.Clear(id);
            // Clear other caches as needed
        }

        /// <summary>
        /// Get a component from the cache
        /// </summary>
        public T GetComponent<T>(GameObject gameObject)
            where T : Component
        {
            if (gameObject == null || !gameObjectToId.TryGetValue(gameObject, out int id))
                return null;

            if (typeof(T) == typeof(Transform))
                return transformCache.Get(id) as T;
            else if (typeof(T) == typeof(RectTransform))
                return rectTransformCache.Get(id) as T;
            else if (typeof(T) == typeof(Image))
                return imageCache.Get(id) as T;
            else if (typeof(T) == typeof(Button))
                return buttonCache.Get(id) as T;
            else if (typeof(T) == typeof(RawImage))
                return rawImageCache.Get(id) as T;

            return null;
        }

        // Specific getters for common components
        public Transform GetTransform(GameObject gameObject)
        {
            if (gameObject == null || !gameObjectToId.TryGetValue(gameObject, out int id))
                return null;

            return transformCache.Get(id);
        }

        public RectTransform GetRectTransform(GameObject gameObject)
        {
            if (gameObject == null || !gameObjectToId.TryGetValue(gameObject, out int id))
                return null;

            return rectTransformCache.Get(id);
        }

        public Image GetImage(GameObject gameObject)
        {
            if (gameObject == null || !gameObjectToId.TryGetValue(gameObject, out int id))
                return null;

            return imageCache.Get(id);
        }

        public Button GetButton(GameObject gameObject)
        {
            if (gameObject == null || !gameObjectToId.TryGetValue(gameObject, out int id))
                return null;

            return buttonCache.Get(id);
        }

        public RawImage GetRawImage(GameObject gameObject)
        {
            if (gameObject == null || !gameObjectToId.TryGetValue(gameObject, out int id))
                return null;

            return rawImageCache.Get(id);
        }

        // Optional: Scene transition cleanup
        public void OnSceneUnloaded()
        {
            transformCache.TrimExcess();
            imageCache.TrimExcess();
            rectTransformCache.TrimExcess();
            buttonCache.TrimExcess();
            rawImageCache.TrimExcess();
        }
    }
}
