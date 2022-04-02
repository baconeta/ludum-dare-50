namespace Props
{
    public class Bridge : PlayerDraggable
    {
        private StatsManager _statsManager;

        private void Start()
        {
            _statsManager = FindObjectOfType<StatsManager>();
        }
        
    }
}
