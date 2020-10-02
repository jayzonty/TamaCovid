using System.Collections.Generic;
using System.Linq;

using UnityEditor;
using UnityEngine;

namespace TamaCovid
{
    /// <summary>
    /// Class containing functionality for
    /// importing a TSV file containing data regarding actions,
    /// and creating/modifying SO_Action scriptable objects from it.
    /// </summary>
    public class ActionsTSVImporter
    {
        /// <summary>
        /// Import list of actions from a TSV file.
        /// (Opens a file selection dialog window)
        /// </summary>
        [MenuItem("TamaCovid/Import Actions TSV")]
        public static void ImportActionsFromTSV()
        {
            string filePath = EditorUtility.OpenFilePanel("Import Actions TSV", Application.dataPath, "tsv");
            if (string.IsNullOrEmpty(filePath))
            {
                return;
            }

            SO_Action currentAction = null;
            List<string> lines = System.IO.File.ReadLines(filePath).ToList();
            for (int i = 1; i < lines.Count; ++i)
            {
                string line = lines[i];

                string[] tokens = line.Split(new char[]{ '\t' }, System.StringSplitOptions.None);

                if (!string.IsNullOrEmpty(tokens[0])) // action category
                {
                    if (!string.IsNullOrEmpty(tokens[1])) // action name
                    {
                        string assetSearchPath = "Assets/ScriptableObjects/Actions/" + tokens[0];
                        if (System.IO.Directory.Exists(assetSearchPath))
                        {
                            string[] assets = AssetDatabase.FindAssets(tokens[1], new[] { assetSearchPath });
                            if (assets.Length > 0)
                            {
                                string assetPath = AssetDatabase.GUIDToAssetPath(assets[0]);
                                currentAction = AssetDatabase.LoadAssetAtPath<SO_Action>(assetPath);

                                currentAction.dialogues.Clear();
                            }
                        }
                        else
                        {
                            currentAction = ScriptableObject.CreateInstance<SO_Action>();
                            currentAction.category = tokens[0];
                            currentAction.name = tokens[1];
                            currentAction.dialogues = new List<Dialogue>();

                            if (!System.IO.Directory.Exists(assetSearchPath))
                            {
                                System.IO.Directory.CreateDirectory(assetSearchPath);
                            }

                            AssetDatabase.CreateAsset(currentAction, assetSearchPath + "/" + tokens[1] + ".asset");
                        }

                        EditorUtility.SetDirty(currentAction);
                    }
                }

                if (!string.IsNullOrEmpty(tokens[2])) // action description
                {
                    currentAction.description = tokens[2];
                }

                if (!string.IsNullOrEmpty(tokens[3])) // action enabled condition
                {
                    currentAction.enabledConditionString = tokens[3];
                }

                if (!string.IsNullOrEmpty(tokens[4])) // dialogue condition
                {
                    string dialogueCondition = tokens[4];
                    currentAction.dialogues.Add(new Dialogue());
                    currentAction.dialogues.Last().lines = new List<Dialogue.Line>();

                    if (dialogueCondition != "-")
                    {
                        currentAction.dialogues.Last().dialogueCondition = tokens[4];
                    }
                }

                if (!string.IsNullOrEmpty(tokens[5]) || !string.IsNullOrEmpty(tokens[6]) || !string.IsNullOrEmpty(tokens[7])) // message, message condition, and command
                {
                    Dialogue.Line dialogueLine = new Dialogue.Line
                    {
                        message = tokens[5],
                        messageCondition = tokens[6],
                        commands = tokens[7]
                    };

                    currentAction.dialogues.Last().lines.Add(dialogueLine);
                }
            }

            AssetDatabase.SaveAssets();
        }
    }
}
