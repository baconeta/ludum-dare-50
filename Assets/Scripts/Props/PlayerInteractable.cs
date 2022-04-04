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
        private float _rangeToCheckCrossProduct = 4;
        private Vector3 _dodoForwardVector = new Vector3(1f, -.5f);
        
        protected StatsController _statsController;
        protected WorldController _worldController;
        protected SpriteRenderer _spriteRenderer;
        protected Dodo _dodo;
        protected GameObject _dodoObject => _dodo.gameObject;

        protected virtual void Start()
        {
            _statsController = FindObjectOfType<StatsController>();
            _worldController = FindObjectOfType<WorldController>();
            _spriteRenderer = GetComponent<SpriteRenderer>();
            if (_spriteRenderer == default)
            {
                Debug.Log("No sprite rendered on " + name + " object.");
            }
            _dodo = FindObjectOfType<Dodo>();
        }

        protected virtual void Update()
        {
            if (!IsPlayerInteracting)
            {
                UpdateObjectSortOrder();
            }
        }

        protected virtual void UpdateObjectSortOrder()
        {
            var distanceToDodo = Vector3.Distance(_dodoObject.transform.position, transform.position);
            if (distanceToDodo < _rangeToCheckCrossProduct)
            {
                var directionFromDodo = _dodoObject.transform.position - transform.position;
                var dodoCrossProduct = Vector3.Cross(directionFromDodo.normalized, _dodoForwardVector.normalized).z;
                if (dodoCrossProduct > 0)
                {
                    _spriteRenderer.sortingOrder = 2;
                }
                else
                {
                    _spriteRenderer.sortingOrder = 4;
                }
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