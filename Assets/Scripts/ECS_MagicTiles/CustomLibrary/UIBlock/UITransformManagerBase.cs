using System.Collections.Generic;
using EventChannel;
using UnityEngine;

namespace UIBlock
{
    public abstract class UITransformManagerBase<T> : PersistentSingleton<T>
        where T : Component
    {
        const int INITIAL_CAPACITY = 32;

        [SerializeField]
        private BoolEventChannel onOrientationChannel;

        [SerializeField]
        protected float tabletAspectRatioThreshold = 1.5f;
        protected RectTransform[] blockTransforms = new RectTransform[INITIAL_CAPACITY];
        protected int trackingIndex = 0;
        protected Queue<int> holeIndices = new Queue<int>();

        protected bool isTablet;
        protected bool isPortrait
        {
            get { return Screen.height > Screen.width; }
        }

        private int eventListenerId;

        protected override void OnAwake()
        {
            eventListenerId = onOrientationChannel.Subscribe(
                target: this,
                (target, data) => OnOrientationChange(data)
            );

            UpdateDeviceInfo();
        }

        protected override void OnDestroy()
        {
            onOrientationChannel.Unsubscribe(eventListenerId);
        }

        void OnOrientationChange(bool isPortrait)
        {
            OnDeviceConfigurationChanged();
        }

        protected int RegisterTransform(RectTransform block)
        {
            if (block == null)
                return -1;

            // Check for existing registration
            int existingIndex = FindTransformIndex(block);
            if (existingIndex != -1)
            {
                return existingIndex;
            }

            // Try to fill a hole first
            if (holeIndices.Count > 0)
            {
                int holeIndex = holeIndices.Dequeue();

                blockTransforms[holeIndex] = block;
                return holeIndex;
            }

            // No holes, add to the end
            if (trackingIndex >= blockTransforms.Length)
            {
                ExpandArrays();
            }

            blockTransforms[trackingIndex] = block;
            trackingIndex++;
            return trackingIndex - 1;
        }

        protected int UnregisterTransform(RectTransform block)
        {
            if (block == null)
                return -1;

            int index = FindTransformIndex(block);

            if (index == -1)
                return -1;

            // Clear the spot and mark it as a hole
            blockTransforms[index] = null;
            holeIndices.Enqueue(index);

            return index;
        }

        protected void UpdateDeviceInfo()
        {
            // Calculate aspect ratio using max/min for consistency
            float largerDimension = Mathf.Max(Screen.width, Screen.height);
            float smallerDimension = Mathf.Min(Screen.width, Screen.height);
            float aspectRatio = largerDimension / smallerDimension;

            // Tablets typically have aspect ratios closer to 1:1
            // Phones typically have more extreme aspect ratios
            bool newIsTablet = aspectRatio < tabletAspectRatioThreshold;

            if (isTablet != newIsTablet)
            {
                isTablet = newIsTablet;
                OnDeviceConfigurationChanged();
            }
        }

        // Template method for derived classes to implement
        protected abstract void OnDeviceConfigurationChanged();

        // Utility methods
        protected virtual void ExpandArrays()
        {
            int newSize = blockTransforms.Length * 2;
            System.Array.Resize(ref blockTransforms, newSize);
        }

        protected int FindTransformIndex(RectTransform transform)
        {
            for (int i = 0; i < trackingIndex; i++)
            {
                if (blockTransforms[i] == transform && !holeIndices.Contains(i))
                {
                    return i;
                }
            }
            return -1;
        }
    }
}
