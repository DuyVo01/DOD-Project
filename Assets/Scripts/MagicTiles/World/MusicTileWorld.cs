using Mono.Cecil.Cil;

public struct MusicTileWorld
{
    private bool isInitialized;

    public void Initialize()
    {
        if (isInitialized)
            return;

        SystemRepository.GetSystem<TileSpawnSystem>().SpawnTileNote();
        SystemRepository.GetSystem<TransformUpdateSystem>().SyncTransformScale();

        ref var bridge = ref BridgeRepository.GetBridge<UnityTransformBridge>(
            BridgeType.NoteTransform
        );

        ref var noteEntityGroup = ref EntityRepository.GetEGroup<
            EntityGroup<MusicNoteComponentType>
        >(EntityType.NoteEntityGroup);

        ref var musicNoteTransformData = ref noteEntityGroup.GetComponent<MusicNoteTransformData>(
            MusicNoteComponentType.MusicNoteTransformData
        );

        for (int entityId = 0; entityId < noteEntityGroup.EntityCount; entityId++)
        {
            bridge.SyncTransformToUnity(entityId, ref musicNoteTransformData);
        }

        isInitialized = true;
    }

    public void Update()
    {
        if (!isInitialized)
            return;
        // // Process input first
        // SystemRepository.GetSystem<InputSystem>().ProcessInput();
        // SystemRepository.GetSystem<InputCollisionSystem>().ProcessCollisions();
        // SystemRepository.GetSystem<MovingTileSystem>().MovingTile();
        // SystemRepository.GetSystem<NoteCornerUpdateSystem>().UpdateCorners();
        // SystemRepository.GetSystem<NoteStateSystem>().NoteStateUpdate();

        ref var bridge = ref BridgeRepository.GetBridge<UnityTransformBridge>(
            BridgeType.NoteTransform
        );

        SystemRepository.GetSystem<InputSystem>().ProcessInput();
        SystemRepository.GetSystem<InputCollisionSystem>().ProcessCollisions();

        ref var movingTileSystem = ref SystemRepository.GetSystem<MovingTileSystem>();
        ref var noteCornerUpdateSystem = ref SystemRepository.GetSystem<NoteCornerUpdateSystem>();
        ref var noteStateSystem = ref SystemRepository.GetSystem<NoteStateSystem>();

        ref var noteEntityGroup = ref EntityRepository.GetEGroup<
            EntityGroup<MusicNoteComponentType>
        >(EntityType.NoteEntityGroup);

        ref var musicNoteTransformData = ref noteEntityGroup.GetComponent<MusicNoteTransformData>(
            MusicNoteComponentType.MusicNoteTransformData
        );

        ref var musicNoteStateData = ref noteEntityGroup.GetComponent<MusicNoteStateData>(
            MusicNoteComponentType.MusicNoteStateData
        );

        ref var perfectLineData = ref SingletonComponentRepository.GetComponent<PerfectLineData>(
            SingletonComponentType.PerfectLine
        );

        for (int entityId = 0; entityId < noteEntityGroup.EntityCount; entityId++)
        {
            // if (!noteEntityGroup.IsEntityActive(entityId))
            //     continue;

            movingTileSystem.MovingTile(entityId, ref musicNoteTransformData);
            noteCornerUpdateSystem.UpdateCorners(entityId, ref musicNoteTransformData);
            noteStateSystem.NoteStateUpdate(
                entityId,
                ref musicNoteTransformData,
                ref musicNoteStateData,
                ref perfectLineData
            );
            bridge.SyncTransformToUnity(entityId, ref musicNoteTransformData);
        }

        ref var inputDebuggerBridge = ref BridgeRepository.GetBridge<InputDebuggerBridge>(
            BridgeType.InputDebugger
        );

        inputDebuggerBridge.SpawnDebuggerAtInputPressed();
    }
}
