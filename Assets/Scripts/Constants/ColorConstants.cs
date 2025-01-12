using Enums;
using UnityEngine;

public abstract class ColorConstants
{
    public static Color GetPieceColorFromKingdomType(KingdomType kingdomType)
    {
        switch (kingdomType)
        {
            case KingdomType.NONE:
                return GetColorFromHex("#AAA7B0");
            case KingdomType.BLUE_KINGDOM:
                return GetColorFromHex("#58B4B9");
            case KingdomType.RED_KINGDOM:
                return GetColorFromHex("#BF5492");
            case KingdomType.YELLOW_KINGDOM:
                return GetColorFromHex("#DBA87D");
            case KingdomType.PURPLE_KINGDOM:
                return GetColorFromHex("#876ABF");
            default:
                throw new System.ArgumentException("Unknown Kingdom type");
        }
    }

    private static Color GetColorFromHex(string hexString)
    {
        Color newCol;
        if (ColorUtility.TryParseHtmlString(hexString, out newCol))
            return newCol;
        throw new System.ArgumentException("Failed to convert color from Kingdom type");
    }
}