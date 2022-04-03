using System.Collections;
using System.Collections.Generic;
using Controllers;
using UnityEditor;
using UnityEngine;

public class GameController : MonoBehaviour
{
    private EndGameScreenController endGameScreenController;

    [SerializeField] public bool isInvincible;
    public bool gameRunning { get; private set; }

    // Start is called before the first frame update
    void Start()
    {
        endGameScreenController = FindObjectOfType<EndGameScreenController>();
        gameRunning = true;
    }

    void ResetGame()
    {
        if (!isInvincible)
        {
            BroadcastMessage("onGameReset");
            gameRunning = true;
        }
    }

    // Update is called once per frame
    void GameFinished()
    {
        gameRunning = false;
        endGameScreenController.showEndGameScreen();
    }
}
