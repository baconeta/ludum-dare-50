using System.Collections;
using System.Collections.Generic;
using Props;
using UnityEngine;

public class PlayerScrubbable : PlayerClickable
{
    protected SpriteRenderer spriteRenderer;
    protected Collider2D Collider;
    protected Animator animator;

    protected float scrubAmountRequired;
    private float currentScrubAmount = 0;
    private Vector3 previousMouseLocation;
    private Vector3 mouseLocation;
    protected Sprite scrubbedSprite;

    protected override void Start()
    {
        base.Start();
        spriteRenderer = GetComponent<SpriteRenderer>();
        Collider = GetComponent<Collider2D>();
        animator = GetComponent<Animator>();
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
        if (currentScrubAmount > scrubAmountRequired)
        {
            spriteRenderer.sprite = scrubbedSprite;
            Collider.enabled = false;
            animator.enabled = false;
        }
    }
}