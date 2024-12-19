public struct MusicTileWorld
{
    private bool isInitialized;

    public void Initialize()
    {
        if (isInitialized)
            return;

        SystemRepository.GetSystem<TileSpawnSystem>().SpawnTileNote();
        ref var transfromUpdateSystem = ref SystemRepository.GetSystem<TransformUpdateSystem>();

        ref var noteEntityGroup = ref EntityRepository.GetEGroup<
            EntityGroup<MusicNoteComponentType>
        >(EntityType.NoteEntityGroup);

        ref var musicNoteTransformData = ref noteEntityGroup.GetComponent<MusicNoteTransformData>(
            MusicNoteComponentType.MusicNoteTransformData
        );

        ref var musicNoteMidiData = ref noteEntityGroup.GetComponent<MusicNoteMidiData>(
            MusicNoteComponentType.MusicNoteMidiData
        );

        ref var musicNoteStateData = ref noteEntityGroup.GetComponent<MusicNoteStateData>(
            MusicNoteComponentType.MusicNoteStateData
        );

        ref var musicNoteFillerData = ref noteEntityGroup.GetComponent<MusicNoteFillerData>(
            MusicNoteComponentType.MusicNoteFiller
        );

        ref var noteStateSystem = ref SystemRepository.GetSystem<NoteStateSystem>();

        for (int entityId = 0; entityId < noteEntityGroup.EntityCount; entityId++)
        {
            noteStateSystem.NoteStateDeterminer(
                entityId,
                ref musicNoteMidiData,
                ref musicNoteStateData
            );
            transfromUpdateSystem.SyncTransformScale(
                entityId,
                ref musicNoteMidiData,
                ref musicNoteTransformData,
                ref musicNoteStateData
            );
        }

        isInitialized = true;
    }

    public void Update()
    {
        if (!isInitialized)
            return;

        ref var bridge = ref BridgeRepository.GetBridge<MusicNoteTransformBridge>(
            BridgeType.NoteTransform
        );

        SystemRepository.GetSystem<InputSystem>().ProcessInput();

        ref var movingTileSystem = ref SystemRepository.GetSystem<MovingTileSystem>();
        ref var noteCornerUpdateSystem = ref SystemRepository.GetSystem<NoteCornerUpdateSystem>();
        ref var noteStateSystem = ref SystemRepository.GetSystem<NoteStateSystem>();
        ref var inputCollisionSystem = ref SystemRepository.GetSystem<InputCollisionSystem>();

        ref var noteEntityGroup = ref EntityRepository.GetEGroup<
            EntityGroup<MusicNoteComponentType>
        >(EntityType.NoteEntityGroup);

        ref var musicNoteTransformData = ref noteEntityGroup.GetComponent<MusicNoteTransformData>(
            MusicNoteComponentType.MusicNoteTransformData
        );

        ref var musicNoteStateData = ref noteEntityGroup.GetComponent<MusicNoteStateData>(
            MusicNoteComponentType.MusicNoteStateData
        );

        ref var musicNoteFillerData = ref noteEntityGroup.GetComponent<MusicNoteFillerData>(
            MusicNoteComponentType.MusicNoteFiller
        );

        ref var perfectLineData = ref SingletonComponentRepository.GetComponent<PerfectLineData>(
            SingletonComponentType.PerfectLine
        );

        for (int entityId = 0; entityId < noteEntityGroup.EntityCount; entityId++)
        {
            movingTileSystem.MovingTile(
                entityId,
                ref musicNoteTransformData,
                ref musicNoteFillerData,
                ref musicNoteStateData
            );

            noteCornerUpdateSystem.UpdateCorners(entityId, ref musicNoteTransformData);

            noteStateSystem.NoteStateUpdate(
                entityId,
                ref musicNoteTransformData,
                ref musicNoteStateData,
                ref perfectLineData
            );

            inputCollisionSystem.ProcessCollisions(
                entityId,
                ref musicNoteTransformData,
                ref musicNoteStateData,
                ref musicNoteFillerData
            );

            bridge.SyncNoteTransformToUnity(
                entityId,
                ref musicNoteTransformData,
                ref musicNoteStateData,
                ref musicNoteFillerData
            );
        }
    }
}
