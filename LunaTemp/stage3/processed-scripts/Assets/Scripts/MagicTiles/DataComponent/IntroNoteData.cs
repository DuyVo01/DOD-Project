using UnityEngine;

public struct IntroNoteData : IDataComponent
{
    public Vector2 Position;
    public bool isActive;

    public IntroNoteData(bool isActive)
    {
        Position = Vector2.zero;
        this.isActive = isActive;
    }
}
