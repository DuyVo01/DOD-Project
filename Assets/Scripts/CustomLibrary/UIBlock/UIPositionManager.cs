using UnityEngine;

namespace UIBlock
{
    public class UIPositionManager : UITransformManagerBase<UIPositionManager>
    {
        private UIPositionPreset[] positionPresets;

        protected override void Awake()
        {
            base.Awake();
            positionPresets = new UIPositionPreset[blockTransforms.Length];
        }

        public void RegisterBlock(RectTransform block, UIPositionPreset preset)
        {
            int newIndex = RegisterTransform(block);

            if (newIndex == -1)
                return;

            positionPresets[newIndex] = preset;
            RepositionSingleBlock(newIndex);
            Debug.Log("Block registered at index " + newIndex);
        }

        public void UnregisterBlock(RectTransform block)
        {
            int index = UnregisterTransform(block);
            if (index == -1)
                return;

            // Clear both transform and preset data
            positionPresets[index] = default; // Clear the preset data
        }

        protected override void ExpandArrays()
        {
            base.ExpandArrays();
            System.Array.Resize(ref positionPresets, blockTransforms.Length);
        }

        protected override void OnDeviceConfigurationChanged()
        {
            UpdateAllBlockPositions();
        }

        private void UpdateAllBlockPositions()
        {
            for (int i = 0; i < trackingIndex; i++)
            {
                if (holeIndices.Contains(i))
                    continue;
                if (blockTransforms[i] != null)
                {
                    RepositionSingleBlock(i);
                }
            }
        }

        private void RepositionSingleBlock(int index)
        {
            var rect = blockTransforms[index];
            var preset = positionPresets[index];

            Vector2 targetPosition = isTablet
                ? (isPortrait ? preset.IpadPortraitPosition : preset.IpadLandscapePosition)
                : (isPortrait ? preset.PhonePortraitPosition : preset.PhoneLandscapePosition);

            if (rect.anchoredPosition != targetPosition)
            {
                rect.anchoredPosition = targetPosition;
            }
        }
    }

    [System.Serializable]
    public struct UIPositionPreset
    {
        public Vector2 PhonePortraitPosition;
        public Vector2 PhoneLandscapePosition;
        public Vector2 IpadPortraitPosition;
        public Vector2 IpadLandscapePosition;
    }
}
