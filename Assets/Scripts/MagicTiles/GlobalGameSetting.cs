using System;
using UnityEngine;

public class GlobalGameSetting : PersistentSingleton<GlobalGameSetting>
{
    public GeneralSetting generalSetting;
    public DataSystemSetting dataSystemSetting;

    protected override void OnAwake()
    {
        SystemRepository.RegisterSystem(new TileSpawnSystem());
        SystemRepository.RegisterSystem(new TransformUpdateSystem());

        DataComponentRepository.RegisterData(
            new MusicNoteMidiData(dataSystemSetting.defaultCapacity)
        );

        DataComponentRepository.RegisterData(
            new MusicNoteTransformData(dataSystemSetting.defaultCapacity)
        );
    }

    private void OnDestroy()
    {
        SystemRepository.Clear();
        DataComponentRepository.Clear();
    }
}

[Serializable]
public class GeneralSetting
{
    public int gameSpeed;
}

[Serializable]
public class DataSystemSetting
{
    public int defaultCapacity = 64;
}
