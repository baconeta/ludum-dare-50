using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Controllers
{
    public class StatsController : MonoBehaviour
    {
        public float time { get; private set; }
        private bool _timerRunning = false;
    
        public List<string> FormattedTimes { get; private set; }
        public List<float> RawTimes { get; private set; }
    
        public int deaths { get; private set; }
        public int eatenFoods { get; private set; }
        public int objectsSmashed { get; private set; }
        public int bridgesCrossed { get; private set; }
        public int bouldersBumped { get; private set; }

        private void Start()
        {
            FormattedTimes = new List<string>();
            RawTimes = Enumerable.Repeat(0.0f, 5).ToList();
        }

        private void Update()
        {
            if (_timerRunning)
            {
                time += Time.deltaTime;
            }
        }

        public void onGameReset()
        {
            StartTimer();
        }

        public void StartTimer()
        {
            time = 0;
            _timerRunning = true;
        }

        public void onGameEnd()
        {
            _timerRunning = false;
            // Add the current time, sort all times.
            RawTimes.Add(time);
            RawTimes.Sort();
            // Only keep the top 5 times.
            RawTimes = RawTimes.GetRange(0, 5);
            // Repopulate formatted times.
            FormattedTimes.Clear();
            foreach (float score in RawTimes) {
                FormattedTimes.Add(FormatTime(time));
            }            
        }

        public string FormatTime(float t)
        {
            int minutes = Mathf.FloorToInt(t / 60F);
            int seconds = Mathf.FloorToInt(t % 60F);
            int milliseconds = Mathf.FloorToInt((t * 100F) % 100F);
            return minutes.ToString ("00") + ":" + seconds.ToString ("00") + ":" + milliseconds.ToString("00");
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

        public void IncrementBouldersBumped()
        {
            bouldersBumped++;
        }
    }
}
