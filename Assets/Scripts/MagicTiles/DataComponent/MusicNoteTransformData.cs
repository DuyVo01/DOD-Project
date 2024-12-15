using UnityEngine;

public struct MusicNoteTransformData : IDataComponent
{
    public ChunkArray<Vector3> positions;
    public ChunkArray<Vector2> sizes;
    public ChunkArray<Vector2> TopLeft;
    public ChunkArray<Vector2> TopRight;
    public ChunkArray<Vector2> BottomLeft;
    public ChunkArray<Vector2> BottomRight;

    public MusicNoteTransformData(int capacity)
    {
        positions = new ChunkArray<Vector3>(capacity);
        sizes = new ChunkArray<Vector2>(capacity);
        TopLeft = new ChunkArray<Vector2>(capacity);
        TopRight = new ChunkArray<Vector2>(capacity);
        BottomLeft = new ChunkArray<Vector2>(capacity);
        BottomRight = new ChunkArray<Vector2>(capacity);

        for (int entityId = 0; entityId < capacity; entityId++)
        {
            positions.Add(Vector2.zero);
            sizes.Add(Vector2.zero);
            TopLeft.Add(Vector2.zero);
            TopRight.Add(Vector2.zero);
            BottomLeft.Add(Vector2.zero);
            BottomRight.Add(Vector2.zero);
        }
    }
}
