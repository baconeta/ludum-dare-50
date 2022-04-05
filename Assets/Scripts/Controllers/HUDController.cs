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