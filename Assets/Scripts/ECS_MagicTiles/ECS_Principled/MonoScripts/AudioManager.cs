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

        private int eventListenerId;

        void Start()
        {
            audioSource = GetComponent<AudioSource>();
            audioSource.clip = audioClip;
        }

        void OnEnable()
        {
            eventListenerId = onSongStartChannel.Subscribe(
                target: this,
                (target, data) => OnGameStart(data)
            );
        }

        void onDisable()
        {
            onSongStartChannel.Unsubscribe(eventListenerId);
        }

        private void OnGameStart(EmptyData _)
        {
            audioSource.PlayWithFadeIn(this, .8f);
        }
    }
}
