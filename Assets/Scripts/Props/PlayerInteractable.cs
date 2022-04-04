using Controllers;
using UnityEngine;

namespace Props
{
    public abstract class PlayerInteractable : MonoBehaviour
    {
        private Camera _gameCamera;

        protected bool CanPlayerInteract = true;
        protected bool CanDodoInteract = false;
        protected bool IsPlayerInteracting = false;
        protected bool IsDodoInteracting = false;
        
        protected StatsController _statsController;
        protected WorldController _worldController;
        
        protected virtual void Start()
        {
            _statsController = FindObjectOfType<StatsController>();
            _worldController = FindObjectOfType<WorldController>();
        }

        protected virtual void Update()
        {
            if (IsPlayerInteracting)
            {
                // Move the prop each frame.
            }
        }

        protected void PlayerInteract()
        {
        }

        public void DodoInteract(bool isInteracting)
        {
            IsDodoInteracting = isInteracting;
        }

        /// <summary>
        /// Returns the world transform position relative to the mouse position on screen
        /// </summary>
        /// <param name="mousePosition"></param>
        /// <returns> A Vector containing the world space position </returns>
        protected Vector3 ConvertMouseToWorldPosition(Vector3 mousePosition)
        {
            if (!_gameCamera)
            {
                _gameCamera = FindObjectOfType<Camera>();
            }

            return _gameCamera.ScreenToWorldPoint(mousePosition);
        }

        private void onGameReset()
        {
            transform.position = new Vector3(1000,1000,0);
            CanPlayerInteract = false;
            CanDodoInteract = false;
        }
    }
}