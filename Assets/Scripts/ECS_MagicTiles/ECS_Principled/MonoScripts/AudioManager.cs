using System;
using EventChannel;
using UnityEngine;

namespace ECS_MagicTile
{
    public class AudioManager : MonoBehaviour
    {
        [SerializeField]
        private IntEventChannel onGameStartChannel;

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
            onGameStartChannel.Subscribe(OnGameStart);
        }

        private void OnGameStart(int obj)
        {
            audioSource.Play();
        }
    }
}
