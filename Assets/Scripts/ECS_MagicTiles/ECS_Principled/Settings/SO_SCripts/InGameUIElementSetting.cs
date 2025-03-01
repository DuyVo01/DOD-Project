using UnityEngine;

namespace ECS_MagicTile
{
    [CreateAssetMenu(
        fileName = "SO_InGameUIElementSetting",
        menuName = "Settings/InGameUIElementSetting"
    )]
    public class InGameUIElementSetting : ScriptableObject
    {
        public ScoreUISettings scoreUISettings;
        public SongNameUISettings songNameUISettings;
        public SongAuthorUISettings songAuthorUISettings;
        public HighScoreUISettings highScoreUISettings;
        public DecorationUISettings decorationUISettings;

        [System.Serializable]
        public struct ScoreUISettings
        {
            public Vector2 phonePortraitPos;
            public Vector2 phoneLandscapePos;
            public Vector2 tabletPortraitPos;
            public Vector2 tabletLandscapePos;
        }

        [System.Serializable]
        public struct SongNameUISettings
        {
            public string songNameContent;
            public Vector2 phonePortraitPos;
            public Vector2 phoneLandscapePos;
            public Vector2 tabletPortraitPos;
            public Vector2 tabletLandscapePos;
        }

        [System.Serializable]
        public struct SongAuthorUISettings
        {
            public string songAuthorContent;
            public Vector2 phonePortraitPos;
            public Vector2 phoneLandscapePos;
            public Vector2 tabletPortraitPos;
            public Vector2 tabletLandscapePos;
        }

        [System.Serializable]
        public struct HighScoreUISettings
        {
            public Vector2 phonePortraitPos;
            public Vector2 phoneLandscapePos;
            public Vector2 tabletPortraitPos;
            public Vector2 tabletLandscapePos;
        }

        [System.Serializable]
        public struct DecorationUISettings
        {
            public Vector2 phonePortraitPos;
            public Vector2 phoneLandscapePos;
            public Vector2 tabletPortraitPos;
            public Vector2 tabletLandscapePos;
        }
    }
}
