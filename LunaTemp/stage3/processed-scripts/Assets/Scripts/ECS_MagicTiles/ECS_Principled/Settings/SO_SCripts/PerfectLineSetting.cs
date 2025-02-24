using System;
using UnityEngine;

namespace ECS_MagicTile
{
    [CreateAssetMenu(
        fileName = "PerfectLineSetting_SO",
        menuName = "Settings/PerfectLineSetting_SO"
    )]
    public class PerfectLineSetting : ScriptableObject
    {
        [Header(" Normalize Positions")]
        public NormalizedFloatPreset portraitNormalizedPos;

        public NormalizedFloatPreset landscapeNormalizedPos;

        [Header("Normalized Size")]
        public NormalizedFloatPreset portraitNormalizedSize;

        public NormalizedFloatPreset landscapeNormalizedSize;

        [System.Serializable]
        public struct NormalizedFloatPreset
        {
            public RangeReactiveFloat normalizedX;

            public RangeReactiveFloat normalizedY;
        }

        private void OnValidate()
        {
            portraitNormalizedPos.normalizedX.OnChangeValidatedInInpsector();
            portraitNormalizedPos.normalizedY.OnChangeValidatedInInpsector();

            landscapeNormalizedPos.normalizedX.OnChangeValidatedInInpsector();
            landscapeNormalizedPos.normalizedY.OnChangeValidatedInInpsector();

            portraitNormalizedSize.normalizedX.OnChangeValidatedInInpsector();
            portraitNormalizedSize.normalizedY.OnChangeValidatedInInpsector();

            landscapeNormalizedSize.normalizedX.OnChangeValidatedInInpsector();
            landscapeNormalizedSize.normalizedY.OnChangeValidatedInInpsector();
        }
    }

    [Serializable]
    public class RangeReactiveFloat : ReactiveValue<float>
    {
        [Range(0, 1)]
        protected new float _value;
    }
}
