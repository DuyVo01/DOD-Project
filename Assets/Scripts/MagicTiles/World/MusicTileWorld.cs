using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicTileWorld
{
    private UnityTransformBridge unityTransformBridge;

    private bool hasDoneInitialize;

    ITileSpawnSystem tileSpawnSystem;
    ITransformUpdateSystem transformUpdateSystem;

    public MusicTileWorld(Transform parent, GameObject tilePrefab)
    {
        unityTransformBridge = new UnityTransformBridge(parent, tilePrefab);

        hasDoneInitialize = false;

        tileSpawnSystem = SystemRepository.GetSystem<TileSpawnSystem>();
        transformUpdateSystem = SystemRepository.GetSystem<TransformUpdateSystem>();
    }

    public void PopulateNoteData(string midiContent)
    {
        ref var musicNoteMidiData = ref DataComponentRepository.GetData<MusicNoteMidiData>();
        ref var musicNoteTransformData =
            ref DataComponentRepository.GetData<MusicNoteTransformData>();

        musicNoteMidiData = MidiNoteParser.ParseFromText(midiContent);

        Debug.Log($"Loaded {musicNoteMidiData.TotalNotes} notes");

        for (int i = 0; i < musicNoteMidiData.TotalNotes; i++)
        {
            musicNoteTransformData.entityIDs.Add(i);
            musicNoteTransformData.positions.Add(Vector2.zero);
        }
        musicNoteTransformData.count = musicNoteMidiData.TotalNotes;

        tileSpawnSystem.SpawnTileNote(ref musicNoteMidiData);

        SyncUp();
        hasDoneInitialize = true;
    }

    public void Update()
    {
        if (!hasDoneInitialize)
        {
            return;
        }

        SyncUp();
    }

    public void Cleanup()
    {
        unityTransformBridge.Cleanup();
    }

    private void SyncUp()
    {
        transformUpdateSystem.SyncTransform(
            ref DataComponentRepository.GetData<MusicNoteMidiData>(),
            ref DataComponentRepository.GetData<MusicNoteTransformData>()
        );

        unityTransformBridge.SyncToUnity(
            ref DataComponentRepository.GetData<MusicNoteTransformData>()
        );
    }
}
