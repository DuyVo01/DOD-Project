using System;
using UnityEngine;

public class GlobalGameSetting : PersistentSingleton<GlobalGameSetting>
{
    [Header("Global Game Settings")]
    public GeneralGameSettingSO generalSetting;
    public DataSystemSettingSO dataSystemSetting;
    public PerfectLineSettingSO perfectLineSettingSO;

    [Header("Music Note")]
    public PresenterTemplateSO musicNotePresenter;
    public Transform presenterParent;

    protected override void OnAwake()
    {
        SystemRepository.RegisterSystem(new TileSpawnSystem());
        SystemRepository.RegisterSystem(new TransformUpdateSystem());
        SystemRepository.RegisterSystem(new MovingTileSystem());
        SystemRepository.RegisterSystem(new NoteCornerUpdateSystem());
        SystemRepository.RegisterSystem(new NoteStateSystem());

        MusicNoteMidiData musicNoteMidiData = MidiNoteParser.ParseFromText(
            generalSetting.midiContent.text
        );

        var musicNoteEntityGroup = new EntityGroup<MusicNoteComponentType>(
            musicNoteMidiData.TotalNotes
        );

        musicNoteEntityGroup.RegisterComponent(
            MusicNoteComponentType.MusicNoteMidiData,
            musicNoteMidiData
        );
        musicNoteEntityGroup.RegisterComponent(
            MusicNoteComponentType.MusicNoteTransformData,
            new MusicNoteTransformData(musicNoteEntityGroup.EntityCount)
        );
        musicNoteEntityGroup.RegisterComponent(
            MusicNoteComponentType.MusicNoteStateData,
            new MusicNoteStateData(musicNoteEntityGroup.EntityCount)
        );

        EntityRepository.RegisterEGroup(EntityType.NoteEntityGroup, ref musicNoteEntityGroup);

        BridgeRepository.RegisterBridge(BridgeType.NoteTransform, new UnityTransformBridge());

        SingletonComponentRepository.RegisterComponent(
            SingletonComponentType.MusicNotePresenterManager,
            new PresenterManager(
                musicNoteEntityGroup.EntityCount,
                presenterParent,
                musicNotePresenter
            )
        );

        SingletonComponentRepository.RegisterComponent(
            SingletonComponentType.PerfectLine,
            new PerfectLineData(
                perfectLineSettingSO.TopLeft,
                perfectLineSettingSO.TopRight,
                perfectLineSettingSO.BottomLeft,
                perfectLineSettingSO.BottomRight
            )
        );
    }

    private void OnDestroy()
    {
        SystemRepository.Clear();
        EntityRepository.Clear();
    }
}
