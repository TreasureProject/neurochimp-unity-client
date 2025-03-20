using System.Collections.Generic;

public static class Utils
{
    public static CharacterInfo ToCharacterInfo(CharacterData characterData)
    {
        return new CharacterInfo
        {
            Id = characterData.id,
            Name = characterData.name,
            SystemPrompt = characterData.system,
            Bio = characterData.bio,
            Lore = characterData.lore,
            MessageExamples = ConvertMessageExamples(characterData.messageExamples),
            PostExamples = characterData.postExamples,
            Topics = characterData.topics,
            Adjectives = characterData.adjectives,
            Knowledge = characterData.knowledge,
        };
    }

    private static List<List<MessageExample>> ConvertMessageExamples(MessageExampleData[][] messageExamplesData)
    {
        var messageExamples = new List<List<MessageExample>>();
        if (messageExamplesData == null)
        {
            return new List<List<MessageExample>>();
        }
        foreach (var exampleList in messageExamplesData)
        {
            var exampleListConverted = new List<MessageExample>();
            foreach (var example in exampleList)
            {
                exampleListConverted.Add(new MessageExample
                {
                    User = example.user,
                    Content = new MessageContent { Text = example.content.text }
                });
            }
            messageExamples.Add(exampleListConverted);
        }
        return messageExamples;
    }
}