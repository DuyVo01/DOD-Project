using UnityEngine;
using UnityEngine.UI;

public static class GraphicExtension
{
    public static void SetAlpha(this Graphic graphic, float alpha)
    {
        graphic.color = new Color(graphic.color.r, graphic.color.g, graphic.color.b, alpha);
    }
}
