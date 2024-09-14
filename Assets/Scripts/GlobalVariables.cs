using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GlobalVariables 
{
    public static bool CanAction = true;

    public static float DayDuration = 120f;
    public static float TimeCounter = 0;
    public static float timeMultiplier = 1f;

    public static int Gold = 1000;

    public static string FormatNumber(float number)
    {
        if (number >= 1000000)
        {
            return (number / 1000000f).ToString("0.0") + "M";
        }
        else if (number >= 1000)
        {
            return (number / 1000f).ToString("0.0") + "K";
        }
        else
        {
            return number.ToString("0");
        }
    }

    public static string FormatTime(float timeInSeconds)
    {
        int minutes = (int)(timeInSeconds / 60);
        int seconds = (int)(timeInSeconds % 60);
        return string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    public static bool CanAffordItem(int itemPrice)
    {
        return Gold - itemPrice >= 0;
    }
}
