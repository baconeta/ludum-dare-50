using UnityEngine;

namespace Props
{
    public class PlayerDraggable : PlayerInteractable
    {
        // TODO Move onMouseDown & onMouseUp to PlayerInteractable.cs.
        protected virtual void OnMouseDown()
        {
            if (CanPlayerInteract && !IsDodoInteracting)
            {
                IsPlayerInteracting = true;
            }
        }

        protected virtual void OnMouseUp()
        {
            if (IsPlayerInteracting)
            {
                IsPlayerInteracting = false;
            }
        }

        // Update is called once per frame
        private void Update()
        {
            // If the player is clicking on the object.
            if (IsPlayerInteracting)
            {
                // when you click and hold down, it follows the mouse cursor, and then drops on release.
                Vector3 mousePos = Input.mousePosition;
                Vector3 loc = transform.position;
                loc.x = mousePos.x;
                loc.y = mousePos.y;
                transform.position = loc;
                Debug.Log(mousePos.x);
                Debug.Log(mousePos.y);
            }
        }
    }
}