namespace Props
{
    public class DodoBait : PlayerDraggable
    {
        protected override void Start()
        {
            base.Start();
            // Left just for LD speed.
            CanDodoInteract = true;
        }

        public void getEaten()
        {
            
            Destroy(this.gameObject);
        }

    }
}
