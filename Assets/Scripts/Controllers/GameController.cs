using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public bool gameRunning { get; private set; }

    // Start is called before the first frame update
    void Replay()
    {
        
    }

    // Update is called once per frame
    void GameFinished()
    {
        gameRunning = false;
        // TODO Show stats.
    }
}
