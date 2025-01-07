using System.Collections.Generic;
using Enums;
using UnityEngine;

public class KingMeepleView : MonoBehaviour
{
    public Color kingColor;
    public MeepleType meepleType = MeepleType.NONE;
    public MeepleState meepleState = MeepleState.IDLE;
    public SpriteRenderer crownSprite;
    public BlockView blockView;
    [SerializeField] private Animator _animator;

    void Awake()
    {
        if (crownSprite)
        {
            crownSprite.color = kingColor;
        }

        this._animator = this.GetComponent<Animator>();
    }

    public void UpdateMeepleStateBasedOnNeighbors(List<MeepleType> adjacentMeepleTypes)
    {
        MeepleType enemyType = GetEnemyMeepleType(this.meepleType);
        bool isAngry = adjacentMeepleTypes.Contains(enemyType);

        if (isAngry && meepleState == MeepleState.IDLE)
        {
            meepleState = MeepleState.ANGRY;
            this._animator.CrossFade("Angry", .5f, 0);
        }
        else if (!isAngry && meepleState == MeepleState.ANGRY)
        {
            meepleState = MeepleState.IDLE;
            this._animator.CrossFade("Idle", .5f, 0);
        }
    }

    public static MeepleType GetEnemyMeepleType(MeepleType baseType)
    {
        switch (baseType)
        {
            case MeepleType.NONE:
                return MeepleType.NONE;
            case MeepleType.RED_KING:
                return MeepleType.BLUE_KING;
            case MeepleType.BLUE_KING:
                return MeepleType.RED_KING;
            case MeepleType.YELLOW_KING:
                return MeepleType.PURPLE_KING;
            case MeepleType.PURPLE_KING:
                return MeepleType.YELLOW_KING;
            default:
                return MeepleType.NONE;
        }
    }
}