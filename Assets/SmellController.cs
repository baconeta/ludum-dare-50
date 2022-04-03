using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Props;

public class SmellController : MonoBehaviour
{

    private GameObject smellObject;
    
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag is "Smell")
        {
            smellObject = col.gameObject;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag is "Smell" && other.gameObject == smellObject)
        {
            smellObject.GetComponent<PlayerInteractable>().DodoInteract(false);
            smellObject = null;
        }
    }

    public GameObject getSmelledObject()
    {
        
        return smellObject;
    }
}
