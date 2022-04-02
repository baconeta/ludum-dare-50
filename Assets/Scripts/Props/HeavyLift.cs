using System;
using UnityEngine;

namespace Props
{
    public class HeavyLift : PlayerDraggable
    {
        private Vector3 _origin;
        [SerializeField] private Vector3 liftedDistanceVector;
        private BoxCollider2D _boxCollider2D;
        private bool _isLifted;
        private float _timeLifted;

        protected override void Start()
        {
            base.Start();
            _boxCollider2D = GetComponent<BoxCollider2D>();
            _timeLifted = 0f;
            _isLifted = false;
        }

        protected override void OnMouseDown()
        {
            _origin = transform.position;
            transform.position = _origin + liftedDistanceVector;

            _isLifted = true;
            _timeLifted = 0f;
            _boxCollider2D.enabled = false;
        }

        protected override void OnMouseUp()
        {
            var offset = _worldController.getMoveDirection() * _timeLifted * _worldController.getWorldSpeed();
            transform.position = _origin - offset;
            _isLifted = false;
            _boxCollider2D.enabled = true;
        }

        protected override void Update()
        {
            if (_isLifted)
            {
                _timeLifted += Time.deltaTime;
            }

            //Don't do base behaviour of dragging object with mouse
        }
    }
}