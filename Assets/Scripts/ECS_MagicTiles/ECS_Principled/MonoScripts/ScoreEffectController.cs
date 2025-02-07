using System;
using EventChannel;
using PrimeTween;
using UnityEngine;
using UnityEngine.UI;

namespace ECS_MagicTile
{
    public class ScoreEffectController : MonoBehaviour
    {
        [Header("Event Channels")]
        [SerializeField]
        private BoolEventChannel scoreSignalEffectChannel;

        [Header("Main Effect components")]
        [SerializeField]
        private CanvasGroup perfectScorePrefab;

        [SerializeField]
        private CanvasGroup greatScorePrefab;

        [Header("Burst Movement Setup")]
        [SerializeField]
        private BurstMovementUIController burstMovementUIController;

        [SerializeField]
        private BurstMovementUIController.BurstMovementElement[] burstMovementElements;
        private Sequence effectSequence;

        void Awake()
        {
            burstMovementUIController.InitializeElement(burstMovementElements);
        }

        void OnEnable()
        {
            scoreSignalEffectChannel.Subscribe(PlayEffect);
        }

        void OnDisable()
        {
            scoreSignalEffectChannel.Unsubscribe(PlayEffect);
        }

        private void PlayEffect(bool isPerfect)
        {
            effectSequence.Stop();

            burstMovementUIController?.ResetAll();
            burstMovementUIController?.StartAll();
            perfectScorePrefab.alpha = 1;

            for (int i = 1; i < burstMovementElements.Length; i++)
            {
                burstMovementElements[i].target.rotation = Quaternion.Euler(
                    0,
                    0,
                    UnityEngine.Random.Range(0, 360)
                );
            }

            effectSequence = Tween
                .Scale(
                    target: perfectScorePrefab.transform,
                    startValue: Vector3.zero,
                    endValue: Vector3.one,
                    duration: 0.2f,
                    ease: Ease.Linear
                )
                .Chain(
                    Tween
                        .Delay(duration: 0.5f)
                        .Chain(
                            Tween
                                .Alpha(
                                    target: perfectScorePrefab,
                                    startValue: 1f,
                                    endValue: 0f,
                                    duration: 0.5f,
                                    ease: Ease.Linear
                                )
                                .OnComplete(() =>
                                {
                                    burstMovementUIController.StopAll();
                                })
                        )
                );
        }
    }
}
