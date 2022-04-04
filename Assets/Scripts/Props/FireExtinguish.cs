using UnityEngine;

//TODO: Change to PlayerScrubbable
public class FireExtinguish : PlayerScrubbable
{
    private Renderer _renderer;

    [SerializeField] protected float valueToExtinguish;
    [SerializeField] protected Sprite extinguishedSprite;
    [SerializeField] private AudioSource burningSound;
    [SerializeField] private AudioSource extinguishSound;

    protected override void Start()
    {
        base.Start();
        scrubAmountRequired = valueToExtinguish;
        scrubbedSprite = extinguishedSprite;
        _renderer = GetComponent<Renderer>();
    }

    private void OnBecameVisible()
    {
        burningSound.Play();
    }

    private void OnBecameInvisible()
    {
        burningSound.Stop();
    }

    protected override void Update()
    {
        base.Update();
        if (!scrubbed && _renderer.isVisible)
        {
            float distance = Vector2.Distance(_dodo.transform.position, transform.position);
            float volume = ((40-(distance*2))/100);
            burningSound.volume = volume;
        }
    }

    protected override void HandleScrub()
    {
        base.HandleScrub();
        burningSound.Stop();
        extinguishSound.Play();
        _statsController.IncrementFiresFoiled();
    }

}
