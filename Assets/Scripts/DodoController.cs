using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class DodoController : MonoBehaviour
{
    [SerializeField] private float dodoSpeed;
    private float timer;
    private Vector3 dodoOrigin;
    [SerializeField] private Transform cliffBounds;
    [SerializeField] private Transform mountainBounds;
    [SerializeField] private float behaviourChangeSpeed;
    private Vector3 m_ForwardDirection = new Vector3(1f, -.5f);
    private Vector3 m_CliffDirection = new Vector3(-.1f, -.05f);
    private Vector3 m_MountainDirection = new Vector3(.1f, .05f);
    private int currentBehaviour;
  


    // Start is called before the first frame update
    void Start()
    {
        dodoOrigin = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        MovePingPong();
        if (Time.time % behaviourChangeSpeed + 0.05 > behaviourChangeSpeed)
        {
            currentBehaviour = Random.Range(1, 4);
        }
        if (currentBehaviour is 1)
        {
            
        }
        if (currentBehaviour is 2)
        {
            //MoveTowardCliff();
        }

        if (currentBehaviour is 3)
        {
            //MoveTowardMountain();
        }

        


        transform.position = new Vector3(Mathf.Clamp(transform.position.x, 
            cliffBounds.position.x, 
            mountainBounds.position.x),
            Mathf.Clamp(transform.position.y,
                cliffBounds.position.y,
                mountainBounds.position.y));

    }

    void MoveTowardCliff()
    {
        Debug.Log("Move Cliff");
        transform.position += m_CliffDirection * dodoSpeed;
    }
    
    void MoveTowardMountain()
    {
        Debug.Log("Move Mountain");
        transform.position += m_MountainDirection * dodoSpeed;
    }

    void MovePingPong()
    {
        transform.position = Vector3.Lerp(cliffBounds.position, mountainBounds.position,
            Mathf.PingPong(Time.time / 100 * dodoSpeed, 1));
    }


}
