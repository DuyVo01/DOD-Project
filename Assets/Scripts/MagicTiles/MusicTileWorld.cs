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
    private MovingTileSystem movingTileSystem;

    private UnityTransformBridge unityTransformBridge;

    private bool hasDoneInitialize;

    public MusicTileWorld(int capacity, Transform parent, GameObject tilePrefab)
    {
        musicNoteMidiData = new MusicNoteMidiData(capacity);
        musicNoteTransformData = new MusicNoteTransformData(capacity);

        //System Initialize
        tileSpawnSystem = new TileSpawnSystem();
        transformUpdateSystem = new TransformUpdateSystem();
        movingTileSystem = new MovingTileSystem();

        unityTransformBridge = new UnityTransformBridge(parent, tilePrefab);

        hasDoneInitialize = false;
    }

    public void PopulateNoteData(string midiContent)
    {
        musicNoteMidiData = MidiNoteParser.ParseFromText(midiContent);
        Debug.Log($"Loaded {musicNoteMidiData.TotalNotes} notes");

        for (int i = 0; i < musicNoteMidiData.TotalNotes; i++)
        {
            musicNoteTransformData.entityIDs.Add(i);
            musicNoteTransformData.positions.Add(Vector2.zero);
        }
        musicNoteTransformData.count = musicNoteMidiData.TotalNotes;

        tileSpawnSystem.SpawnTile(
            musicNoteMidiData.PositionIds,
            musicNoteMidiData.TimeAppears,
            ref musicNoteMidiData.Positions
        );

        SyncUp();
        hasDoneInitialize = true;
    }

    public void Update()
    {
        if (!hasDoneInitialize)
        {
            return;
        }

        movingTileSystem.MovingTile(ref musicNoteMidiData);
        SyncUp();
    }

    public void Cleanup()
    {
        unityTransformBridge.Cleanup();
    }

    private void SyncUp()
    {
        transformUpdateSystem.SyncTransform(ref musicNoteMidiData, ref musicNoteTransformData);

        unityTransformBridge.SyncToUnity(ref musicNoteTransformData);
    }
}
