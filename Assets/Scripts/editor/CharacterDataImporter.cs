using UnityEngine;
using UnityEditor;
using System.IO;
using Newtonsoft.Json;
using System.Collections.Generic;

public class CharacterDataImporter : EditorWindow
{
    private string jsonFilePath;

    [MenuItem("Tools/Character Data Importer")]
    public static void ShowWindow()
    {
        GetWindow<CharacterDataImporter>("Character Data Importer");
    }

    private void OnGUI()
    {
        GUILayout.Label("Import Character Data from JSON", EditorStyles.boldLabel);

        if (GUILayout.Button("Select JSON File"))
        {
            jsonFilePath = EditorUtility.OpenFilePanel("Select JSON File", "", "json");
        }

        GUILayout.Label("Selected File: " + jsonFilePath);

        if (GUILayout.Button("Import"))
        {
            ImportCharacterData(jsonFilePath);
        }
    }

    private void ImportCharacterData(string path)
    {
        if (string.IsNullOrEmpty(path) || !File.Exists(path))
        {
            Debug.LogError("Invalid file path");
            return;
        }

        string jsonContent = File.ReadAllText(path);
        CharacterInfo characterInfo = JsonConvert.DeserializeObject<CharacterInfo>(jsonContent);

        CharacterData characterData = ScriptableObject.CreateInstance<CharacterData>();
        characterData.id = characterInfo.Id;
        characterData.name = characterInfo.Name;
        characterData.system = characterInfo.SystemPrompt;
        characterData.bio = characterInfo.Bio;
        characterData.lore = characterInfo.Lore;
        characterData.messageExamples = ConvertMessageExamples(characterInfo.MessageExamples);
        characterData.postExamples = characterInfo.PostExamples;
        characterData.topics = characterInfo.Topics;
        characterData.adjectives = characterInfo.Adjectives;
        characterData.knowledge = characterInfo.Knowledge;

        string assetPath = $"Assets/Resources/CharacterData/{characterData.name}.asset";
        AssetDatabase.CreateAsset(characterData, assetPath);
        AssetDatabase.SaveAssets();

        Debug.Log($"Character data imported and saved to {assetPath}");
    }

    private MessageExampleData[][] ConvertMessageExamples(List<List<MessageExample>> messageExamples)
    {
        var messageExamplesData = new MessageExampleData[messageExamples.Count][];
        for (int i = 0; i < messageExamples.Count; i++)
        {
            var exampleList = messageExamples[i];
            messageExamplesData[i] = new MessageExampleData[exampleList.Count];
            for (int j = 0; j < exampleList.Count; j++)
            {
                var example = exampleList[j];
                messageExamplesData[i][j] = new MessageExampleData
                {
                    user = example.User,
                    content = new MessageContentData { text = example.Content.Text }
                };
            }
        }
        return messageExamplesData;
    }
}