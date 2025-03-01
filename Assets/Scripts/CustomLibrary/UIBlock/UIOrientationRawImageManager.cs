using System.Collections.Generic;
using EventChannel;
using UnityEngine;
using UnityEngine.UI;

namespace UIBlock
{
    public class UIOrientationRawImageManager : PersistentSingleton<UIOrientationRawImageManager>
    {
        [SerializeField]
        private BoolEventChannel orientationChangedChannel;

        private List<Background> _backgrounds = new List<Background>();

        private bool isPortrait
        {
            get { return Screen.height > Screen.width; }
        }

        // Public getter with lazy initialization and thread safety


        protected override void OnAwake()
        {
            orientationChangedChannel.Subscribe(OnOrientationChange);
            UpdateBackgrounds();
        }

        void OnOrientationChange(bool isPortrait)
        {
            UpdateBackgrounds();
        }

        public void RegisterBackground(
            RawImage rawImage,
            Texture2D texture2DPortarait,
            Texture2D texture2DLandscape
        )
        {
            _backgrounds.Add(
                new Background
                {
                    rawImage = rawImage,
                    texture2DPortrait = texture2DPortarait,
                    texture2DLandscape = texture2DLandscape,
                }
            );

            UpdateBackgrounds();
        }

        public void UnregisterBackground(RawImage rawImage)
        {
            for (int i = 0; i < _backgrounds.Count; i++)
            {
                if (_backgrounds[i].rawImage == rawImage)
                {
                    _backgrounds.RemoveAt(i);
                    return;
                }
            }
        }

        private void UpdateBackgrounds()
        {
            for (int i = 0; i < _backgrounds.Count; i++)
            {
                Background background = _backgrounds[i];
                background.rawImage.texture = isPortrait
                    ? background.texture2DPortrait
                    : background.texture2DLandscape;
            }
        }

        [System.Serializable]
        public struct Background
        {
            public RawImage rawImage;
            public Texture2D texture2DPortrait;
            public Texture2D texture2DLandscape;
        }
    }
}
