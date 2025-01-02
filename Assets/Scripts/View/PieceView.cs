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
            blockModel.piecePosition = new Vector2Int(blockModel.piecePosition.x, blockModel.piecePosition.y);
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

    public Vector2Int GetPiecePosition()
    {
        float zRotation = transform.rotation.eulerAngles.z;
        Vector2 offset = this.posOffset;
        if (Mathf.Approximately(zRotation, 0) || Mathf.Approximately(zRotation, 180) ||
            Mathf.Approximately(zRotation, 360) || Mathf.Approximately(zRotation, -180) ||
            Mathf.Approximately(zRotation, -360))
        {
            offset = this.posOffset; //Keep Horizontal offset
        }
        else if (Mathf.Approximately(zRotation, 90) || Mathf.Approximately(zRotation, 270) ||
                 Mathf.Approximately(zRotation, -90f) || Mathf.Approximately(zRotation, -270))
        {
            offset = new Vector2(posOffset.y, posOffset.x); //Change to vertical offset
        }

        Vector2Int piecePos = new Vector2Int(
            Mathf.RoundToInt(transform.position.x + offset.x),
            Mathf.RoundToInt(transform.position.y + offset.y));

        return piecePos;
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
        MouseInputManager.instance.OnPieceSelected(this);
    }
}