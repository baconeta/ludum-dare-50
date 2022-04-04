using UnityEngine;

//TODO: Change to PlayerScrubbable
public class FireExtinguish : PlayerScrubbable
{
    private Renderer _renderer;
    private AudioSource _audioSource;

    [SerializeField] protected float valueToExtinguish;
    [SerializeField] protected Sprite extinguishedSprite;


    protected override void Start()
    {
        base.Start();
        scrubAmountRequired = valueToExtinguish;
        scrubbedSprite = extinguishedSprite;
        _renderer = GetComponent<Renderer>();
        _audioSource = GetComponent<AudioSource>();
    }

    private void OnBecameVisible()
    {
        _audioSource.Play();
    }

    private void OnBecameInvisible()
    {
        _audioSource.Stop();
    }

    protected override void Update()
    {
        base.Update();
        if (!scrubbed && _renderer.isVisible)
        {
            float distance = Vector2.Distance(_dodo.transform.position, transform.position);
            float volume = ((40-(distance*2))/100);
            _audioSource.volume = volume;
        }
    }

    protected override void HandleScrub()
    {
        base.HandleScrub();
        _audioSource.Stop();
        _statsController.IncrementFiresFoiled();
    }

}
