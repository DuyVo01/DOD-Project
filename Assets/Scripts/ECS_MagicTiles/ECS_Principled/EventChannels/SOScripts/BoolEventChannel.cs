using UnityEngine;

namespace EventChannel
{
    [CreateAssetMenu(
        fileName = "sO_BoolEventChannel",
        menuName = "Event Channels/BoolEventChannel"
    )]
    public class BoolEventChannel : EventChannelSO<bool> { }
}
