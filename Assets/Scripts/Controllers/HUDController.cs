using UnityEngine;

namespace Controllers
{
    public class HUDController : MonoBehaviour
    {
        [SerializeField] private EndGameScreenController endGameScreenController;
        private GameController gameController;

        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
        }

        public void GameStart()
        {
            gameController = FindObjectOfType<GameController>();
            if (gameController != null)
            {
                transform.SetParent(gameController.transform);
            }
        }

        public void RestartGame()
        {
            // First show a fact, then reset the game from there
            ShowDodoFact();
        }

        private void ShowDodoFact()
        {
            // Here we fade the screen out to the background image and plonk a random dodo fact on the screen
            // After some time, we fade it out and call the functions below
            Invoke(nameof(ResetScene), 8f);
            
        }

        private void ResetScene()
        {
            if (gameController != null)
            {
                gameController.ResetGame();
            }
        }

        public void onGameEnd()
        {
            if (endGameScreenController != null)
            {
                endGameScreenController.enabled = true;
                endGameScreenController.onGameEnd();
            }
        }
    }
}