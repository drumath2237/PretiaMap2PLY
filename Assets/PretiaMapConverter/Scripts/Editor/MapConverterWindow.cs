using UnityEditor;
using UnityEngine;

namespace PretiaMapConverter.Editor
{
    public class MapConverterWindow : EditorWindow
    {
        private string _mapFilePath = null;

        [MenuItem("Pretia Map Converter/Export Wizard")]
        private static void ShowWindow()
        {
            var window = GetWindow<MapConverterWindow>();
            window.titleContent = new GUIContent("Export Wizard");
            window.Show();
        }

        private void OnGUI()
        {
            EditorGUILayout.Space();
            GUILayout.Label("Select map file");


            EditorGUILayout.Space();
            if (!string.IsNullOrEmpty(_mapFilePath))
            {
                EditorGUILayout.HelpBox($"Path:\n{_mapFilePath}", MessageType.None);
            }
            else
            {
                EditorGUILayout.HelpBox("Path is not assigned", MessageType.Error);
            }
            
            if (GUILayout.Button("Select"))
            {
                _mapFilePath = EditorUtility.OpenFilePanelWithFilters("pretia map file", Application.dataPath,
                    new[] { "Pretia Map", "map", "All files", "*" });
            }


            EditorGUILayout.Space();
            GUILayout.Button("Export");
        }
    }
}