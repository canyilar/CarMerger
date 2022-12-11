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

            SerializedProperty carPrefab = obj.FindProperty("_carPrefabs");
            SerializedProperty carHolder = obj.FindProperty("_carHolder");
            SerializedProperty spawnOnStart = obj.FindProperty("_spawnCarsOnStart");

            EditorGUILayout.PropertyField(carPrefab);
            EditorGUILayout.PropertyField(spawnOnStart);
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
                spawnOnStart.name,
            };
            DrawPropertiesExcluding(obj, exlucededProps);
            obj.ApplyModifiedProperties();
        }

    }
}