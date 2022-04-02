using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class WorldController : MonoBehaviour
{
    [Tooltip("The speed at which the world moves.")]
    [SerializeField] public float worldSpeed;
    private Vector3 moveDirection = new Vector3(1f, .5f);
    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    
    public void setWorldSpeed(float speed = 1) 
    {
        worldSpeed = speed;
    }

    public float getWorldSpeed()
    {
        return worldSpeed;
    }

    public Vector3 getMoveDirection()
    {
        return moveDirection;
    }
}
