using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugHandler : MonoBehaviour
{
    public static bool enableDebug = true;
    public static bool enableClickPropInfo = true;

    public static void Print(string message)
    {
        if (enableDebug)
        {
            Debug.Log(message);
        }
    }
}
