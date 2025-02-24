using EventChannel;
using UnityEngine;

namespace ECS_MagicTile
{
    public class AudioManager : MonoBehaviour
    {
        [SerializeField]
        private EmptyEventChannel onSongStartChannel;

        [SerializeField]
        private AudioClip audioClip;

        private AudioSource audioSource;

        void Start()
        {
            audioSource = GetComponent<AudioSource>();
            audioSource.clip = audioClip;
        }

        void OnEnable()
        {
            onSongStartChannel.Subscribe(OnGameStart);
        }

        private void OnGameStart([Bridge.Ref] EmptyData data)
        {
            audioSource.PlayWithFadeIn(this, .8f);
        }
    }
}
