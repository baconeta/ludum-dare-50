using UnityEngine;

namespace Props
{
    public class HeavyLift : PlayerDraggable
    {
        private Vector3 origin;

        protected override void OnMouseDown()
        {
            base.OnMouseDown();
            origin = transform.position;
        }
        protected override void OnMouseUp()
        {
            base.OnMouseUp();
            transform.position = origin;
        }
    }
}