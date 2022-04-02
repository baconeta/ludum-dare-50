namespace Props
{
    public class PlayerClickable : PlayerInteractable
    {
        // TODO Move onMouseDown & onMouseUp to PlayerInteractable.cs.
        protected virtual void OnMouseDown()
        {
            if (canPlayerInteract && !isDodoInteracting)
            {
                isPlayerInteracting = true;
            }
        }

        protected virtual void OnMouseUp()
        {
            if (isPlayerInteracting)
            {
                isPlayerInteracting = false;
            }
        }
    }
}