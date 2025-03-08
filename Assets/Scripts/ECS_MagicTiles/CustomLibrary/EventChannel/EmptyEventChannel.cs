using UnityEngine;

namespace EventChannel
{
    [CreateAssetMenu(
        fileName = "SO_EmptyEventChannel",
        menuName = "Event Channels/EmptyEventChannel"
    )]
    public class EmptyEventChannel : EventChannelSO<EmptyData> { }
}
