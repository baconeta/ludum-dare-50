using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MountainBoundController : MonoBehaviour
{
    private Vector3 inwardsVector = new Vector3(-.5f, -.25f);
    private Vector3 outwardsVector = new Vector3(.5f, .25f);

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Collider2D hit = Physics2D.OverlapPoint(transform.position);
        if (!hit)
        {
           transform.position += inwardsVector;
        }
        else
        {
            if (Physics2D.OverlapPoint(transform.position + outwardsVector))
            {
                transform.position += outwardsVector;
            }
        }

        if (transform.position.x < 9)
        {
            transform.position = new Vector3(9, -6);
        }
    }
}
