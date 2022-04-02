using UnityEngine;
using UnityEngine.PlayerLoop;
using System;
using System.Transactions;
using UnityEditor;

public class DodoBait : PlayerInteractable
{
    
    protected bool canPlayerInteract = true;
    protected bool isPlayerInteracting;
    protected bool canDodoInteract = true;
    protected bool isDodoInteracting;

    private void OnMouseDown()
    {
        if (canPlayerInteract && !isDodoInteracting)
        {
            isPlayerInteracting = true;
        }
    }

    private void OnMouseUp()
    {
        if (isPlayerInteracting)
        {
            isPlayerInteracting = false;
        }
    }


    // Update is called once per frame
    private void Update()
    {
        // If the player is clicking on the object.
        if (isPlayerInteracting)
        {
            // when you click and hold down, it follows the mouse cursor, and then drops on release.
            Vector3 mousePos = Input.mousePosition;
            Vector3 loc = transform.position;
            loc.x = mousePos.x;
            loc.y = mousePos.y;
            transform.position = loc;
            Debug.Log(mousePos.x);
            Debug.Log(mousePos.y);
        }
    }
}
