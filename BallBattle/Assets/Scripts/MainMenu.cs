using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.XR.ARFoundation;

public class MainMenu : MonoBehaviour
{
    public GameObject playARBtn;
    public GameObject mainMenu;
    public GameObject optionsMenu;
    public TextMeshProUGUI bgmText;
    public TextMeshProUGUI soundText;
    public Globals Globals;

    public ARSession aRSession;

    void Start()
    {
        StartCoroutine(checkAR());
    }
    public void LoadScene(string scene)
    {
        switch (scene)
        {
            case "MainScene":
                Globals.gamePaused = false;
                break;
            case "ARScene":
                Globals.usingAR = true;
                break;
        }
        SceneManager.LoadScene(scene);
    }

    public void OptionsMenu()
    {
        optionsMenu.SetActive(true);
        mainMenu.SetActive(false);
    }

    public void MenuBack()
    {
        optionsMenu.SetActive(false);
        mainMenu.SetActive(true);
    }

    public void ToggleBGM()
    {
        bgmText.text = Globals.ToggleBGM();
    }

    public void ToggleSound()
    {
        soundText.text = Globals.ToggleSound();
    }

    IEnumerator checkAR()
    {
        if ((ARSession.state == ARSessionState.None) ||
            (ARSession.state == ARSessionState.CheckingAvailability))
        {
            yield return ARSession.CheckAvailability();
        }
        if (ARSession.state == ARSessionState.Unsupported)
        {
            playARBtn.SetActive(false);
            Globals.supportsAR = false;
        }
        else
        {
            aRSession.enabled = true;
            Globals.supportsAR = true;
        }
    }
}
