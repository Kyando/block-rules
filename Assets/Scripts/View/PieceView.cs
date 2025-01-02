using Unity.VisualScripting;
using UnityEngine;

public class PieceView : MonoBehaviour
{
    public PieceModel.PieceType pieceType;
    public Color pieceColor;
    public PieceModel pieceModel;
    public Vector2 posOffset;
    public GameObject blockPrefab;
    private SpriteRenderer spriteRenderer;


    void Awake()
    {
        this.pieceModel = new PieceModel(this.pieceType, this.pieceColor);

        for (int i = 0; i < pieceModel.blocks.Length; i++)
        {
            BlockModel blockModel = pieceModel.blocks[i];
            blockModel.piecePosition = new Vector2Int(blockModel.piecePosition.x,blockModel.piecePosition.y);
            Vector3 blockPos = new Vector3(this.transform.position.x + blockModel.piecePosition.x + posOffset.x,
                this.transform.position.y + blockModel.piecePosition.y + posOffset.y, 0);
            GameObject block = Instantiate(blockPrefab, blockPos, Quaternion.identity, this.transform);
            block.transform.GetChild(0).GetComponent<SpriteRenderer>().color = pieceColor;
        }
        
        

        // BoxCollider2D collider2D = this.AddComponent<BoxCollider2D>();
    }

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        this.spriteRenderer.color = new Color(.6f, .6f, .6f, 1f);
    }

    // Update is called once per frame
    void Update()
    {
    }


    private void OnMouseEnter()
    {
        this.spriteRenderer.color = new Color(1f, 1f, 1f, 1f);
    }

    private void OnMouseExit()
    {
        this.spriteRenderer.color = new Color(.6f, .6f, .6f, 1f);
    }

    private void OnMouseDown()
    {
        Debug.Log("OnMouseDown");
        MouseInputManager.instance.OnPieceSelected(this);
    }
}