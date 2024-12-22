public struct LaneLineInitializer
{
    public static void Initialize()
    {
        ref var laneLineSortingSystem = ref SystemRepository.GetSystem<LaneLineSortingSystem>();

        ref var laneLineEntityGroup = ref EntityRepository.GetEGroup<
            EntityGroup<LaneLineComponentType>
        >(EntityType.LaneLineEntityGroup);

        ref var landLineData = ref laneLineEntityGroup.GetComponent<LaneLineData>(
            LaneLineComponentType.LaneLineData
        );
        ref var perfectLineData = ref SingletonComponentRepository.GetComponent<PerfectLineData>(
            SingletonComponentType.PerfectLine
        );

        for (int entityId = 0; entityId < laneLineEntityGroup.EntityCount; entityId++)
        {
            laneLineSortingSystem.PositionLandLine(entityId, ref landLineData, ref perfectLineData);
        }
    }
}
