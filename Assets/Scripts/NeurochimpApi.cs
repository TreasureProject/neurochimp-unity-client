using UnityEngine;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

public class NeurochimpApi : MonoBehaviour
{
    private static NeurochimpApi _instance = null;
    public static NeurochimpApi Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindFirstObjectByType<NeurochimpApi>();
            }
            return _instance;
        }
    }
    private static readonly HttpClient client = new HttpClient();
    private string baseUrl = Constants.API_URL;

    public async Task<ChatResponse> PostChat(ChatRequest request)
    {
        string json = JsonConvert.SerializeObject(request);
        StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
        HttpResponseMessage response = await client.PostAsync($"{baseUrl}/chat", content);
        string responseContent = await response.Content.ReadAsStringAsync();
        return JsonConvert.DeserializeObject<ChatResponse>(responseContent);
    }

    public async Task<GameInfoResponse> PostGameInfo(GameInfoRequest request)
    {
        string json = JsonConvert.SerializeObject(request);
        StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
        HttpResponseMessage response = await client.PostAsync($"{baseUrl}/gameInfo", content);
        string responseContent = await response.Content.ReadAsStringAsync();
        return JsonConvert.DeserializeObject<GameInfoResponse>(responseContent);
    }

    public async Task<CharacterResponse> PostCharacter(CharacterRequest request)
    {
        string json = JsonConvert.SerializeObject(request);
        StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
        HttpResponseMessage response = await client.PostAsync($"{baseUrl}/character", content);
        string responseContent = await response.Content.ReadAsStringAsync();
        return JsonConvert.DeserializeObject<CharacterResponse>(responseContent);
    }

    public async Task<ChatHistoryResponse> GetChatHistory(ChatHistoryRequest request)
    {
        string json = JsonConvert.SerializeObject(request);
        StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
        HttpResponseMessage response = await client.PostAsync($"{baseUrl}/get_chat_history", content);
        string responseContent = await response.Content.ReadAsStringAsync();
        return JsonConvert.DeserializeObject<ChatHistoryResponse>(responseContent);
    }

    public async Task<string> PostAddEvent(AddEventRequest request)
    {
        string json = JsonConvert.SerializeObject(request);
        StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
        HttpResponseMessage response = await client.PostAsync($"{baseUrl}/add_event", content);
        string responseContent = await response.Content.ReadAsStringAsync();
        return responseContent;
    }
}