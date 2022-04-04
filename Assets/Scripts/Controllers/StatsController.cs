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
        public string LatestTime { get; private set; }
    
        public int deaths { get; private set; }
        public int melonsMunched { get; private set; }
        public int sabersSlain { get; private set; }
        public int trunksTraversed { get; private set; }
        public int firesFoiled { get; private set; }

        private void Start()
        {
            // Add five zeros to start with.
            RawTimes = Enumerable.Repeat(0.0f, 5).ToList();
            RawTimes.Add(PlayerPrefs.GetFloat("BestTime1"));
            RawTimes.Add(PlayerPrefs.GetFloat("BestTime2"));
            RawTimes.Add(PlayerPrefs.GetFloat("BestTime3"));
            RawTimes.Add(PlayerPrefs.GetFloat("BestTime4"));
            RawTimes.Add(PlayerPrefs.GetFloat("BestTime5"));
            RawTimes.Sort();
            RawTimes.Reverse();
            // Only keep the 5 best times.
            RawTimes = RawTimes.GetRange(0, 5);

            FormattedTimes = new List<string>();
            RepopulateFormattedTimes();

            deaths = PlayerPrefs.GetInt("TotalDodoDeaths");
            melonsMunched = PlayerPrefs.GetInt("TotalMelonsMunched");
            sabersSlain = PlayerPrefs.GetInt("TotalSabersSlain");
            trunksTraversed = PlayerPrefs.GetInt("TotalTrunksTraversed");
            firesFoiled = PlayerPrefs.GetInt("TotalFiresFoiled");
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
            if (_timerRunning) {
                _timerRunning = false;
                LatestTime = FormatTime(time);
                RawTimes.Add(time);
                // Sort by largest-first.
                RawTimes.Sort();
                RawTimes.Reverse();
                // Only keep the 5 best times.
                RawTimes = RawTimes.GetRange(0, 5);
                SaveBestTimes();
                RepopulateFormattedTimes();

                deaths++;

                PlayerPrefs.SetInt("TotalDodoDeaths", deaths);
                PlayerPrefs.SetInt("TotalMelonsMunched", melonsMunched);
                PlayerPrefs.SetInt("TotalSabersSlain", sabersSlain);
                PlayerPrefs.SetInt("TotalTrunksTraversed", trunksTraversed);
                PlayerPrefs.SetInt("TotalFiresFoiled", firesFoiled);
                PlayerPrefs.Save();
            }
        }

        private void SaveBestTimes()
        {
            for (int i = 0; i < 5; i++) {
                PlayerPrefs.SetFloat("BestTime"+(i+1), RawTimes[i]);
            }
        }

        private void RepopulateFormattedTimes()
        {
            // Only keep the 5 best times.
            RawTimes = RawTimes.GetRange(0, 5);
            // Repopulate formatted times.
            FormattedTimes.Clear();
            for (int i = 0; i < 5; i++) {
                FormattedTimes.Add(FormatTime(RawTimes[i]));
            }            
        }

        public string FormatTime(float t)
        {
            int minutes = Mathf.FloorToInt(t / 60F);
            int seconds = Mathf.FloorToInt(t % 60F);
            int milliseconds = Mathf.FloorToInt((t * 100F) % 100F);
            return minutes.ToString ("00") + ":" + seconds.ToString ("00") + ":" + milliseconds.ToString("00");
        }

        public void IncrementMelonsMunched()
        {
            melonsMunched += 1;
        }

        public void IncrementSabersSlain()
        {
            sabersSlain += 1;
        }

        public void IncrementTrunksTraversed()
        {
            trunksTraversed += 1;
        }

        public void IncrementFiresFoiled()
        {
            firesFoiled++;
        }
    }
}
