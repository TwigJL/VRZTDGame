using UnityEngine;
using System.Text.RegularExpressions;

public class HideConsoleWarnings : MonoBehaviour
{
    [SerializeField] private string[] warningMessagesToHide;

    void OnEnable()
    {
        Application.logMessageReceived += HandleLog;
    }

    void OnDisable()
    {
        Application.logMessageReceived -= HandleLog;
    }

    void HandleLog(string logString, string stackTrace, LogType type)
    {
        if (type == LogType.Warning || type == LogType.Error)
        {
            foreach (string warningMessage in warningMessagesToHide)
            {
                if (Regex.IsMatch(logString, warningMessage))
                {
                    return;
                }
            }
        }

        Debug.unityLogger.Log(type, logString);
    }
}
