using UnityEngine;

public class GridModel : MonoBehaviour
{
    [SerializeField] private int _gridWidth = 3;
    [SerializeField] private int _gridHeight = 3;

    private CellGridModel[,] _grid;
    public GameObject cellPrefab;
    public Camera camera;

    public int[,] piece = new int[4, 4]
    {
        { 0, 0, 0, 0 }, 
        { 0, 1, 0, 0 }, 
        { 0, 1, 1, 0 }, 
        { 0, 1, 0, 0 }
    };

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // this.transform.position = new Vector3((_gridWidth *0.5f)-.05f, (_gridHeight *0.5f) -.05f, 0);
        camera.transform.position = new Vector3((_gridWidth * 0.5f) - .5f, (_gridHeight * 0.5f) - .5f,
            camera.transform.position.z);
        _grid = new CellGridModel[_gridWidth, _gridHeight];
        for (int y = 0; y < _gridHeight; y++)
        {
            for (int x = 0; x < _gridWidth; x++)
            {
                _grid[x, y] = new CellGridModel(new Vector2Int(x, y), Color.gray);
                Vector3 position = new Vector3(x, y, 0);
                GameObject go = Instantiate(cellPrefab, position, Quaternion.identity, this.transform);
                go.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().color = _grid[x, y].color;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
    }
}