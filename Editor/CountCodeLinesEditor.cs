#if UNITY_EDITOR
using System.IO;
using UnityEditor;
using UnityEngine;

namespace AbyssMoth
{
    public sealed class CountCodeLinesEditor : EditorWindow
    {
        private string folderPath = "Assets";

        [MenuItem("RimuruDev Tools/Count Code Lines")]
        public static void ShowWindow()
        {
            GetWindow<CountCodeLinesEditor>("Count Code Lines");
        }

        private void OnGUI()
        {
            GUILayout.Label("Count Code Lines in Project", EditorStyles.boldLabel);

            GUILayout.Label("Folder Path:");
            folderPath = GUILayout.TextField(folderPath);

            if (GUILayout.Button("Select Folder"))
            {
                string selectedPath = EditorUtility.OpenFolderPanel("Select Folder", folderPath, "");
                if (!string.IsNullOrEmpty(selectedPath))
                {
                    folderPath = selectedPath;
                }
            }

            if (GUILayout.Button("Count Lines"))
            {
                CountLinesInProject(folderPath);
            }
        }

        private void CountLinesInProject(string directoryPath)
        {
            if (!Directory.Exists(directoryPath))
            {
                Debug.LogError("Specified folder does not exist.");
                return;
            }

            var totalLines = 0;
            var files = Directory.GetFiles(directoryPath, "*.cs", SearchOption.AllDirectories);

            foreach (var file in files)
            {
                var fileLineCount = CountLinesInFile(file);
                Debug.Log($"{file}: {fileLineCount} lines of code");
                totalLines += fileLineCount;
            }

            Debug.Log($"\nTotal lines of code: {totalLines}");
        }

        private int CountLinesInFile(string filePath)
        {
            var lineCount = 0;

            foreach (var line in File.ReadLines(filePath))
            {
                var trimmedLine = line.Trim();
                if (!trimmedLine.StartsWith("//") && !string.IsNullOrWhiteSpace(trimmedLine))
                {
                    lineCount++;
                }
            }

            return lineCount;
        }
    }
}
#endif