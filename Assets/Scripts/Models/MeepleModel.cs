using System.Collections.Generic;
using Enums;
using UnityEngine.Serialization;

[System.Serializable]
public class MeepleModel
{
    public MeepleType meepleType = MeepleType.NONE;
    public MeepleState meepleState = MeepleState.IDLE;
    public List<MeepleCategory> meepleCategories = new List<MeepleCategory>();
}