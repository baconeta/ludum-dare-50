using System.Collections;
using System.Collections.Generic;
using Props;
using UnityEngine;

public class PlayerScrubbable : PlayerClickable
{
    protected float scrubAmountRequired;
    private float currentScrubAmount = 0;
    private Vector3 previousMouseLocation;
    private Vector3 mouseLocation;
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
            Destroy(this.gameObject);
        }
    }
    
}
