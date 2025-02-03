using UnityEngine;

namespace EventChannel
{
    [CreateAssetMenu(
        fileName = "SO_ScoreSignalEffectChannel",
        menuName = "Event Channels/Score Signal Effect Channel"
    )]
    public class BoolEventChannel : EventChannelSO<bool> { }
}
