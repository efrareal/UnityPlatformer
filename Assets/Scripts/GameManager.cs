using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState
{
    menu,
    inGame,
    gameOver
}

public class GameManager : MonoBehaviour
{
    public static GameManager sharedInstance;

    public GameState currentGameState = GameState.menu;

    public GameObject uiTitlePanel;
    public GameObject gameOverUIPanel;

    private void Awake()
    {
        sharedInstance = this;
    }

    private void Start()
    {
        BackToMenu();
    }

    private void Update()
    {
        if (Input.GetKeyDown("s") && currentGameState != GameState.inGame)
        {
            StartGame();
        }
    }

    public void StartGame()
    {
        SetGameState(GameState.inGame);
        PlayerController.sharedInstance.StartGame();
    }

    public void GameOver()
    {
        SetGameState(GameState.gameOver);
    }

    public void BackToMenu()
    {
        SetGameState(GameState.menu);
    }

    void SetGameState(GameState newGameState)
    {
        if(newGameState == GameState.menu)
        {
            uiTitlePanel.SetActive(true);
            gameOverUIPanel.SetActive(false);
        }
        else if(newGameState == GameState.inGame)
        {
            uiTitlePanel.SetActive(false);
            gameOverUIPanel.SetActive(false);
        }
        else if(newGameState == GameState.gameOver)
        {
            gameOverUIPanel.SetActive(true);
            uiTitlePanel.SetActive(false);
        }

        this.currentGameState = newGameState;
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
