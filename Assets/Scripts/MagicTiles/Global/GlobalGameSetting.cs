using System;
using UnityEngine;

public class GlobalGameSetting : PersistentSingleton<GlobalGameSetting>
{
    [Header("Global Game Settings")]
    public GeneralGameSettingSO generalSetting;
    public DataSystemSettingSO dataSystemSetting;
    public PerfectLineSettingSO perfectLineSettingSO;
    public PresenterSettingSO presenterSetting;

    [Header("Music Note")]
    public Transform notePresenterParent;

    [Header("Input Debugger")]
    public Transform inputPresenterParent;

    protected override void OnAwake()
    {
        #region Systems registration
        SystemRepository.RegisterSystem(new TileSpawnSystem());
        SystemRepository.RegisterSystem(new TransformUpdateSystem());
        SystemRepository.RegisterSystem(new MovingTileSystem());
        SystemRepository.RegisterSystem(new NoteCornerUpdateSystem());
        SystemRepository.RegisterSystem(new NoteStateSystem());
        SystemRepository.RegisterSystem(new InputSystem());
        SystemRepository.RegisterSystem(new InputCollisionSystem());
        #endregion

        #region Entities and data components registration

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
        musicNoteEntityGroup.RegisterComponent(
            MusicNoteComponentType.MusicNoteFiller,
            new MusicNoteFillerData(musicNoteEntityGroup.EntityCount)
        );

        EntityRepository.RegisterEGroup(EntityType.NoteEntityGroup, ref musicNoteEntityGroup);

        #endregion

        #region Singleton registration
        SingletonComponentRepository.RegisterComponent(
            SingletonComponentType.PerfectLine,
            new PerfectLineData(
                perfectLineSettingSO.TopLeft,
                perfectLineSettingSO.TopRight,
                perfectLineSettingSO.BottomLeft,
                perfectLineSettingSO.BottomRight
            )
        );

        SingletonComponentRepository.RegisterComponent(
            SingletonComponentType.Input,
            new InputDataComponent(2)
        );
        #endregion

        #region Presenters registration


        PresenterManagerRepository.RegisterManager(
            PresenterManagerType.MusicNotePresenterManager,
            new PresenterManager(
                musicNoteEntityGroup.EntityCount,
                notePresenterParent,
                presenterSetting.musicNotePresenterPrefab
            )
        );

        PresenterManagerRepository.RegisterManager(
            PresenterManagerType.InputDebuggerPresenterManager,
            new PresenterManager(
                dataSystemSetting.defaultCapacity,
                inputPresenterParent,
                presenterSetting.inputDebuggerPresenterPrefab
            )
        );
        #endregion

        #region bridges registration
        BridgeRepository.RegisterBridge(BridgeType.NoteTransform, UnityTransformBridge.Create());
        BridgeRepository.RegisterBridge(BridgeType.InputDebugger, new InputDebuggerBridge());
        #endregion
        GizmoDebugger.Instance.InitData(musicNoteEntityGroup.EntityCount);
    }

    private void OnDestroy()
    {
        SystemRepository.Clear();
        EntityRepository.Clear();
    }
}
