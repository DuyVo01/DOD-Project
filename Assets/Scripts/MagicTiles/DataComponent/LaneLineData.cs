using UnityEngine;

public struct LaneLineData : IDataComponent
{
    public ChunkArray<Vector2> Positions;
    public ChunkArray<Vector2> Sizes;

    public LaneLineData(int capacity)
    {
        Positions = new ChunkArray<Vector2>(capacity);
        Sizes = new ChunkArray<Vector2>(capacity);

        for (int i = 0; i < capacity; i++)
        {
            Positions.Add(Vector2.zero);
            Sizes.Add(Vector2.zero);
        }
    }
}
