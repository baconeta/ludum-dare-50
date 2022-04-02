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
    private Vector3 m_CliffDirection = new Vector3(-.1f, -.5f);
    private Vector3 m_MountainDirection = new Vector3(.1f, .5f);
  


    // Start is called before the first frame update
    void Start()
    {
        dodoOrigin = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        int randNum = 2;
        if (Time.time % behaviourChangeSpeed + 0.1 > behaviourChangeSpeed)
        {
            randNum = Random.Range(2, 4);
        }
        if (randNum is 1)
        {
            //MovePingPong();
        }
        if (randNum is 2)
        {
            MoveTowardCliff();
        }

        if (randNum is 3)
        {
            MoveTowardMountain();
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
        transform.position += m_CliffDirection / dodoSpeed;
    }
    
    void MoveTowardMountain()
    {
        Debug.Log("Move Mountain");
        transform.position += m_MountainDirection / dodoSpeed;
    }

    void MovePingPong()
    {
        transform.position = Vector3.Lerp(cliffBounds.position, mountainBounds.position,
            Mathf.PingPong(Time.time / dodoSpeed, 1));
    }


}
