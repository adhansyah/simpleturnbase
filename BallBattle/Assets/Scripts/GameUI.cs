using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameUI : MonoBehaviour
{
    public Globals Globals;
    public GameManager GameManager;

    public TextMeshProUGUI timeText;
    public TextMeshProUGUI enemyNameText;
    public TextMeshProUGUI playerNameText;
    public TextMeshProUGUI currentEnemyPointsText;
    public TextMeshProUGUI currentPlayerPointsText;
    public GameObject enemyEnergyBarObj;
    public GameObject playerEnergyBarObj;
    public GameObject energyPoint;

    public GameObject countdownObj;
    public TextMeshProUGUI countdownText;

    public GameObject pausePanel;
    public GameObject toggleARBtn;
    public TextMeshProUGUI bgmText;
    public TextMeshProUGUI soundText;

    public GameObject matchResultObj;
    public TextMeshProUGUI matchResultTitleText;
    public TextMeshProUGUI enemyPointsText;
    public TextMeshProUGUI playerPointsText;
    public TextMeshProUGUI nextMatchBtnText;

    void Start()
    {
        for (int i = 0; i < Params.energyBarCount; i++) {
            GameObject eE = Instantiate(energyPoint, enemyEnergyBarObj.transform);
            EnergyPoint eEP = eE.GetComponent<EnergyPoint>();
            eEP.SetColor(true);
            GameManager.AddEnergy(true, eEP);
            
            GameObject eP = Instantiate(energyPoint, playerEnergyBarObj.transform);
            EnergyPoint ePP = eP.GetComponent<EnergyPoint>();
            ePP.SetColor(false);
            GameManager.AddEnergy(false, ePP);
        }

        if (Globals.bgm)
        {
            bgmText.text = "BGM: Yes";
        }
        else
        {
            bgmText.text = "BGM: No";
        }

        if (Globals.sound)
        {
            soundText.text = "Sound: Yes";
        }
        else
        {
            soundText.text = "Sound: No";
        }

        if (Globals.supportsAR)
        {
            if (Globals.usingAR)
            {
                toggleARBtn.GetComponentInChildren<TextMeshProUGUI>().text = "Non-AR mode";
            }
            else
            {
                toggleARBtn.GetComponentInChildren<TextMeshProUGUI>().text = "AR mode";
            }
        }
        else
        {
            toggleARBtn.SetActive(false);
        }
    }

    public void StartCountdown()
    {
        if (!gameObject.activeInHierarchy) return;
        if (GameManager.enemy.isAtk)
        {
            enemyNameText.text = enemyNameText.text.Replace("Defender", "Attacker");
            playerNameText.text = playerNameText.text.Replace("Attacker", "Defender");
        }
        else
        {
            enemyNameText.text = enemyNameText.text.Replace("Attacker", "Defender");
            playerNameText.text = playerNameText.text.Replace("Defender", "Attacker");
        }
        countdownObj.SetActive(true);
        StartCoroutine(Countdown(3));
    }

    IEnumerator CountdownDone()
    {
        yield return new WaitForSeconds(1);
        countdownObj.SetActive(false);
        Globals.gamePaused = false;
        GameManager.MatchStart();
    }

    IEnumerator Countdown(int i)
    {
        countdownText.text = i.ToString();
        yield return new WaitForSeconds(1);
        if (i > 0) StartCoroutine(Countdown(i - 1));
        if (i == 1)
        {
            countdownText.text = "Game start!";
            StartCoroutine(CountdownDone());
        }
    }

    void Update()
    {
        if (Globals.gamePaused) return;
        GameManager.timeLeft -= Time.deltaTime;
        timeText.text = $"{GameManager.timeLeft:F1}s";
    }

    public void GamePause()
    {
        if (Globals.gamePaused) return;
        pausePanel.SetActive(true);
        Globals.gamePaused = true;
    }

    public void GameResume()
    {
        pausePanel.SetActive(false);
        Globals.gamePaused = false;
    }

    public void ToggleBGM()
    {
        bgmText.text = Globals.ToggleBGM();
    }

    public void ToggleSound()
    {
        soundText.text = Globals.ToggleSound();
    }

    public void ARBtn()
    {
        Globals.gamePaused = true;
        SceneManager.LoadScene("ARScene");
    }

    public void GameExit()
    {
        Globals.gamePaused = true;
        GameManager.matchOrd = 0;
        GameManager.playerPoint = 0;
        GameManager.enemyPoint = 0;
        SceneManager.LoadScene("MenuScene");
    }

    public void ShowResult(bool isPlayerWon, bool isPlayerAtk, bool isDraw = false)
    {
        enemyPointsText.text = GameManager.enemyPoint.ToString();
        playerPointsText.text = GameManager.playerPoint.ToString();
        currentEnemyPointsText.text = GameManager.enemyPoint.ToString();
        currentPlayerPointsText.text = GameManager.playerPoint.ToString();
        if (GameManager.matchOrd == Params.matchPerGame)
        {
            nextMatchBtnText.text = "New Game";
            if (GameManager.playerPoint > GameManager.enemyPoint)
            {
                matchResultTitleText.text = "You Won!";
                matchResultTitleText.color = Params.playerColor;
            }
            else if (GameManager.playerPoint < GameManager.enemyPoint)
            {
                matchResultTitleText.text = "You Lost!";
                matchResultTitleText.color = Params.enemyColor;
            }
            else
            {
                matchResultTitleText.text = "Draw!";
                matchResultTitleText.color = Color.gray;
            }
        }
        else
        {
            if (!isDraw)
            {
                if (isPlayerWon)
                {
                    if (isPlayerAtk)
                    {
                        matchResultTitleText.text = "Goal!!!";
                    }
                    else
                    {
                        matchResultTitleText.text = "Caught!!!";
                    }
                    matchResultTitleText.color = Params.playerColor;
                }
                else
                {
                    if (isPlayerAtk)
                    {
                        matchResultTitleText.text = "Caught!!!";
                    }
                    else
                    {
                        matchResultTitleText.text = "Goal!!!";
                    }
                    matchResultTitleText.color = Params.enemyColor;
                }
            }
            else
            {
                matchResultTitleText.text = "Draw!!!";
                matchResultTitleText.color = Color.gray;
            }
        }
        matchResultObj.SetActive(true);
    }

    public void NextMatch()
    {
        if (GameManager.matchOrd < Params.matchPerGame)
        {
            matchResultObj.SetActive(false);
            GameManager.Start();
        }
        else
        {
            if (GameManager.playerPoint == GameManager.enemyPoint)
            {
                SceneManager.LoadScene("MainScene");
                GameManager.matchOrd = 0;
            }
            else
            {
                SceneManager.LoadScene("MainScene");
                GameManager.matchOrd = 0;
            }
        }
    }

    public void ExitMatch()
    {
        GameManager.matchOrd = 0;
        GameManager.playerPoint = 0;
        GameManager.enemyPoint = 0;
        SceneManager.LoadScene("MenuScene");
    }

    public GameObject DebuggingPanel;
    public void ToggleDebugger()
    {
        DebuggingPanel.SetActive(!DebuggingPanel.activeInHierarchy);
    }
}
