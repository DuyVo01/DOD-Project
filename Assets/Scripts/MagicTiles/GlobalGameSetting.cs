using System;
using UnityEngine;

public class GlobalGameSetting : PersistentSingleton<GlobalGameSetting>
{
    public GeneralSetting generalSetting;
}

[Serializable]
public class GeneralSetting
{
    public int gameSpeed;
}
