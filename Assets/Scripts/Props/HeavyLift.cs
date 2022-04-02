using System;
using UnityEngine;

namespace Props
{
    public class HeavyLift : PlayerDraggable
    {
        [SerializeField] private Vector3 liftedDistanceVector;
        private BoxCollider2D _boxCollider2D;

        protected override void Start()
        {
            base.Start();
            _boxCollider2D = GetComponent<BoxCollider2D>();
        }

        protected override void OnMouseDown()
        {
            transform.position += liftedDistanceVector;
            _boxCollider2D.enabled = false;
        }

        protected override void OnMouseUp()
        {
            transform.position -= liftedDistanceVector;
            _boxCollider2D.enabled = true;
        }

        protected override void Update()
        {
            //Don't do base behaviour of dragging object with mouse
        }
    }
}