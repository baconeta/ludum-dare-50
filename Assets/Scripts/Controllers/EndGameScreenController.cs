using System.Collections;
using Controllers;
using UnityEngine;
using UnityEngine.UI;

namespace Controllers
{
    public class EndGameScreenController : MonoBehaviour
    {
        [SerializeField] private Text HighscoreBanner;
        [SerializeField] private Text Highscore1;
        [SerializeField] private Text Highscore2;
        [SerializeField] private Text Highscore3;
        [SerializeField] private Text Highscore4;
        [SerializeField] private Text Highscore5;
        [SerializeField] private Text TotalBanner;
        [SerializeField] private Text TotalDodoDeaths;
        [SerializeField] private Text TotalMelonsMunched;
        [SerializeField] private Text TotalFiresFoiled;
        [SerializeField] private Text TotalTrunksTraversed;
        [SerializeField] private Text TotalSabersSlain;
        [SerializeField] private Color NormalTextColour;
        [SerializeField] private Color HighlightedTextColour;

        private StatsController _statsController;
        private Canvas _canvas;
        [SerializeField] private DodoFacts ddf;

        private void Start()
        {
            _canvas = GetComponent<Canvas>();
            hideEndGameScreen();
        }

        public void onGameReset()
        {
            hideEndGameScreen();
        }

        public void onGameEnd()
        {
            _statsController = FindObjectOfType<StatsController>();
            ddf.GetComponent<Image>().enabled = true;
            ddf.ChooseImage();
            StartCoroutine(nameof(WaitToShowEndScreen));
        }

        private IEnumerator WaitToShowEndScreen()
        {
            yield return new WaitForSeconds(6f);
            ddf.GetComponent<Image>().enabled = false;
            showEndGameScreen();
        }

        public void showEndGameScreen()
        {
            Highscore1.text = _statsController.FormattedTimes[0];
            Highscore2.text = _statsController.FormattedTimes[1];
            Highscore3.text = _statsController.FormattedTimes[2];
            Highscore4.text = _statsController.FormattedTimes[3];
            Highscore5.text = _statsController.FormattedTimes[4];

            Highscore1.color = Highscore1.text == _statsController.LatestTime
                ? HighlightedTextColour
                : NormalTextColour;
            Highscore2.color = Highscore2.text == _statsController.LatestTime
                ? HighlightedTextColour
                : NormalTextColour;
            Highscore3.color = Highscore3.text == _statsController.LatestTime
                ? HighlightedTextColour
                : NormalTextColour;
            Highscore4.color = Highscore4.text == _statsController.LatestTime
                ? HighlightedTextColour
                : NormalTextColour;
            Highscore5.color = Highscore5.text == _statsController.LatestTime
                ? HighlightedTextColour
                : NormalTextColour;

            TotalDodoDeaths.text = "Dodo Deaths: " + _statsController.deaths;
            TotalMelonsMunched.text = "Melons Munched: " + _statsController.melonsMunched;
            TotalSabersSlain.text = "Sabers Slain: " + _statsController.sabersSlain;
            TotalTrunksTraversed.text = "Trunks Traversed: " + _statsController.trunksTraversed;
            TotalFiresFoiled.text = "Fires Foiled: " + _statsController.firesFoiled;

            _canvas.enabled = true;
        }

        public void hideEndGameScreen()
        {
            _canvas.enabled = false;
        }
    }
}