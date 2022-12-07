using UnityEditor;
using UnityEngine;

namespace CarMerger.Editors
{
    [CustomEditor(typeof(Spawner))]
    public class SpawnerEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            Spawner carSpawner = (Spawner)target;
            SerializedObject obj = new(carSpawner);

            SerializedProperty carPrefab = obj.FindProperty("_carPrefab");
            SerializedProperty carHolder = obj.FindProperty("_carHolder");

            EditorGUILayout.PropertyField(carPrefab);
            EditorGUILayout.PropertyField(carHolder);

            GUILayout.Space(10);

            if (GUILayout.Button("Spawn Car"))
            {
                carSpawner.SpawnCar();
            }

            string[] exlucededProps = new string[]
            {
                "m_Script",
                carHolder.name,
                carPrefab.name,
            };
            DrawPropertiesExcluding(obj, exlucededProps);
            obj.ApplyModifiedProperties();
        }

    }
}