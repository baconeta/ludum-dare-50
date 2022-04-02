namespace Props
{
    public class PlayerClickable : PlayerInteractable
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
    }
}