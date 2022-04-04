using UnityEngine;

namespace Props
{
    public class PlayerDraggable : PlayerInteractable
    {
        private Collider2D _collider2D;
        protected Vector2 mouseOffset;

        protected override void Start()
        {
            base.Start();
            _collider2D = GetComponent<Collider2D>();
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
        }

        // Update is called once per frame
        protected override void Update()
        {
            // If the player is clicking on the object.
            if (IsPlayerInteracting)
            {
                // when you click and hold down, it follows the mouse cursor, and then drops on release.
                Vector3 mousePos = ConvertMouseToWorldPosition(Input.mousePosition);
                Vector3 loc = transform.position;
                loc.x = mousePos.x;
                loc.y = mousePos.y;
                transform.position = loc + (Vector3) mouseOffset;
            }
        }

        protected void EnableCollisions(bool enabled)
        {
            _collider2D.enabled = enabled;
        }
    }
}