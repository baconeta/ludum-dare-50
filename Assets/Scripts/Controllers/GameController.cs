using System.Collections;
using System.Collections.Generic;
using Controllers;
using UnityEditor;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] public bool isInvincible;
    public bool gameRunning { get; set; }

    // Start is called before the first frame update
    void Start()
    {
        gameRunning = true;
        HUDController hud = FindObjectOfType<HUDController>();
        if (hud != null)
        {
            hud.GameStart();
        }
    }

    public void ResetGame()
    {
        BroadcastMessage("onGameReset");
        gameRunning = true;
    }

    // Update is called once per frame
    public void EndGame(float delay = 0f)
    {
        ;
        if (!isInvincible)
        {
            Invoke(nameof(BroadcastGameover), delay);
        }
    }

    private void BroadcastGameover()
    {
        BroadcastMessage("onGameEnd");
        gameRunning = false;
    }
}