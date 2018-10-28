using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public static class CustomDeLogger {

    public static bool showHealthLog;


    [MenuItem("Precursor/My DebugLog/Health")]
    private static void showHealth(){
        CustomDeLogger.showHealthLog = !CustomDeLogger.showHealthLog;
        //Menu.SetChecked("Precursor/My DebugLog/Health", CustomDeLogger.showHealthLog);
    }

    [MenuItem("Precursor/My DebugLog/Health", true)]
    private static bool ToggleActionValidate()
    {
        Menu.SetChecked("Precursor/My DebugLog/Health", CustomDeLogger.showHealthLog);
        return true;
    }

    public static void DLog_Health(string debuglog)
    {
        if (!CustomDeLogger.showHealthLog) return;
        Debug.Log(debuglog);
    }

    public static void DLog_Health(string debuglog, string type)
    {
        if (!CustomDeLogger.showHealthLog) return;
        if (type == "warning")
        {
            Debug.LogWarning(debuglog);
        }
        else if (type == "errpr")
        {
            Debug.LogError(debuglog);
        }

    }
}
