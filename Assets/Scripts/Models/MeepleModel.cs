using Enums;

[System.Serializable]
public class MeepleModel
{
    public MeepleType meepleType = MeepleType.NONE;
    public MeepleState meepleState = MeepleState.IDLE;
}