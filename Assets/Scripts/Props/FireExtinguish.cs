using UnityEngine;

//TODO: Change to PlayerScrubbable
public class FireExtinguish : PlayerScrubbable
{
    private Dodo _dodo;
    private Renderer _renderer;
    private AudioSource _audioSource;

    [SerializeField] protected float valueToExtinguish;
    [SerializeField] protected Sprite extinguishedSprite;

    protected override void Start()
    {
        base.Start();
        scrubAmountRequired = valueToExtinguish;
        scrubbedSprite = extinguishedSprite;
        _dodo = FindObjectOfType<Dodo>();
    }

    private void onBecameVisible()
    {
        _audioSource.Play();
    }

    private void onBecameInvisible()
    {
        _audioSource.Stop();
    }

    protected override void Update()
    {
        base.Update();
        if (!scrubbed)
        {
            float distance = Vector2.Distance(_dodo.transform.position, transform.position);
            Debug.Log("Distance: " + distance);
            //_audioSource.volume
        }
    }
    // Scale volume with distance to Dodo.
    // Disable sound when scrubbed.
    
    protected override void HandleScrub()
    {
        base.HandleScrub();
        _audioSource.Stop();
    }
    
}
