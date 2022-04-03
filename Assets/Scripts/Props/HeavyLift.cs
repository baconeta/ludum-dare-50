using System;
using UnityEngine;

namespace Props
{
    public class HeavyLift : PlayerDraggable
    {
        [SerializeField] private Vector3 liftedDistanceVector;

        [SerializeField] private bool useLiftAnim;
        [SerializeField] private BoulderAnimation _liftAnimation;

        protected override void Start()
        {
            base.Start();
        }

        protected override void OnMouseDown()
        {
            EnableCollisions(false);
            transform.position += liftedDistanceVector;
            if (useLiftAnim)
            {
                _liftAnimation.LiftBoulderAnims();
            }
        }

        protected override void OnMouseUp()
        {
            EnableCollisions(true);
            transform.position -= liftedDistanceVector;
            if (useLiftAnim)
            {
                _liftAnimation.DropBoulderAnims();
            }
        }
    }
}