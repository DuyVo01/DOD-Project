using UnityEngine;

namespace UIBlock
{
    public class UIScaleManager : UITransformManagerBase<UIScaleManager>
    {
        private UIScalePreset[] scalePresets;

        protected override void Awake()
        {
            base.Awake();
            scalePresets = new UIScalePreset[blockTransforms.Length];
        }

        public void RegisterBlock(RectTransform block, UIScalePreset preset)
        {
            int newIndex = RegisterTransform(block);
            if (newIndex == -1)
                return;
            scalePresets[newIndex] = preset;
            RescaleSingleBlock(newIndex);
        }

        // In UIScaleManager
        public void UnregisterBlock(RectTransform block)
        {
            int index = UnregisterTransform(block);

            if (index == -1)
                return;

            scalePresets[index] = default; // Clear the preset data
        }

        protected override void ExpandArrays()
        {
            base.ExpandArrays();
            System.Array.Resize(ref scalePresets, blockTransforms.Length);
        }

        protected override void OnDeviceConfigurationChanged()
        {
            UpdateAllBlockScales();
        }

        private void UpdateAllBlockScales()
        {
            for (int i = 0; i < trackingIndex; i++)
            {
                if (holeIndices.Contains(i))
                    continue;
                if (blockTransforms[i] != null)
                {
                    RescaleSingleBlock(i);
                }
            }
        }

        private void RescaleSingleBlock(int index)
        {
            var rect = blockTransforms[index];
            var preset = scalePresets[index];

            Vector2 targetScale = isTablet
                ? (isPortrait ? preset.IpadPortraitScale : preset.IpadLandscapeScale)
                : (isPortrait ? preset.PhonePortraitScale : preset.PhoneLandscapeScale);

            if (rect.localScale != (Vector3)targetScale)
            {
                rect.localScale = new Vector3(targetScale.x, targetScale.y, 1f);
            }
        }
    }

    public struct UIScalePreset
    {
        public Vector2 PhonePortraitScale;
        public Vector2 PhoneLandscapeScale;
        public Vector2 IpadPortraitScale;
        public Vector2 IpadLandscapeScale;
    }
}
