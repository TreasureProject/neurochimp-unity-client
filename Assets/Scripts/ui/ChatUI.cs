using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections.Generic;

public class ChatUI : MonoBehaviour
{
    [SerializeField] private TMP_InputField messageInputField;
    [SerializeField] private Button sendButton;
    [SerializeField] private Transform spawnLocation;
    [SerializeField] private GameObject chatMessagePrefab;
    [SerializeField] private Character character;
    private List<ChatMessage> _messages = new List<ChatMessage>();
    private bool _sending = false;

    private async void Start()
    {
        sendButton.onClick.AddListener(SendChatMessage);
        var chatHistory = await NeurochimpApi.Instance.GetChatHistory(new ChatHistoryRequest()
        {
            UserId = Constants.USER_ID,
            RoomId = GameManager.Instance.RoomId,
            CharacterId = character.getId()
        });
        if (chatHistory != null && chatHistory.ChatHistory != null)
        {
            for (int i = 0; i < chatHistory.ChatHistory.Count; i++)
            {
                CreateChatObject(chatHistory.ChatHistory[i].SenderName, chatHistory.ChatHistory[i].Text, chatHistory.ChatHistory[i].SenderId == Constants.USER_ID);
            }
        }
    }

    private void OnDestroy()
    {
        sendButton.onClick.RemoveListener(SendChatMessage);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            SendChatMessage();
        }
    }

    private void CreateChatObject(string sender, string content, bool isLocal)
    {
        GameObject chatObject = Instantiate(chatMessagePrefab, spawnLocation);
        ChatMessage chatMessage = chatObject.GetComponent<ChatMessage>();
        chatMessage.Init(sender, content, isLocal);
        _messages.Add(chatMessage);
    }

    private void UpdateChatState(bool available) {
        _sending = !available;
        sendButton.enabled = available;
    }

    private async void SendChatMessage()
    {
        if (_sending)
        {
            return;
        }

        string content = messageInputField.text.Trim();
        messageInputField.text = string.Empty;
        if (string.IsNullOrEmpty(content))
        {
            return;
        }

        UpdateChatState(false);
        try
        {
            CreateChatObject(Constants.USER_NAME, content, true);
            var response = await NeurochimpApi.Instance.PostChat(new ChatRequest()
            {
                AgentType = "npcAgent",
                Message = new Message()
                {
                    Sender = Constants.USER_NAME,
                    SenderId = Constants.USER_ID,
                    IsAgent = false,
                    message = content,
                    Image = "",
                    Timestamp = System.DateTime.Now.Ticks,
                    RoomId = GameManager.Instance.RoomId,
                },
                Model = "gpt-4o-mini",
                CustomGameState = "",
                ExtraFields = new string[] { "emotion: 'string, explain in one word (Available: admire, confused, cringe_in_disqust, cringe_in_fear) you are feeling right now after this interaction. You must choose from the available ones or leave it empty'" },
                CharacterId = character.getId(),
                RoomId = GameManager.Instance.RoomId,
                ApiKey = Constants.API_KEY,
            });
            var responseText = response.Response;
            character.PlayEmotion(response.Emotion);
            if (!string.IsNullOrEmpty(responseText))
            {
                CreateChatObject(character.getName(), responseText, false);
                character.ShowChatBubble(responseText);
            }
            UpdateChatState(true);
        }
        catch (System.Exception e)
        {
            Debug.LogError(e);
            UpdateChatState(true);
        }
    }
}