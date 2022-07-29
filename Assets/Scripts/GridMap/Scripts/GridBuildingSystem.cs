using System.Collections.Generic;
using UnityEngine;

using UnityEngine.InputSystem;

public class GridBuildingSystem : MonoBehaviour
{
    private bool buildMode;
    [SerializeField] private Canvas buildUI;
    [SerializeField] private int gridWidth = 2;
    [SerializeField] private int gridHeight = 2;
    [SerializeField] private float cellSize = 2;
    [SerializeField] GameObject objectOrigin;
    private GridCreator<GridObject> grid;
    [SerializeField] private GameObject objToInstantiate;
    [SerializeField] private GameObject startObject;
    SpawnManagerScriptableObject selectedObject;
    private GameManager gameManager;

    [Header("procedural generation")]
    [SerializeField] private float noiseSize = 0.1f;
    [SerializeField] private float waterLevel = 0.5f;
    [Space(20)]
    [SerializeField] private GameObject[] treePrefabs;
    [SerializeField] private float treeNoiseSize = .05f;
    [SerializeField] private float treeDensity = .5f;

    private void Awake()
    {
        buildMode = false;
        grid = new GridCreator<GridObject>(gridWidth, gridHeight, cellSize, objectOrigin.transform.position + new Vector3(-gridWidth / 2, 0.5f, -gridHeight / 2), (GridCreator<GridObject> g, int x, int z) => new GridObject(g, x, z));

        TerrainProceduralGen();
        TreeProceduralGen();
    }
    private void TerrainProceduralGen()
    {

        float xOffset = Random.Range(-100, 100);
        float yOffset = Random.Range(-100, 100);
        float[,] noiseMap = new float[gridWidth, gridHeight];

        for (int x = 0; x < gridWidth; x++)
        {
            for (int y = 0; y < gridHeight; y++)
            {
                float noiseValue = Mathf.PerlinNoise(x * noiseSize + xOffset, y * noiseSize + yOffset);
                noiseMap[x, y] = noiseValue;
            }
        }


        float[,] fallofMap = new float[gridHeight, gridWidth];

        for (int x = 0; x < gridWidth; x++)
        {
            for (int y = 0; y < gridHeight; y++)
            {
                float xv = x / (float)gridWidth * 2 - 1;
                float yv = y / (float)gridHeight * 2 - 1;
                float v = Mathf.Max(Mathf.Abs(xv), Mathf.Abs(yv));
                fallofMap[x, y] = Mathf.Pow(v, 3f) / (Mathf.Pow(v, 3f) + Mathf.Pow(2.2f - 2.2f * v, 3f));
            }
        }


        for (int x = 0; x < gridWidth; x++)
        {
            for (int z = 0; z < gridHeight; z++)
            {

                GridObject gridObject = grid.GetGridObject(x, z);
                float noiseValue = noiseMap[x, z];
                noiseValue -= fallofMap[x, z];
                if (noiseValue > waterLevel)
                {
                    GameObject buildObj = Instantiate(startObject, grid.GetWorldPosition(x, z), Quaternion.identity);
                    gridObject.SetObject(buildObj, buildObj.GetComponentInChildren<TileManager>().scriptableObject);
                }
                else gridObject.SetObject(null, null);
            }
        }

    }
    private void TreeProceduralGen()
    {
        (float xOffset, float yOffset) = (Random.Range(-100, 100), Random.Range(-100, 100));
        float[,] noiseMap = new float[gridWidth, gridHeight];

        for (int x = 0; x < gridWidth; x++)
        {
            for (int y = 0; y < gridHeight; y++)
            {
                float noiseValue = Mathf.PerlinNoise(x * treeNoiseSize + xOffset, y * treeNoiseSize + yOffset);
                noiseMap[x, y] = noiseValue;
            }
        }

        for (int x = 0; x < gridWidth; x++)
        {
            for (int y = 0; y < gridHeight; y++)
            {
                GridObject gridObject = grid.GetGridObject(x, y);
                float noiseValue = noiseMap[x, y];
                if (gridObject.gridObject != null)
                {
                    float density = Random.Range(0f, treeDensity);
                    if (noiseValue < density)
                    {
                        GameObject randonTree = treePrefabs[Random.Range(0, treePrefabs.Length)];
                        GameObject tree = Instantiate(randonTree, gridObject.gridObject.transform);
                        tree.transform.localScale = Vector3.one * Random.Range(.8f, 1.2f);
                        tree.transform.rotation = Quaternion.Euler(0, Random.Range(0, 360), 0);
                    }
                }

            }
        }

    }

    private void OnClick(InputAction.CallbackContext obj)
    {
        print("click");


        if (buildMode == true)
        {
            Vector2 mousePos = Input.mousePos.ReadValue<Vector2>();
            Ray ray = Camera.main.ScreenPointToRay(mousePos);
            if (Physics.Raycast(ray, out RaycastHit raycastHit, 99f, LayerMask.GetMask("Grid Raycast")))
            {
                grid.GetXZ(raycastHit.point, out int x, out int z);

                GridObject gridObject = grid.GetGridObject(x, z);

                if (gridObject.CanBuild(selectedObject))
                {
                    GameObject buildObj = Instantiate(selectedObject.objectToIntantiate, gridObject.gridObject.transform);
                }
                else if (selectedObject.destroyTerrain && !gridObject.isGround())
                {
                    Destroy(gridObject.gridObject);
                }
                else Debug.LogWarning("Can't build here");
            }
        }
        else
        {
            if (selectedObject != null)
            {


            }

        }
    }

    public void SetSO(SpawnManagerScriptableObject SOobject)
    {

        selectedObject = SOobject;

    }

    public void GetGridObjects(List<TileManager> tileManagers)
    {
        for (int x = 0; x < gridWidth; x++)
        {
            for (int z = 0; z < gridHeight; z++)
            {

                GridObject gridObject = grid.GetGridObject(x, z);
                tileManagers.Add(gridObject.gridObject.GetComponentInChildren<TileManager>());
            }
        }
    }

    void Start()
    {
        Input.fire.performed += OnClick;
        gameManager = FindObjectOfType<GameManager>();
    }
    public void EnterBuildMode()
    {
        buildMode = true;
        buildUI.enabled = true;
    }
    public void ExitBuildMode()
    {
        buildMode = false;
        buildUI.enabled = false;
    }




    // Update is called once per frame
    void Update()
    {

    }
}

public class GridObject
{
    private SpawnManagerScriptableObject SOobject;
    private GridCreator<GridObject> grid;
    private int x;
    private int z;
    public GameObject gridObject { get; private set; }
    public TileManager Tile { get; private set; }

    public GridObject(GridCreator<GridObject> grid, int x, int z)
    {
        this.grid = grid;
        this.x = x;
        this.z = z;
    }

    public bool isGround()
    {
        return SOobject.isGround;
    }

    public void SetObject(GameObject gridObject, SpawnManagerScriptableObject SOobject)
    {
        this.SOobject = SOobject;
        this.gridObject = gridObject;
        if (gridObject != null) Tile = gridObject.GetComponentInChildren<TileManager>();
    }

    public bool CanBuild(SpawnManagerScriptableObject SOobject)
    {

        if (gridObject == null || this.SOobject != SOobject)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void ClearTransform()
    {
        gridObject = null;
    }

}