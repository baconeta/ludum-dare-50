using System.Collections;
using System.Collections.Generic;
using Controllers;
using UnityEditor;
using UnityEngine;

public class GameController : MonoBehaviour
{

    [SerializeField] public bool isInvincible;
    public bool gameRunning { get; private set; }

    // Start is called before the first frame update
    void Start()
    {
        gameRunning = true;
    }

    void ResetGame()
    {
        BroadcastMessage("onGameReset");
        gameRunning = true;
    }

    // Update is called once per frame
    void EndGame()
    {;
        if (!isInvincible)
        {
            BroadcastMessage("onGameEnd");
            gameRunning = false;
        }
    }
}
