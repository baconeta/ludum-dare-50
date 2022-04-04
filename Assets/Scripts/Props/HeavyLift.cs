using System;
using UnityEngine;

namespace Props
{
    public class HeavyLift : PlayerDraggable
    {
        [SerializeField] private Vector3 liftedDistanceVector;

        [SerializeField] private bool useLiftAnim;
        [SerializeField] private BoulderAnimation _liftAnimation;
        private bool isLifted;

        protected override void Start()
        {
            base.Start();
        }

        protected override void OnMouseDown()
        {
            isLifted = true;
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
            isLifted = false;
            if (useLiftAnim)
            {
                _liftAnimation.DropBoulderAnims();
            }
        }

        public bool isRockLifted()
        {
            return isLifted;
        }
    }
}