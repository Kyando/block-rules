using System.Collections.Generic;
using Enums;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

public class PieceView : MonoBehaviour
{
    public PieceModel.PieceType pieceType;
    public KingdomType kingdomType = KingdomType.NONE;
    private Color pieceColor;
    public PieceModel pieceModel;
    public Vector2 posOffset;
    public BlockView blockPrefab;
    public List<BlockView> blocks;
    public bool isOnGrid { get; private set; }

    public List<BaseMeepleView> meepleList = new List<BaseMeepleView>();

    [SerializeField] private bool isMouseOver = false;
    public bool isPieceSelected = false;
    private SpriteRenderer spriteRenderer = null;
    private SpriteRenderer bottomSpriteRenderer = null;


    void Awake()
    {
        float initialRotation = transform.eulerAngles.z;
        this.transform.rotation = Quaternion.Euler(0, 0, 0);
        this.pieceColor = ColorConstants.GetPieceColorFromKingdomType(kingdomType);
        this.pieceModel = new PieceModel(this.pieceType, this.pieceColor, kingdomType);

        for (int i = 0; i < transform.childCount; i++)
        {
            BaseMeepleView baseMeeple = transform.GetChild(i).GetComponent<BaseMeepleView>();
            if (baseMeeple)
            {
                meepleList.Add(baseMeeple);
            }
        }


        spriteRenderer = GetComponent<SpriteRenderer>();

        GameObject bottomSpriteObj = new GameObject("BottomSprite")
        {
            transform =
            {
                position = this.transform.position + new Vector3(0, -.25f, 0),
                rotation = this.transform.rotation,
                localScale = this.transform.localScale,
                parent = this.transform
            }
        };

        this.bottomSpriteRenderer = bottomSpriteObj.AddComponent<SpriteRenderer>();
        bottomSpriteRenderer.sprite = this.spriteRenderer.sprite;
        bottomSpriteRenderer.color =
            new Color(this.pieceColor.r * .8f, this.pieceColor.g * .8f, this.pieceColor.b * .8f, 1);
        bottomSpriteRenderer.sortingOrder = this.spriteRenderer.sortingOrder - 1;

        for (int i = 0; i < pieceModel.blocks.Length; i++)
        {
            BlockModel blockModel = pieceModel.blocks[i];
            blockModel.piecePosition = new Vector2Int(blockModel.piecePosition.x, blockModel.piecePosition.y);
            Vector3 blockPos = new Vector3(this.transform.position.x + blockModel.piecePosition.x + posOffset.x,
                this.transform.position.y + blockModel.piecePosition.y + posOffset.y, 0);
            BlockView blockView = Instantiate(blockPrefab, blockPos, Quaternion.identity, this.transform);
            blockView.blockModel = blockModel;
            blockView.GameObject().SetActive(false);
            blocks.Add(blockView);
        }

        InitializeMeeplesAndBlocks();
        InitializeRotation(initialRotation);

        // BoxCollider2D collider2D = this.AddComponent<BoxCollider2D>();
    }

    private void InitializeRotation(float initialRotation)
    {
        for (int i = 0; i < 8; i++)
        {
            RotatePieceClowckwise();
            if (Mathf.Approximately(initialRotation, transform.rotation.eulerAngles.z))
            {
                break;
            }
        }

        foreach (var meepleView in meepleList)
        {
            meepleView.transform.rotation = Quaternion.identity;
        }
    }

    public void InitializeMeeplesAndBlocks()
    {
        if (meepleList.Count == 0)
        {
            for (int j = 0; j < blocks.Count; j++)
            {
                blocks[j].blockModel.meepleModel = null;
            }

            return;
        }

        for (int i = 0; i < meepleList.Count; i++)
        {
            BaseMeepleView meeple = meepleList[i];
            BlockView blockView = null;
            for (int j = 0; j < blocks.Count; j++)
            {
                if (blockView is null)
                {
                    blockView = blocks[j];
                    continue;
                }

                BlockView newBlockView = blocks[j];
                float currentDist = Vector3.Distance(meeple.transform.position, blockView.transform.position);
                float newDist = Vector3.Distance(meeple.transform.position, newBlockView.transform.position);

                if (newDist < currentDist)
                {
                    blockView = newBlockView;
                }
            }

            if (blockView is not null)
            {
                meeple.blockView = blockView;
                blockView.blockModel.meepleModel = meeple.meepleModel;
            }
        }
    }

