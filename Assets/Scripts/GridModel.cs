using UnityEngine;

public class GridModel : MonoBehaviour
{
    [SerializeField] private int _gridWidth = 2;
    [SerializeField] private int _gridHeight = 3;

    private Color[,] _grid;
    public GameObject cellPrefab;
    public Camera camera;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        this.transform.position = new Vector3(-.05f, .05f, 0);
        camera.transform.position = new Vector3(_gridWidth * 0.5f, _gridHeight * 0.5f, camera.transform.position.z);
        _grid = new Color[_gridWidth, _gridHeight];
        Color[] row_colors = { Color.red, Color.green, Color.blue, };
        for (int y = 0; y < _gridHeight; y++)
        {
            for (int x = 0; x < _gridWidth; x++)
            {
                _grid[x, y] = row_colors[y];
                Vector3 position = new Vector3(x, y, 0);
                GameObject go = Instantiate(cellPrefab, position, Quaternion.identity, this.transform);
                go.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().color = _grid[x, y];
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
    }
}