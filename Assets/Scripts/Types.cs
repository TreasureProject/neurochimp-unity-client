using Newtonsoft.Json;
using System.Collections.Generic;

public class ChatRequest
{
    [JsonProperty("agentType")]
    public string AgentType { get; set; }

    [JsonProperty("message")]
    public Message Message { get; set; }

    [JsonProperty("model")]
    public string Model { get; set; }

    [JsonProperty("apiKey")]
    public string ApiKey { get; set; }

    [JsonProperty("customGameState")]
    public string CustomGameState { get; set; }

    [JsonProperty("extraFields")]
    public string[] ExtraFields { get; set; }

    [JsonProperty("characterId")]
    public string CharacterId { get; set; }

    [JsonProperty("roomId")]
    public string RoomId { get; set; }
}

public class Message
{
    [JsonProperty("sender")]
    public string Sender { get; set; }

    [JsonProperty("senderId")]
    public string SenderId { get; set; }

    [JsonProperty("isAgent")]
    public bool IsAgent { get; set; }

    [JsonProperty("message")]
    public string message { get; set; }

    [JsonProperty("image")]
    public string Image { get; set; }

    [JsonProperty("timestamp")]
    public long Timestamp { get; set; }

    [JsonProperty("roomId")]
    public string RoomId { get; set; }

}

public class GameInfoRequest
{
    [JsonProperty("gameInfo")]
    public string GameInfo { get; set; }

    [JsonProperty("userId")]
    public string UserId { get; set; }

    [JsonProperty("gameName")]
    public string GameName { get; set; }

    [JsonProperty("gameInputInfo")]
    public string GameInputInfo { get; set; }

    [JsonProperty("gameMode")]
    public string GameMode { get; set; }

    [JsonProperty("characterId")]
    public string CharacterId { get; set; }

    [JsonProperty("roomId")]
    public string RoomId { get; set; }
}

public class CharacterRequest
{
    [JsonProperty("character")]
    public CharacterInfo Character { get; set; }
}

public class CharacterInfo
{
    [JsonProperty("id")]
    public string Id { get; set; }

    [JsonProperty("name")]
    public string Name { get; set; }

    [JsonProperty("system")]
    public string SystemPrompt { get; set; }

    [JsonProperty("bio")]
    public string[] Bio { get; set; }

    [JsonProperty("lore")]
    public string[] Lore { get; set; }

    [JsonProperty("messageExamples")]
    public List<List<MessageExample>> MessageExamples { get; set; }

    [JsonProperty("postExamples")]
    public string[] PostExamples { get; set; }

    [JsonProperty("topics")]
    public string[] Topics { get; set; }

    [JsonProperty("adjectives")]
    public string[] Adjectives { get; set; }

    [JsonProperty("knowledge")]
    public string[] Knowledge { get; set; }
}

public class MessageExample
{
    [JsonProperty("user")]
    public string User { get; set; }

    [JsonProperty("content")]
    public MessageContent Content { get; set; }
}

public class MessageContent
{
    [JsonProperty("text")]
    public string Text { get; set; }
}

public class ChatResponse
{
    [JsonProperty("response")]
    public string Response { get; set; }

    [JsonProperty("reasoning")]
    public string Reasoning { get; set; }

    [JsonProperty("emotion")]
    public string Emotion { get; set; }
}

public class Tool
{
    [JsonProperty("type")]
    public string Type { get; set; }

    [JsonProperty("output")]
    public Dictionary<string, object> Output { get; set; }
}

public class GameInfoResponse
{
    [JsonProperty("status")]
    public string Status { get; set; }
}

public class CharacterResponse
{
    [JsonProperty("status")]
    public string Status { get; set; }
}

public class ChatHistoryRequest
{
    [JsonProperty("userId")]
    public string UserId { get; set; }

    [JsonProperty("roomId")]
    public string RoomId { get; set; }

    [JsonProperty("characterId")]
    public string CharacterId { get; set; }
}

public class ChatHistoryResponseMessage {
    [JsonProperty("senderId")]
    public string SenderId { get; set; }

    [JsonProperty("senderName")]
    public string SenderName { get; set; }

    [JsonProperty("text")]
    public string Text { get; set; }
}
public class ChatHistoryResponse
{
    [JsonProperty("chat_history")]
    public List<ChatHistoryResponseMessage> ChatHistory { get; set; }
}

public class AddEventRequest
{
    [JsonProperty("event")]
    public string Event { get; set; }

    [JsonProperty("userId")]
    public string UserId { get; set; }

    [JsonProperty("roomId")]
    public string RoomId { get; set; }

    [JsonProperty("characterId")]
    public string CharacterId { get; set; }

    [JsonProperty("model")]
    public string Model { get; set; }

    [JsonProperty("apiKey")]
    public string ApiKey { get; set; }
}