using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;
using System.Threading.Tasks;
using AStar;
using TMPro;
using System.Collections.Generic;

public class Character : MonoBehaviour
{
    [SerializeField] private CharacterData data;
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private Animator animator;
    [SerializeField] private MapGrid grid;
    [SerializeField] private bool debugMouse = true;
    [SerializeField] private GameObject chatBubble;
    [SerializeField] private TMP_Text chatBubbleText;

    private (int, int)[] _path = new (int, int)[0];
    private int _pathIndex = 0;
    private Vector3 _target;
    private Coroutine _chatBubbleHideRoutine;
    private List<string> _availableEmotions = new List<string>()
    {
        "admire", "cringe_in_disqust" ,"cringe_in_fear", "confused"
    };

    private void Awake()
    {
        NeurochimpApi.Instance.PostCharacter(new CharacterRequest() { Character = Utils.ToCharacterInfo(this.data) });
    }

    private void Update()
    {
        if (debugMouse)
        {
            HandleMouseInput();
        }
    }

    // Used for debugging movement & animations
    private async void HandleMouseInput()
    {
        if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 targetPosition = new Vector2(mousePosition.x, mousePosition.y);
            await MoveToTarget(targetPosition);
        }
    }

    public async Task MoveToTarget(Vector2 position)
    {
        (int, int) startPosition = grid.worldSpaceToCordinate(transform.position);
        (int, int) targetPosition = grid.worldSpaceToCordinate(position);

        _path = await AStarPathfinding.GeneratePath(startPosition.Item1, startPosition.Item2, targetPosition.Item1, targetPosition.Item2, grid.walkableMap);

        if (_path.Length != 0)
        {
            _pathIndex = 0;
            _target = grid.cordinateToWorldSpace(_path[_pathIndex].Item1, _path[_pathIndex].Item2);
            StartCoroutine(FollowPath());
        }
    }

    private IEnumerator FollowPath()
    {
        while (_pathIndex < _path.Length)
        {
            _target = grid.cordinateToWorldSpace(_path[_pathIndex].Item1, _path[_pathIndex].Item2);
            Vector2 direction = (_target - transform.position).normalized;

            animator.SetFloat("MoveX", direction.x);
            animator.SetFloat("MoveY", direction.y);
            animator.SetBool("isMoving", true);

            while (Vector3.Distance(transform.position, _target) > 0.1f)
            {
                transform.position = Vector3.MoveTowards(transform.position, _target, moveSpeed * Time.deltaTime);
                yield return null;
            }
            _pathIndex++;
        }

        animator.SetBool("isMoving", false);
    }

    public string getId()
    {
        return data.id;
    }

    public string getName()
    {
        return data.name;
    }

    public void ShowChatBubble(string text)
    {
        if (_chatBubbleHideRoutine != null)
        {
            StopCoroutine(_chatBubbleHideRoutine);
            _chatBubbleHideRoutine = null;
        }
        chatBubble.SetActive(true);
        chatBubbleText.text = text;
        float seconds = text.Length * 0.1f;
        _chatBubbleHideRoutine = StartCoroutine(HideChatBubble(seconds));
    }

    private IEnumerator HideChatBubble(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        chatBubble.SetActive(false);
        _chatBubbleHideRoutine = null;
    }

    public void PlayEmotion(string emotion)
    {
        if (string.IsNullOrEmpty(emotion))
        {
            return;
        }
        if (!_availableEmotions.Contains(emotion))
        {
            Debug.LogError("Invalid emotion: " + emotion);
            return;
        }

        animator.SetTrigger(emotion);
    }
}