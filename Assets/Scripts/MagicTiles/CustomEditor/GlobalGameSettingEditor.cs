#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(GlobalGameSetting))]
public class GlobalGameSettingEditor : Editor
{
    public override void OnInspectorGUI()
    {
        // Draw the default inspector UI
        DrawDefaultInspector();

        // Add a button to display Game View dimensions
        if (GUILayout.Button("Show Simulator Screen Dimensions"))
        {
            // Get the Game View size
            Vector2 gameViewSize = GetMainGameViewSize();
            Debug.Log($"Game View Width: {gameViewSize.x}, Game View Height: {gameViewSize.y}");
        }
    }

    private Vector2 GetMainGameViewSize()
    {
        // Use UnityEditor.Handles to get the Game View size
        return UnityEditor.Handles.GetMainGameViewSize();
    }
}
#endif
