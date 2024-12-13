using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ITransformUpdateSystem
{
    public void SyncTransform(
        ref MusicNoteMidiData musicNoteMidiData,
        ref MusicNoteTransformData musicNoteTransformData
    );
}
