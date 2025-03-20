using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance = null;
    public static GameManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindFirstObjectByType<GameManager>();
            }
            return _instance;
        }
    }

    [Tooltip("Room ID, can be set for the game server or instance - in case of Multiplayer, it's used for the short term memories")]
    [SerializeField] public string RoomId;
    [SerializeField][TextArea] private string GameInfo;
    [SerializeField] private string GameName;
    [Tooltip("Game mode is either 2D or 3D")]
    [SerializeField] private string GameMode = "2d";
    [SerializeField] private Character character;

    private void Start()
    {
        NeurochimpApi.Instance.PostGameInfo(new GameInfoRequest()
        {
            GameInfo = this.GameInfo,
            UserId = Constants.USER_ID,
            RoomId = RoomId,
            GameName = this.GameName,
            GameInputInfo = "",
            GameMode = this.GameMode,
            CharacterId = character.getId(),
        });
    }

    public void SendEvent(Character character, string eventData) {
        NeurochimpApi.Instance.PostAddEvent(new AddEventRequest()
        {
            CharacterId = character.getId(),
            RoomId = RoomId,
            Event = eventData,
            ApiKey = Constants.API_KEY,
            Model = "gpt-4o-mini",
            UserId = Constants.USER_ID,
        });
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            SendEvent(character, "New game event");
        }
    }
}