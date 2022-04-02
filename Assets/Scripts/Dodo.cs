using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Dodo : MonoBehaviour
{
    [SerializeField] private float dodoSpeed;
    private Vector3 dodoOrigin;
    [Tooltip("How often the behaviour automatically changes")]
    private Vector3 m_ForwardDirection = new Vector3(1f, -.5f);
    private Vector3 m_CliffDirection = new Vector3(-.1f, -.05f);
    private Vector3 m_MountainDirection = new Vector3(.1f, .05f);
    [SerializeField] float sniffRange;
    
    //Movement and Behaviours
    [SerializeField] private float behaviourChangeSpeed = 5;
    private float _currentBehaviour = 1;
    private bool _hasMoved = false;
    
  


    // Start is called before the first frame update
    void Start()
    {
        dodoOrigin = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Move();

    }

    //Input desiredBehaviour to choose a behaviour
    void ChangeMovementBehaviour(int desiredBehaviour = 0)
    {
        float timer = 1;//Time.time % Random.Range(4, 7);
        if (desiredBehaviour != 0)
        {
            _currentBehaviour = desiredBehaviour;
        }
        else
        {
            if (timer < 0.1)
            {
                _currentBehaviour = Random.Range(1, 4);
            }
        }
        
        
    }

    void MoveForwards()
    {
        
    }

    void Move()
    {
        float timer = 1;//Time.time % behaviourChangeSpeed;

        if (timer < 0.1 && !_hasMoved)
        {
            ChangeMovementBehaviour();
            _hasMoved = true;
        }
        else if (timer > 0.1)
        {
            _hasMoved = false;
        }
        
        switch (_currentBehaviour)
        {
            case 1:
                //Debug.Log("Moving Forwards");
                MoveForwards();
                break;
            case 2:
                //Debug.Log("Moving Up");
                MoveSinWave();
                break;
            case 3:
                //Debug.Log("Moving Down");
                MoveSinWave(true);
                break;
        }
    }

    //Direction: 1 = left, 2 = right
    void MoveSinWave(bool moveDown = false)
    {
        float timer;
        float sinWave = 0;
        if (moveDown)
        {
            timer = 1;
            sinWave = Mathf.Sin(timer);
        }
        if (sinWave > 0.99)
        {
            MoveForwards();
        }
        transform.position = dodoOrigin + (new Vector3(sinWave / 2, sinWave  / 4, 0) * dodoSpeed);
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("DeathHazard"))
        {
            DamagePlayer(col.name);
        }
    }

    void DamagePlayer(string source)
    {
        Debug.Log($"You took damage from {source} and died");
    }

}
