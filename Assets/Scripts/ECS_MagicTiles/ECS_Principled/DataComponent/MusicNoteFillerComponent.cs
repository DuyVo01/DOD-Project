using System.Collections;
using System.Collections.Generic;
using ECS_MagicTile;
using UnityEngine;

namespace ECS_MagicTile.Components
{
    public struct MusicNoteFillerComponent : IComponent
    {
        public bool IsVisible;
        public float FillPercent;
    }
}
