using System;
using UnityEngine;

namespace Props
{
    public class HeavyLift : PlayerDraggable
    {
        [SerializeField] private Vector3 liftedDistanceVector;

        protected override void Start()
        {
            base.Start();
        }

        protected override void OnMouseDown()
        {
            EnableCollisions(false);
            transform.position += liftedDistanceVector;
        }

        protected override void OnMouseUp()
        {
            EnableCollisions(true);
            transform.position -= liftedDistanceVector;
        }
    }
}