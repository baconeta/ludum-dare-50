using System;
using UnityEngine;

namespace Props
{
    public class HeavyLift : PlayerDraggable
    {
        private Vector3 _origin;
        [SerializeField] private Vector3 liftedDistanceVector;
        private BoxCollider2D _boxCollider2D;

        private void Start()
        {
            _origin = transform.position;
            _boxCollider2D = GetComponent<BoxCollider2D>();
        }

        protected override void OnMouseDown()
        {
            Vector3 newLocation = _origin;
            newLocation += liftedDistanceVector;
            transform.position = newLocation;

            _boxCollider2D.enabled = false;
        }

        protected override void OnMouseUp()
        {
            transform.position = _origin;
            _boxCollider2D.enabled = true;
        }
    }
}