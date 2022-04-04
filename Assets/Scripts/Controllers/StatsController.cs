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
        public int foodsEaten { get; private set; }
        public int objectsSmashed { get; private set; }
        public int bridgesCrossed { get; private set; }
        public int bouldersBumped { get; private set; }

        private void Start()
        {
            // Add five zeros to start with.
            RawTimes = Enumerable.Repeat(0.0f, 5).ToList();
            RawTimes.Add(PlayerPrefs.GetFloat("BestTime1"));
            RawTimes.Add(PlayerPrefs.GetFloat("BestTime2"));
            RawTimes.Add(PlayerPrefs.GetFloat("BestTime3"));
            RawTimes.Add(PlayerPrefs.GetFloat("BestTime4"));
            RawTimes.Add(PlayerPrefs.GetFloat("BestTime5"));
            FormattedTimes = new List<string>();
            RepopulateFormattedTimes();

            deaths = PlayerPrefs.GetInt("TotalDeaths");
            foodsEaten = PlayerPrefs.GetInt("TotalFoodsEaten");
            objectsSmashed = PlayerPrefs.GetInt("TotalObjectsSmashed");
            bridgesCrossed = PlayerPrefs.GetInt("TotalBridgesCrossed");
            bouldersBumped = PlayerPrefs.GetInt("TotalBouldersBumped");
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
            RawTimes.Add(time);
            SaveBestTimes();
            RepopulateFormattedTimes();

            PlayerPrefs.SetInt("TotalDeaths", deaths);
            PlayerPrefs.SetInt("TotalFoodsEaten", foodsEaten);
            PlayerPrefs.SetInt("TotalObjectsSmashed", objectsSmashed);
            PlayerPrefs.SetInt("TotalBridgesCrossed", bridgesCrossed);
            PlayerPrefs.SetInt("TotalBouldersBumped", bouldersBumped);
            PlayerPrefs.Save();
        }

        private void SaveBestTimes()
        {
            // Only keep the 5 best times.
            RawTimes.Sort();
            RawTimes = RawTimes.GetRange(0, 5);
            for (int i = 0; i < 5; i++) {
                PlayerPrefs.SetFloat("BestTime"+(i+1), RawTimes[i]);
            }
        }

        private void RepopulateFormattedTimes()
        {
            // Only keep the 5 best times.
            RawTimes.Sort();
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
            foodsEaten++;
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
