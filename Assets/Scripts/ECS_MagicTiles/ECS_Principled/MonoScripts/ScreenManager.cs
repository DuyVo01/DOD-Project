using EventChannel;
using Unity.VisualScripting;
using UnityEngine;

namespace ECS_MagicTile
{
    public class ScreenManager : MonoBehaviour
    {
        [SerializeField]
        private BoolEventChannel OnOrientationChange;

        public bool IsPortrait { get; private set; }

        void Start()
        {
            IsPortrait = Screen.currentResolution.width > Screen.currentResolution.height;
        }

        private void Update()
        {
            if (Screen.currentResolution.width < Screen.currentResolution.height)
            {
                if (!IsPortrait)
                {
                    IsPortrait = true;
                    OnOrientationChange.RaiseEvent(true);
                }
            }
            else
            {
                if (IsPortrait)
                {
                    IsPortrait = false;
                    OnOrientationChange.RaiseEvent(false);
                }
            }
        }
    }
}
