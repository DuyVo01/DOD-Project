using ECS_MagicTile;
using UnityEngine.UI;

public class ProgressSyncTool : BaseSyncTool
{
    public ProgressSyncTool(GlobalPoint globalPoint)
        : base(globalPoint)
    {
        progressSlider = globalPoint.progressSlider;
    }

    protected override Archetype Archetype => Archetype.Registry.SongProgress;

    private readonly Slider progressSlider;

    public void SycnProgress(ProgressComponent progressComponent)
    {
        progressSlider.value = progressComponent.currentProgressPercent;
    }
}
