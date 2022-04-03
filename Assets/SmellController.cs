using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmellController : MonoBehaviour
{
    private Collider2D collider;

    private GameObject smellObject;
    // Start is called before the first frame update
    void Start()
    {
        collider = GetComponent<CircleCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

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
            smellObject = null;
        }
    }

    public GameObject getSmelledObject()
    {
        return smellObject;
    }
}
