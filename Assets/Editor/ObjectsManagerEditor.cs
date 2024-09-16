using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(ObjectsManager))]
public class ObjectsManagerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        ObjectsManager objManager = (ObjectsManager)target;

        GUILayout.Label("Soil Values");

        foreach (var kvp in objManager.SoilValues)
        {
            GUILayout.Label($"Position: {kvp.Key} - Watered: {kvp.Value}");
        }

        GUILayout.Label("Seed Values");

        foreach (var seedPos in objManager.SeedValues)
        {
            GUILayout.Label($"Position: {seedPos.Key} - State: {seedPos.Value}");
        }
    }
}
