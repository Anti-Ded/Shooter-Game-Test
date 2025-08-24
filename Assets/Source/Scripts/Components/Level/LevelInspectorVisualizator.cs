using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;

[CustomEditor(typeof(LevelComponent))]
public class LevelInspectorVisualizator : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        LevelComponent level = (LevelComponent)target;
        if (GUILayout.Button("Calculate"))
            level.Calculate();
    }
}
#endif