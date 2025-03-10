using System;

namespace ECS_MagicTile
{
    /// <summary>
    /// Custom delegates that support ref parameters for component processing
    /// </summary>
    public static class DelegateTypes
    {
        // Single component delegates
        public delegate void ActionRef<T>(ref T item);
        public delegate void ActionRefWithEntity<T>(ref T item, int entityId);
        
        // Two component delegates
        public delegate void ActionRef<T1, T2>(ref T1 item1, ref T2 item2);
        public delegate void ActionRefWithEntity<T1, T2>(ref T1 item1, ref T2 item2, int entityId);
        
        // Three component delegates
        public delegate void ActionRef<T1, T2, T3>(ref T1 item1, ref T2 item2, ref T3 item3);
        public delegate void ActionRefWithEntity<T1, T2, T3>(ref T1 item1, ref T2 item2, ref T3 item3, int entityId);
        
        // Block processing delegates (without Span)
        public delegate void BlockAction<T>(T[] components, int[] entityIds, int startIndex, int count);
        public delegate void BlockAction<T1, T2>(T1[] components1, T2[] components2, int[] entityIds, int startIndex, int count);
        public delegate void BlockAction<T1, T2, T3>(T1[] components1, T2[] components2, T3[] components3, int[] entityIds, int startIndex, int count);
    }
}
