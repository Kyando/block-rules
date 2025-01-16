using System.Collections.Generic;
using Enums;
using UnityEngine;
using UnityEngine.Serialization;

public class BaseMeepleView : MonoBehaviour
{
    [SerializeField] public MeepleModel meepleModel = new MeepleModel();
    public BlockView blockView;
    [SerializeField] private Animator _animator;

    void Awake()
    {
        this._animator = this.GetComponent<Animator>();
    }

    public void UpdateMeepleStateBasedOnNeighbors(List<KingdomType> adjacentKingdoms)
    {
        KingdomType enemyType = GetEnemyKingdomType(this.blockView.blockModel.kingdomType);
        bool isAngry = adjacentKingdoms.Contains(enemyType);

        if (isAngry && meepleModel.meepleState == MeepleState.IDLE)
        {
            meepleModel.meepleState = MeepleState.ANGRY;
            this._animator.CrossFade("Angry", .5f, 0);
        }
        else if (!isAngry && meepleModel.meepleState == MeepleState.ANGRY)
        {
            meepleModel.meepleState = MeepleState.IDLE;
            this._animator.CrossFade("Idle", .5f, 0);
        }
    }

    public void SetMeepleState(MeepleState meepleState)
    {
        if (meepleState == meepleModel.meepleState)
        {
            return;
        }

        if (meepleState == MeepleState.ANGRY)
        {
            meepleModel.meepleState = MeepleState.ANGRY;
            this._animator.CrossFade("Angry", .5f, 0);
        }
        else if (meepleState == MeepleState.IDLE)
        {
            meepleModel.meepleState = MeepleState.IDLE;
            this._animator.CrossFade("Idle", .5f, 0);
        }
    }

    public static KingdomType GetEnemyKingdomType(KingdomType baseType)
    {
        switch (baseType)
        {
            case KingdomType.NONE:
                return KingdomType.NONE;
            case KingdomType.RED_KINGDOM:
                return KingdomType.BLUE_KINGDOM;
            case KingdomType.BLUE_KINGDOM:
                return KingdomType.RED_KINGDOM;
            case KingdomType.YELLOW_KINGDOM:
                return KingdomType.PURPLE_KINGDOM;
            case KingdomType.PURPLE_KINGDOM:
                return KingdomType.YELLOW_KINGDOM;
            default:
                return KingdomType.NONE;
        }
    }
}