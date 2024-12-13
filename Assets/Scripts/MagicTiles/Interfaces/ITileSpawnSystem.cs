using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ITileSpawnSystem
{
    public void SpawnTileNote(ref MusicNoteMidiData musicNoteMidiData);
}
