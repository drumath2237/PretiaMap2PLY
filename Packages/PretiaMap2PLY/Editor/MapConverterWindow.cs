using System.IO;
using UnityEditor;
using UnityEngine;

namespace PretiaMap2PLY.Editor
{
    public class MapConverterWindow : EditorWindow
    {
        private string _mapFilePath = null;
        private bool isExporting = false;

        [MenuItem("PretiaMap2PLY/Export Wizard")]
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

            if (string.IsNullOrEmpty(_mapFilePath) || isExporting)
            {
                GUI.enabled = false;
            }

            if (GUILayout.Button(isExporting ? "Exporting..." : "Export"))
            {
                byte[] mapDataBytes;
                using (var fs = new FileStream(_mapFilePath, FileMode.Open, FileAccess.Read))
                {
                    mapDataBytes = new byte[fs.Length];
                    fs.Read(mapDataBytes, 0, (int)fs.Length);
                }

                var mapDataString = System.Text.Encoding.UTF8.GetString(mapDataBytes);
                var (isSuccess, err) = MapDataConverter.TryGetPointCloudDataFromMap(mapDataString, out var pointCloud);
                if (!isSuccess)
                {
                    Debug.LogError("Cannot convert map");
                    Debug.LogError(err);
                    return;
                }

                var folderName = EditorUtility.SaveFolderPanel("save ply point cloud", Application.dataPath, "");
                isExporting = true;
                PlyExporter.Export(folderName, pointCloud).ContinueWith(_ => { isExporting = false; });
            }

            GUI.enabled = true;
        }
    }
}