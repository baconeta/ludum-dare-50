namespace Props
{
    public class DodoBait : PlayerDraggable
    {
        private StatsManager _statsManager;

        private void Start()
        {
            _statsManager = FindObjectOfType<StatsManager>();
            CanDodoInteract = true;
        }

    }
}
