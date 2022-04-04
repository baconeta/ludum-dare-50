using UnityEngine;
using Unity.Services.Analytics;
using Unity.Services.Core;

namespace Controllers
{
    public class HUDController : MonoBehaviour
    {
        [SerializeField] private EndGameScreenController endGameScreenController;
        private GameController gameController;

        public async void Start()
        {
            await UnityServices.InitializeAsync();
        }

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