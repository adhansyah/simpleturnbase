using TMPro;
using UnityEngine;

public class OnscreenDebugOutput : MonoBehaviour
{
    public GameObject logPanel;
    public GameObject logPrefab;
    public TextMeshProUGUI toggleLogBtnText;

    public bool stopLogging = false;

    public void ToggleLog()
    {
        if (stopLogging)
        {
            toggleLogBtnText.text = "Stop logging";
        }
        else
        {
            toggleLogBtnText.text = "Start logging";
        }
        stopLogging = !stopLogging;
    }

    public void ClearLog()
    {
        GameObject[] allLogs = new GameObject[logPanel.transform.childCount];
        int i = 0;

        foreach (Transform child in logPanel.transform)
        {
            allLogs[i] = child.gameObject;
            i += 1;
        }

        foreach (GameObject log in allLogs)
        {
            Destroy(log.gameObject);
        }
    }

    public void PrintLog(string text)
    {
        if (stopLogging) return;
        GameObject logObj = Instantiate(logPrefab, logPanel.transform);
        logObj.GetComponent<TextMeshProUGUI>().text = text;
    }

    public static void Log(string text)
    {
        OnscreenDebugOutput onscreenDebugOutput = FindObjectOfType<OnscreenDebugOutput>();
        if (onscreenDebugOutput != null)
        {
            onscreenDebugOutput.PrintLog(text);
        }
        else
        {
            Debug.Log(text);
        }
    }
}