    void Start()
    {
        UpdateSpriteColors();
    }

    public Vector2Int GetPiecePosition()
    {
        float zRotation = transform.rotation.eulerAngles.z;
        Vector2 offset = this.posOffset;
        if (Mathf.Approximately(zRotation, 0) || Mathf.Approximately(zRotation, 360) ||
            Mathf.Approximately(zRotation, -360))
        {
            offset = this.posOffset; //Keep Horizontal offset
        }
        else if (Mathf.Approximately(zRotation, 90) || Mathf.Approximately(zRotation, -270))
        {
            offset = new Vector2(-posOffset.y, posOffset.x); //wrong
        }
        else if (Mathf.Approximately(zRotation, 180) || Mathf.Approximately(zRotation, -180))
        {
            offset = new Vector2(-posOffset.x, -posOffset.y);
        }

        if (Mathf.Approximately(zRotation, 270) || Mathf.Approximately(zRotation, -90f))
        {
            offset = new Vector2(posOffset.y, -posOffset.x); //wrong
        }

        Vector2Int piecePos = new Vector2Int(
            Mathf.RoundToInt(transform.position.x + offset.x),
            Mathf.RoundToInt(transform.position.y + offset.y));

        foreach (var meeple in meepleList)
        {
            meeple.transform.rotation = Quaternion.identity;
        }


        return piecePos;
    }

    public void RotatePieceClowckwise()
    {
        Vector3 rotationEuler = this.transform.rotation.eulerAngles;
        float zRotation = rotationEuler.z - 90;
        if (zRotation == 0 || zRotation == -360)
            zRotation = 360;
        this.transform.rotation = Quaternion.Euler(0, 0, zRotation);
        if (pieceModel is not null)
        {
            this.pieceModel.RotatePiece(clockwise: true);
            this.bottomSpriteRenderer.transform.position = this.transform.position + new Vector3(0, -.25f, 0);
        }
    }

    public void SetIsPieceOnGrid(bool isOnGrid)
    {
        this.isOnGrid = isOnGrid;
        UpdateSpriteColors();
    }

    public void UpdateColorsBasedOnKingdomType()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        this.pieceColor = ColorConstants.GetPieceColorFromKingdomType(kingdomType);
        spriteRenderer.color = new Color(pieceColor.r, pieceColor.g, pieceColor.b, 1f);
    }

    public void UpdateSpriteColors()
    {
        if (isPieceSelected || isOnGrid || isMouseOver)
        {
            this.spriteRenderer.color = new Color(pieceColor.r, pieceColor.g, pieceColor.b, 1f);
            this.bottomSpriteRenderer.color = new Color(pieceColor.r * .8f, pieceColor.g * .8f, pieceColor.b * .8f, 1f);
            return;
        }

        this.spriteRenderer.color = new Color(pieceColor.r * .6f, pieceColor.g * .6f, pieceColor.b * .6f, 1f);
        this.bottomSpriteRenderer.color = new Color(pieceColor.r * .6f * .8f, pieceColor.g * .6f * .8f,
            pieceColor.b * .6f * .8f, 1f);
    }


    private void OnMouseEnter()
    {
        this.isMouseOver = true;
        UpdateSpriteColors();
    }

    private void OnMouseExit()
    {
        this.isMouseOver = false;
        UpdateSpriteColors();
    }


    private void OnMouseDown()
    {
        MouseInputManager.instance.OnPieceClicked(this);
    }
}