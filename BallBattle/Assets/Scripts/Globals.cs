using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Globals", menuName = "ScriptableObject/Globals")]
public class Globals : ScriptableObject
{
    public bool bgm = true;
    public bool sound = true;
    public bool supportsAR = true;
    public bool usingAR = false;

    // Well, I wanted to make this static, but this is easier to control
    // the game state within Unity Editor
    public bool gamePaused = true;

    public string ToggleBGM()
    {
        if (!bgm)
        {
            bgm = true;
            Debug.Log("Set BGM: true");
            return "BGM: Yes";
        }
        else
        {
            bgm = false;
            Debug.Log("Set BGM: false");
            return "BGM: No";
        }
        // TODO: Add bgm file ctrl
    }

    public string ToggleSound()
    {
        if (!sound)
        {
            sound = true;
            Debug.Log("Set Sound: true");
            return "Sound: Yes";
        }
        else
        {
            sound = false;
            Debug.Log("Set Sound: false");
            return "Sound: No";
        }
        // TODO: Add sound file ctrl
    }

#if UNITY_STANDALONE_WIN
    // need to rotate the field, UI, etc.
    // the default resolution should be 576x1024
#endif
}
