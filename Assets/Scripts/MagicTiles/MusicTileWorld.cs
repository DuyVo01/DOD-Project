using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicTileWorld
{
    //Data components
    private MusicNoteMidiData musicNoteMidiData;
    private MusicNoteTransformData musicNoteTransformData;

    //System components
    private TileSpawnSystem tileSpawnSystem;
    private TransformUpdateSystem transformUpdateSystem;

    private UnityTransformBridge unityTransformBridge;

    private int addedNoteCount;

    public MusicTileWorld(int capacity, Transform parent, GameObject tilePrefab)
    {
        musicNoteMidiData = new MusicNoteMidiData(capacity);
        musicNoteTransformData = new MusicNoteTransformData(capacity);
        tileSpawnSystem = new TileSpawnSystem();
        transformUpdateSystem = new TransformUpdateSystem();
        unityTransformBridge = new UnityTransformBridge(parent, tilePrefab);
        addedNoteCount = 0;
    }

    public void AddNote(int id, int posId, float timeAppears)
    {
        musicNoteMidiData.ids[addedNoteCount] = id;
        musicNoteMidiData.pos_ids[addedNoteCount] = posId;
        musicNoteMidiData.time_appears[addedNoteCount] = timeAppears;
        musicNoteMidiData.positions[addedNoteCount] = Vector2.zero;

        musicNoteTransformData.entityIDs.Add(addedNoteCount);
        musicNoteTransformData.positions.Add(Vector2.zero);
        musicNoteTransformData.count++;

        addedNoteCount++;

        tileSpawnSystem.SpawnTile(
            musicNoteMidiData.pos_ids,
            musicNoteMidiData.time_appears,
            ref musicNoteMidiData.positions
        );
    }

    public void Update()
    {
        transformUpdateSystem.SyncTransform(ref musicNoteMidiData, ref musicNoteTransformData);

        unityTransformBridge.SyncToUnity(ref musicNoteTransformData);
    }

    public void Cleanup()
    {
        unityTransformBridge.Cleanup();
    }
}
