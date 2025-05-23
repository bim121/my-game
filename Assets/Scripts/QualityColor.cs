using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public enum Quality { Common, Uncommon, Rare, Epic }

public static class QualityColor 
{
    private static Dictionary<Quality, string> colors = new Dictionary<Quality, string>
{
    { Quality.Common, "#d6d6d6" },
    { Quality.Uncommon, "#00ff00ff" },
    { Quality.Rare, "#0000ffff" },
    { Quality.Epic, "#800080ff" }
};

    public static Dictionary<Quality, string> MyColor
    {
        get
        {
            return colors;
        }
    }
}
