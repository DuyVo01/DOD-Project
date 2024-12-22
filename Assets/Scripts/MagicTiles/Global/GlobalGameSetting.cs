using System;
using UnityEngine;

public class GlobalGameSetting : PersistentSingleton<GlobalGameSetting>
{
    [Header("Global Game Settings")]
    public GeneralGameSettingSO generalSetting;
    public DataSystemSettingSO dataSystemSetting;

    [Header("Presenter Setting")]
    public PresenterSettingSO presenterSetting;

    [Header("Perfect Line Setting")]
    public PerfectLineSettingSO perfectLineSettingSO;

    [Header("Music Note")]
    public MusicNoteSettingSO musicNoteSettingSO;
    public Transform notePresenterParent;

    [Header("Input Debugger")]
    public Transform inputPresenterParent;

    [Header("Lane Line Settings")]
    public LaneLineSettingSO laneLineSettings;

    [Header("Intro Note Setting")]
    public IntroNoteSettingSO introNoteSetting;

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
        SystemRepository.RegisterSystem(new LaneLineSortingSystem());
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

        var landLineEntityGroup = new EntityGroup<LaneLineComponentType>(5);
        landLineEntityGroup.RegisterComponent(
            LaneLineComponentType.LaneLineData,
            new LaneLineData(landLineEntityGroup.EntityCount)
        );

        EntityRepository.RegisterEGroup(EntityType.LaneLineEntityGroup, ref landLineEntityGroup);

        #endregion

        #region Singleton data registration
        SingletonComponentRepository.RegisterComponent(
            SingletonComponentType.PerfectLine,
            new PerfectLineData(
                perfectLineSettingSO.TopLeft,
                perfectLineSettingSO.TopRight,
                perfectLineSettingSO.BottomLeft,
                perfectLineSettingSO.BottomRight,
                perfectLineSettingSO.Position
            )
        );

        SingletonComponentRepository.RegisterComponent(
            SingletonComponentType.Input,
            new InputDataComponent(2)
        );

        SingletonComponentRepository.RegisterComponent(
            SingletonComponentType.IntroNote,
            new IntroNoteData(true)
        );
        #endregion

        #region Presenters registration

        PresenterManagerRepository.RegisterManager(
            PresenterManagerType.MusicNotePresenterManager,
            new PresenterManager(
                musicNoteEntityGroup.EntityCount,
                presenterSetting.shortMusicNotePresenterPrefab,
                notePresenterParent
            )
        );

        PresenterManagerRepository.RegisterManager(
            PresenterManagerType.LongNotePresenterManager,
            new PresenterManager(
                musicNoteEntityGroup.EntityCount,
                presenterSetting.longMusicNotePresenterPrefab,
                notePresenterParent
            )
        );

        PresenterManagerRepository.RegisterManager(
            PresenterManagerType.InputDebuggerPresenterManager,
            new PresenterManager(
                dataSystemSetting.defaultCapacity,
                presenterSetting.inputDebuggerPresenterPrefab,
                inputPresenterParent
            )
        );

        PresenterManagerRepository.RegisterManager(
            PresenterManagerType.LaneLinePresenterManager,
            new PresenterManager(
                landLineEntityGroup.EntityCount,
                presenterSetting.laneLinePresenter,
                inputPresenterParent
            )
        );

        PresenterManagerRepository.RegisterManager(
            PresenterManagerType.IntroNotePresenterManager,
            new PresenterManager(1, presenterSetting.introNotePressenyer)
        );

        #endregion

        #region Initialize Data
        MusicNoteInitializer.Initialize();
        LaneLineInitializer.Initialize();
        #endregion

        #region bridges registration
        BridgeRepository.RegisterBridge(
            BridgeType.NoteTransform,
            MusicNoteTransformBridge.Create()
        );
        BridgeRepository.RegisterBridge(BridgeType.LaneLineBridge, LaneLineBridge.Create());
        #endregion

        GizmoDebugger.Instance.InitData(musicNoteEntityGroup.EntityCount);
    }

    private void OnDestroy()
    {
        SystemRepository.Clear();
        EntityRepository.Clear();
    }
}
