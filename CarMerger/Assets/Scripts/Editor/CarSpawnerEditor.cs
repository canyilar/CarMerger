using UnityEditor;
using UnityEngine;

namespace CarMerger.Editors
{
    [CustomEditor(typeof(CarSpawner))]
    public class CarSpawnerEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

        }

    }
}