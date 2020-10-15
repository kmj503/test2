using UnityEngine;
using UnityEditor;

[CanEditMultipleObjects]
[CustomEditor(typeof(UbhBaseShot), true)]
public class UbhBaseShotInspector : Editor
{
    public override void OnInspectorGUI ()
    {
        serializedObject.Update();
        DrawProperties();
        serializedObject.ApplyModifiedProperties();
    }

    void DrawProperties ()
    {
        UbhBaseShot obj = target as UbhBaseShot;

        EditorGUILayout.Space();

        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button("start shot")) {
            if (Application.isPlaying && obj.gameObject.activeInHierarchy) {
                obj.Shot();
            }
        }
        EditorGUILayout.EndHorizontal();

        if (obj._BulletPrefab == null) {
            Color guiColor = GUI.color;
            GUI.color = Color.yellow;

            EditorGUILayout.LabelField("*****WARNING*****");
            EditorGUILayout.LabelField("총알 프리펩 설정이 안됨");

            GUI.color = guiColor;
        }

        EditorGUILayout.Space();

        DrawDefaultInspector();
    }
}