using System.Collections.Generic;
using UnityEngine;

namespace Controllers
{
    public class StatsController : MonoBehaviour
    {
        public float time { get; private set; }
        private bool _timerRunning = false;
    
        public List<string> times { get; private set; }
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
            if (_timerRunning)
            {
                time += Time.deltaTime;
            }
        }

        public void EndRun()
        {
            _timerRunning = false;
            times.Add(GetFormattedTime());
            scores.Add(CalculateScore());
        }

        public string GetFormattedTime()
        {
            int minutes = Mathf.FloorToInt(time / 60F);
            int seconds = Mathf.FloorToInt(time % 60F);
            int milliseconds = Mathf.FloorToInt((time * 100F) % 100F);
            return minutes.ToString ("00") + ":" + seconds.ToString ("00") + ":" + milliseconds.ToString("00");
        }
    
        private int CalculateScore()
        {
            return (int) time + eatenFoods + objectsSmashed + bridgesCrossed;
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
}
