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
        
        protected StatsManager _statsManager;
        protected WorldController _worldController;
        
        protected virtual void Start()
        {
            _statsManager = FindObjectOfType<StatsManager>();
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

        protected void DodoInteract()
        {
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
    }
}