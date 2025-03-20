using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class MessageContentData
{
    public string text;
}

[System.Serializable]
public class MessageExampleData
{
    public string user;
    public MessageContentData content;
}

[CreateAssetMenu(fileName = "CharacterData", menuName = "ScriptableObjects/CharacterData", order = 1)]
public class CharacterData : ScriptableObject
{
    public string id;
    public string name;
    public string system;
    public string[] bio;
    public string[] lore;
    public MessageExampleData[][] messageExamples;
    public string[] postExamples;
    public string[] topics;
    public string[] adjectives;
    public string[] knowledge;
}