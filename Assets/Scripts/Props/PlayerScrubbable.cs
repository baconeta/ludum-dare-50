using System.Collections;
using System.Collections.Generic;
using Props;
using UnityEngine;

public class PlayerScrubbable : PlayerClickable
{
    protected SpriteRenderer _spriteRenderer;
    protected Collider2D _collider;
    protected Animator _animator;

    protected float scrubAmountRequired;
    private float currentScrubAmount = 0;
    private Vector3 previousMouseLocation;
    private Vector3 mouseLocation;
    protected Sprite scrubbedSprite;

    protected override void Start()
    {
        base.Start();
        _spriteRenderer = FindObjectOfType<SpriteRenderer>();
        _collider = FindObjectOfType<Collider2D>();
        _animator = FindObjectOfType<Animator>();
    }

    protected override void OnMouseDown()
    {
        base.OnMouseDown();
    }

    protected void OnMouseDrag()
    {
        //Get distance of mouse movement between frames, then add that amount to currentScrubAmount.
        previousMouseLocation = mouseLocation;
        mouseLocation = ConvertMouseToWorldPosition(Input.mousePosition);
        currentScrubAmount += Vector3.Distance(mouseLocation, previousMouseLocation);
        Debug.Log(currentScrubAmount);
        if (currentScrubAmount > scrubAmountRequired)
        {
            _spriteRenderer.sprite = scrubbedSprite;
            _collider.enabled = false;
            _animator.enabled = false;
        }
    }
}