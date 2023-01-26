using EG.Tower.Game;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(GameHub), true)]
public class GameHubEditor : Editor
{
    public GameHub Target
    {
        get
        {
            return (GameHub)target;
        }
    }

    public void Update()
    {
        Repaint();
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        EditorGUI.BeginChangeCheck();

        if (Target.Session == null || Target.Session.HeroModel == null)
        {
            return;
        }

        EditorGUILayout.LabelField("Hero Debug", EditorStyles.boldLabel);
        EditorGUILayout.Space();
        EditorGUILayout.LabelField("Name", Target.Session.HeroModel.Name);
        EditorGUILayout.LabelField("Main Virtue", Target.Session.HeroModel.MainVirtueTrait?.Virtue);
        EditorGUILayout.LabelField("Main Vice", Target.Session.HeroModel.MainViceTrait?.Vice);
        EditorGUILayout.Space();

        EditorGUILayout.LabelField("Traits");
        EditorGUILayout.Space();
        var traits = Target.Session.HeroModel.Traits;
        for (int i = 0; i < traits.Length; i++)
        {
            EditorGUILayout.LabelField(traits[i].AsText());
        }

        EditorGUILayout.Space();

        if (EditorGUI.EndChangeCheck())
        {
            serializedObject.ApplyModifiedProperties();
            SceneView.RepaintAll();
        }

        serializedObject.ApplyModifiedProperties();
    }
}
