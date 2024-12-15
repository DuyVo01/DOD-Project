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
        bridge.SyncTransformToUnity();

        isInitialized = true;
    }

    public void Update()
    {
        if (!isInitialized)
            return;

        SystemRepository.GetSystem<MovingTileSystem>().MovingTile();
        SystemRepository.GetSystem<NoteCornerUpdateSystem>().UpdateCorners();
        SystemRepository.GetSystem<NoteStateSystem>().NoteStateUpdate();

        ref var bridge = ref BridgeRepository.GetBridge<UnityTransformBridge>(
            BridgeType.NoteTransform
        );
        bridge.SyncTransformToUnity();
    }
}
