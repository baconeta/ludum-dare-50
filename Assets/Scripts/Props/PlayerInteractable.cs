using UnityEngine;

namespace Props
{
    public abstract class PlayerInteractable : MonoBehaviour
    {
        protected bool canPlayerInteract = true;
        protected bool isPlayerInteracting = false;
        protected bool canDodoInteract = false;
        protected bool isDodoInteracting = false;
        private Camera _gameCamera;

        protected void playerInteract()
        {
        }

        protected void dodoInteract()
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