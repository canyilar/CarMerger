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

            SerializedProperty carPrefabs = serializedObject.FindProperty("_carPrefabs");
            SerializedProperty carHolder = serializedObject.FindProperty("_carHolder");
            SerializedProperty spawnOnStart = serializedObject.FindProperty("_spawnCarsOnStart");
            SerializedProperty placeholders = serializedObject.FindProperty("_carPlaceholderPrefabs");

            EditorGUILayout.PropertyField(spawnOnStart);
            EditorGUILayout.PropertyField(carHolder);
            EditorGUILayout.PropertyField(carPrefabs);
            EditorGUILayout.PropertyField(placeholders);

            GUILayout.Space(10);

            if (GUILayout.Button("Spawn Car"))
            {
                carSpawner.SpawnCar();
            }

            string[] exlucededProps = new string[]
            {
                "m_Script",
                carHolder.name,
                carPrefabs.name,
                placeholders.name,
                spawnOnStart.name,
            };
            DrawPropertiesExcluding(serializedObject, exlucededProps);
            serializedObject.ApplyModifiedProperties();
        }

    }
}