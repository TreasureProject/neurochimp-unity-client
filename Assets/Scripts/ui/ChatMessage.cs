using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ChatMessage : MonoBehaviour
{
    [SerializeField] private TMP_Text senderText;
    [SerializeField] private TMP_Text contentText;
    [SerializeField] private Image backgroundImage;
    [SerializeField] private Color usersColor;
    [SerializeField] private Color othersColor;

    public void Init(string sender, string content, bool isLocal)
    {
        senderText.text = $"{sender}:";
        contentText.text = content;
        backgroundImage.color = isLocal ? usersColor : othersColor;
    }
}