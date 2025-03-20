using UnityEditor;
using UnityEngine;

public class MapGrid : MonoBehaviour
{
	private static MapGrid _instance;
	public static MapGrid Instance
	{
		get
		{
			if (_instance == null)
			{
				_instance = FindFirstObjectByType<MapGrid>();
			}
			return _instance;
		}
	}

	public bool[,] walkableMap;
	[SerializeField] private float gridWidth;
	[SerializeField] private float gridHeight;
	[SerializeField] private float cellSize;
	[SerializeField] private GameObject wallTilePrefab;
	[SerializeField] private GameObject groundTilePrefab;
	[SerializeField][Range(0f, 1f)] private float nonWalable;
	[SerializeField] private float mapScale;
	[SerializeField] private GameObject characterObject;

	private Vector2 cellSpacing = Vector2.zero;
	private Vector2 topLeft = Vector2.zero;

	private void destoyOldGrid()
	{
		for (int i = gameObject.transform.childCount - 1; i >= 0; i--)
		{
			DestroyImmediate(transform.GetChild(i).gameObject);
		}
	}

	void Start()
	{
		createGrid();
	}

    public void createGrid()
    {
        destoyOldGrid();

        topLeft = new Vector2(gameObject.transform.position.x - gridWidth / 2, gameObject.transform.position.y + gridHeight / 2);

        cellSpacing = new Vector2(cellSize, cellSize);

        int cellAmountX = (int)((gridWidth + 0.001f) / cellSpacing.x);
        int cellAmountY = (int)((gridHeight + 0.001f) / cellSpacing.y);
        walkableMap = new bool[cellAmountY, cellAmountX];

        demo2.NoiseGenerator noiseGenerator = new demo2.NoiseGenerator(true, 1 / mapScale);

        for (int y = 0; y < walkableMap.GetLength(0); y++)
        {
            float gridObjectYpos = topLeft.y - cellSpacing.y * y - cellSpacing.y / 2;

            for (int x = 0; x < walkableMap.GetLength(1); x++)
            {
                float gridObjectXPos = topLeft.x + cellSpacing.x * x + cellSpacing.x / 2;

                bool walkable = noiseGenerator.GetPerlinNoise(gridObjectXPos, gridObjectYpos) > nonWalable ? true : false;
                walkableMap[y, x] = walkable;

                GameObject tilePrefab = walkable ? groundTilePrefab : wallTilePrefab;

                if (!walkable)
                {
                    bool isAdjacentToWalkable = (x > 0 && walkableMap[y, x - 1]) ||
                                                (x < walkableMap.GetLength(1) - 1 && walkableMap[y, x + 1]) ||
                                                (y > 0 && walkableMap[y - 1, x]) ||
                                                (y < walkableMap.GetLength(0) - 1 && walkableMap[y + 1, x]);

                    if (isAdjacentToWalkable)
                    {
                        tilePrefab = wallTilePrefab;
                    }
                }

                // Ensure edge tiles are wall tiles if they are outside wall tiles
                if (x == 0 || x == walkableMap.GetLength(1) - 1 || y == 0 || y == walkableMap.GetLength(0) - 1)
                {
                    tilePrefab = wallTilePrefab;
                    walkableMap[y, x] = false;
                }

                Instantiate(tilePrefab, new Vector3(gridObjectXPos, gridObjectYpos, 0), Quaternion.identity, transform);
            }
        }
        MoveCharacterToWalkable();
    }

    private void MoveCharacterToWalkable() { 
	Vector2 targetPosition = new Vector2(Random.Range(topLeft.x, topLeft.x + gridWidth), Random.Range(topLeft.y - gridHeight, topLeft.y));
        if (!GetGridCell(targetPosition))
        {
            MoveCharacterToWalkable();
        }
        else
        {
            characterObject.transform.position = targetPosition;
        }
    }

	public void GenerateGrid()
	{
		createGrid();
	}

	public bool GetGridCell(int xCodrinate, int yCordinate)
	{
		return walkableMap[yCordinate, xCodrinate];
	}

	public bool GetGridCell(Vector3 worldPos)
	{
		cellSpacing = new Vector2(cellSize, cellSize);

		int xCord = Mathf.RoundToInt((worldPos.x - topLeft.x) / cellSpacing.x);
		int yCord = Mathf.RoundToInt((-worldPos.y + topLeft.y) / cellSpacing.y);

		return walkableMap[yCord, xCord];
	}

	public Vector3 cordinateToWorldSpace(int xCord, int yCord)
	{
		float worldPosX = topLeft.x + cellSpacing.x * xCord + cellSpacing.x / 2;
		float worldPosY = topLeft.y - cellSpacing.y * yCord - cellSpacing.y / 2;

		return new Vector3(worldPosX, worldPosY, 0);
	}

	public (int, int) worldSpaceToCordinate(Vector2 worldPos)
	{
		int xCord = Mathf.RoundToInt((worldPos.x - topLeft.x - cellSpacing.x / 2) / cellSpacing.x);
		int yCord = Mathf.RoundToInt((worldPos.y - topLeft.y + cellSpacing.y / 2) / -cellSpacing.y);

		return (xCord, yCord);
	}

	private void OnDrawGizmosSelected()
	{
		Gizmos.color = Color.blue;
		Gizmos.DrawWireCube(transform.position, new Vector3(gridWidth, gridHeight, 0));
	}
}


#if (UNITY_EDITOR)
namespace gridEditor
{
	[CustomEditor(typeof(MapGrid))]
	public class MapGridEditor : Editor
	{
		public override void OnInspectorGUI()
		{
			base.OnInspectorGUI();
			MapGrid mapGrid2 = target as MapGrid;

			if (GUILayout.Button("Generate Grid"))
			{
                mapGrid2.GenerateGrid();
			}
		}
	}
}
#endif