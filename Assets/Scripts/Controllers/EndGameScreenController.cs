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
        [SerializeField] private Text TotalFoodsEaten;
        [SerializeField] private Text TotalObjectsSmashed;
        [SerializeField] private Text TotalBridgesCrossed;
        [SerializeField] private Text TotalBouldersBumped;

        private StatsController _statsController;

        private void Start()
        {
            _statsController = FindObjectOfType<StatsController>();
            hideEndGameScreen();
        }

        public void onGameReset()
        {
            hideEndGameScreen();
        }

        public void onGameEnd()
        {
            showEndGameScreen();
        }

        public void showEndGameScreen()
        {
            Highscore1.text = _statsController.scores[0].ToString();
            Highscore2.text = _statsController.scores[1].ToString();
            Highscore3.text = _statsController.scores[2].ToString();
            Highscore4.text = _statsController.scores[3].ToString();
            Highscore5.text = _statsController.scores[4].ToString();
            TotalDodoDeaths.text = "Dodo Deaths: " + _statsController.deaths.ToString();
            TotalFoodsEaten.text = "Snacks Eaten: " + _statsController.eatenFoods.ToString();
            TotalObjectsSmashed.text = "Things Smashed: " + _statsController.objectsSmashed.ToString();
            TotalBridgesCrossed.text = "Bridges Crossed: " + _statsController.bridgesCrossed.ToString();
            TotalBouldersBumped.text = "Boulders Bumped: " + _statsController.bouldersBumped.ToString();
            HighscoreBanner.enabled = true;
            Highscore1.enabled = true;
            Highscore2.enabled = true;
            Highscore3.enabled = true;
            Highscore4.enabled = true;
            Highscore5.enabled = true;
            TotalBanner.enabled = true;
            TotalDodoDeaths.enabled = true;
            TotalFoodsEaten.enabled = true;
            TotalObjectsSmashed.enabled = true;
            TotalBridgesCrossed.enabled = true;
            TotalBouldersBumped.enabled = true;
                
        }

        public void hideEndGameScreen()
        {
            HighscoreBanner.enabled = false;
            Highscore1.enabled = false;
            Highscore2.enabled = false;
            Highscore3.enabled = false;
            Highscore4.enabled = false;
            Highscore5.enabled = false;
            TotalBanner.enabled = false;
            TotalDodoDeaths.enabled = false;
            TotalFoodsEaten.enabled = false;
            TotalObjectsSmashed.enabled = false;
            TotalBridgesCrossed.enabled = false;
            TotalBouldersBumped.enabled = false;
        }
    }
}