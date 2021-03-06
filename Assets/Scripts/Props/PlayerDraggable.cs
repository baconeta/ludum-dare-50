using UnityEngine;

namespace Props
{
    public class PlayerDraggable : PlayerInteractable
    {
        protected Collider2D _collider;
        protected Vector2 mouseOffset;
        private bool _isFallingFromMountain;

        protected override void Start()
        {
            base.Start();
            _collider = GetComponent<Collider2D>();
        }

        // TODO Move onMouseDown & onMouseUp to PlayerInteractable.cs.
        protected virtual void OnMouseDown()
        {
            if (CanPlayerInteract && !IsDodoInteracting)
            {
                IsPlayerInteracting = true;
                EnableCollisions(false);
                
                Vector3 mousePos = ConvertMouseToWorldPosition(Input.mousePosition);
                Vector3 loc = transform.position;
                mouseOffset = (Vector2) loc - (Vector2) mousePos;
            }
        }

        protected virtual void OnMouseUp()
        {
            if (IsPlayerInteracting)
            {
                IsPlayerInteracting = false;
                EnableCollisions(true);
            }
            checkFallOffCliff();
            checkFallFromMountain();
        }

        protected virtual void OnObjectLanded()
        {
        }

        private void checkFallFromMountain()
        {
            float _mountainEdgeCrossProduct = getCrossProduct("mountain");
            if (_mountainEdgeCrossProduct > 0)
            {
                gameObject.AddComponent<Rigidbody2D>();
                _isFallingFromMountain = true;
            }
        }

        private void checkFallOffCliff()
        {
            
            float edgeObjectCross = getCrossProduct("cliff");
            if (edgeObjectCross < 0)
            {
                gameObject.AddComponent<Rigidbody2D>();
            }

        }

        private float getCrossProduct(string side)
        {
            Vector3 edgeVector = new Vector3(1f, -0.5f, 0f);
            Vector3 edgePoint;
            Vector3 directionOfObject;
            switch(side)
            {
                case "cliff":
                    edgePoint = _worldController.transform.GetChild(4).position;
                    directionOfObject = transform.position - edgePoint;
                    return Vector3.Cross(edgeVector.normalized, directionOfObject.normalized).z;
                case "mountain":
                    edgePoint = _worldController.transform.GetChild(5).position;
                    directionOfObject = transform.position - edgePoint;
                    return Vector3.Cross(edgeVector.normalized, directionOfObject.normalized).z;
            }
            return 0;

        }

        // Update is called once per frame
        protected override void Update()
        {
            base.Update();

            if (transform.position.x < 0)
                Destroy(gameObject);

            // If the player is clicking on the object.
            if (IsPlayerInteracting)
            {
                // when you click and hold down, it follows the mouse cursor, and then drops on release.
                MoveObjectWithMouse();
            }

            if (gameObject.GetComponent<Rigidbody2D>())
            {
                if (_isFallingFromMountain && getCrossProduct("mountain") < 0)
                {
                    _isFallingFromMountain = false;
                    Destroy(gameObject.GetComponent<Rigidbody2D>());
                    OnObjectLanded();
                }
            }
            
        }

        private void MoveObjectWithMouse()
        {
            Vector3 mousePos = ConvertMouseToWorldPosition(Input.mousePosition);
            Vector3 loc = transform.position;
            loc.x = mousePos.x;
            loc.y = mousePos.y;
            transform.position = loc + (Vector3)mouseOffset;
        }

        protected void EnableCollisions(bool enabled)
        {
            _collider.enabled = enabled;
        }
    }
}