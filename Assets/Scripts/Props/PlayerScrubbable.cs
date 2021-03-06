using Props;
using UnityEngine;
using System.Collections;

public class PlayerScrubbable : PlayerClickable
{
    protected Collider2D _collider;
    protected Animator _animator;

    protected float scrubAmountRequired;
    private float currentScrubAmount = 0;
    private Vector3 mouseLocation;
    protected Sprite scrubbedSprite;
    protected bool scrubbed = false;
    [SerializeField] private float minimumShakeThreshold;
    private bool shaking = false;

    protected override void Start()
    {
        base.Start();
        _collider = GetComponent<Collider2D>();
        _animator = GetComponent<Animator>();
    }

    protected override void OnMouseDown()
    {
        base.OnMouseDown();
    }

    protected void OnMouseDrag()
    {
        //Get distance of mouse movement between frames, then add that amount to currentScrubAmount.
        var previousMouseLocation = mouseLocation;
        mouseLocation = ConvertMouseToWorldPosition(Input.mousePosition);
        float dragDistance = Vector3.Distance(mouseLocation, previousMouseLocation);
        currentScrubAmount += dragDistance;
        if (currentScrubAmount > scrubAmountRequired && !scrubbed)
        {
            HandleScrub();
        }
        if (dragDistance > minimumShakeThreshold && !scrubbed)
        {
            StartCoroutine(DoShake());
        }
    }

    protected virtual void HandleScrub()
    {
        scrubbed = true;
        _spriteRenderer.sprite = scrubbedSprite;
        _collider.enabled = false;
        _animator.enabled = false;
    }

    protected IEnumerator DoShake()
    {
        if (!shaking && !scrubbed)
        {
            shaking = true;
            transform.Rotate(0, 0, 5);
            yield return new WaitForSeconds(0.1F);
            transform.Rotate(0, 0, -10);
            yield return new WaitForSeconds(0.1F);
            transform.Rotate(0, 0, 5);
            shaking = false;
        }
    }

}
