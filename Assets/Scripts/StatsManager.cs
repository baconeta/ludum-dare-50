using System.Collections.Generic;
using UnityEngine;

public class StatsManager : MonoBehaviour
{
    private float time = 0;
    private bool _timerRunning = false;
    
    public List<float> times { get; private set; }
    public List<int> scores { get; private set; }
    
    public int deaths { get; private set; }
    public int eatenFoods { get; private set; }
    public int objectsSmashed { get; private set; }
    public int bridgesCrossed { get; private set; }

    public void StartRun()
    {
        time = 0;
        _timerRunning = true;
        deaths = 0; 
        eatenFoods = 0;
        objectsSmashed = 0;
        bridgesCrossed = 0;
    }

    public void Update()
    {
        time = time + Time.deltaTime;
    }

    public void EndRun()
    {
        _timerRunning = false;
        times.Add(time);
        scores.Add(CalculateScore());
    }
    
    private int CalculateScore()
    {
        int score = (int)time + eatenFoods + objectsSmashed + bridgesCrossed;
        return score;
    }

    public void IncrementFoodEaten()
    {
        eatenFoods++;
    }

    public void IncrementObjectsSmashed()
    {
        objectsSmashed++;
    }

    public void IncrementBridgesCrossed()
    {
        bridgesCrossed++;
    }
}
