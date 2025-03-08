using Unity.Burst.Intrinsics;
using UnityEngine;

namespace UIBlock
{
    public static class UIBlockExtension
    {
        public static void RegisterBlock(
            this RectTransform block,
            Vector2 phonePotraitPos,
            Vector2 phoneLandscapePos,
            Vector2 tabletPotraitPos,
            Vector2 tabletLandscapePos
        )
        {
            UIPositionManager.Instance.RegisterBlock(
                block,
                new UIPositionPreset
                {
                    PhonePortraitPosition = phonePotraitPos,
                    PhoneLandscapePosition = phoneLandscapePos,
                    IpadPortraitPosition = tabletPotraitPos,
                    IpadLandscapePosition = tabletLandscapePos,
                }
            );
        }
    }
}
