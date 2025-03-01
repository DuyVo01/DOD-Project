using ComponentCache;
using UIBlock;
using UnityEngine;
using UnityEngine.UI;

namespace ECS_MagicTile
{
    public class InGameUIElementHandlerSystem : MonoBehaviour, IGameSystem
    {
        [Header("Settings Data")]
        public InGameUIElementSetting inGameUIElementSetting;

        [Header("UI Elements")]
        [SerializeField]
        private GameObject scoreUI;

        [SerializeField]
        private GameObject songNameUI;

        [SerializeField]
        private GameObject songAuthorUI;

        [SerializeField]
        private GameObject highScoreUI;

        [SerializeField]
        private GameObject decorationUI;
        public bool IsEnabled { get; set; }
        public World World { get; set; }

        bool hasGetAllComponents = false;

        public void SetWorld(World world) { }

        public void RunInitialize()
        {
            if (hasGetAllComponents)
                return;

            hasGetAllComponents = true;

            scoreUI.RegisterComponent(scoreUI.GetComponent<RectTransform>());

            songNameUI.RegisterComponent(songNameUI.GetComponent<RectTransform>());
            songNameUI.RegisterComponent(songNameUI.GetComponent<Text>());

            songAuthorUI.RegisterComponent(songAuthorUI.GetComponent<RectTransform>());
            songAuthorUI.RegisterComponent(songAuthorUI.GetComponent<Text>());

            highScoreUI.RegisterComponent(highScoreUI.GetComponent<RectTransform>());

            decorationUI.RegisterComponent(decorationUI.GetComponent<RectTransform>());

            scoreUI
                .RectTransform()
                .RegisterBlock(
                    inGameUIElementSetting.scoreUISettings.phonePortraitPos,
                    inGameUIElementSetting.scoreUISettings.phoneLandscapePos,
                    inGameUIElementSetting.scoreUISettings.tabletPortraitPos,
                    inGameUIElementSetting.scoreUISettings.tabletPortraitPos
                );

            songNameUI
                .RectTransform()
                .RegisterBlock(
                    inGameUIElementSetting.songNameUISettings.phonePortraitPos,
                    inGameUIElementSetting.songNameUISettings.phoneLandscapePos,
                    inGameUIElementSetting.songNameUISettings.tabletPortraitPos,
                    inGameUIElementSetting.songNameUISettings.tabletPortraitPos
                );

            songAuthorUI
                .RectTransform()
                .RegisterBlock(
                    inGameUIElementSetting.songAuthorUISettings.phonePortraitPos,
                    inGameUIElementSetting.songAuthorUISettings.phoneLandscapePos,
                    inGameUIElementSetting.songAuthorUISettings.tabletPortraitPos,
                    inGameUIElementSetting.songAuthorUISettings.tabletPortraitPos
                );

            highScoreUI
                .RectTransform()
                .RegisterBlock(
                    inGameUIElementSetting.highScoreUISettings.phonePortraitPos,
                    inGameUIElementSetting.highScoreUISettings.phoneLandscapePos,
                    inGameUIElementSetting.highScoreUISettings.tabletPortraitPos,
                    inGameUIElementSetting.highScoreUISettings.tabletPortraitPos
                );

            decorationUI
                .RectTransform()
                .RegisterBlock(
                    inGameUIElementSetting.decorationUISettings.phonePortraitPos,
                    inGameUIElementSetting.decorationUISettings.phoneLandscapePos,
                    inGameUIElementSetting.decorationUISettings.tabletPortraitPos,
                    inGameUIElementSetting.decorationUISettings.tabletPortraitPos
                );

            songNameUI.Text().text = inGameUIElementSetting.songNameUISettings.songNameContent;
            songAuthorUI.Text().text = inGameUIElementSetting
                .songAuthorUISettings
                .songAuthorContent;
        }

        public void RunUpdate(float deltaTime) { }

        public void RunCleanup() { }
    }
}
