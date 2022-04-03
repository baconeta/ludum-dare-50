using Props;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Dodo : MonoBehaviour
{
    [SerializeField] private float dodoSpeed;
    [SerializeField] private float dodoAcceleration;
    private float _currentDodoAcceleration;
    private Vector3 deccelerationOffset = new Vector3(0.5f, 0.25f);

    private Vector3 dodoOrigin;
    [Tooltip("How often the behaviour automatically changes")]
    private Vector3 m_ForwardDirection = new Vector3(1f, -.5f);
    [SerializeField] float sniffRange;
    
    //Movement and Behaviours
    [SerializeField] private float behaviourChangeSpeed = 5;
    private int _currentBehaviour = 1;
    private bool _hasMoved = false;
    private float _behaviourTimer;

    private bool b_isTransitionMovement;
    [SerializeField]private float _wobbleWidth;
    [SerializeField] private float _wobbleFrequency;
 


    //Vector line that Dodo Moves on
    private Vector3 _sideVector3 = new Vector3(.005f, 0.0025f);
    
    //Boundaries
    [SerializeField] Transform _cliffBound;
    private Vector3 _cliffBoundPos;
    [SerializeField] Transform _mountainBound;
    private Vector3 _mountainBoundPos;
    private bool isOnBridge;

  


    // Start is called before the first frame update
    void Start()
    {
        _cliffBoundPos = _cliffBound.position;
        _mountainBoundPos = _mountainBound.position;
        dodoOrigin = transform.position;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!isOnBridge)
        {
            _behaviourTimer = Time.time % behaviourChangeSpeed;
            Move();
        }
    }

    //Input desiredBehaviour to choose a behaviour
    void ChangeMovementBehaviour(int desiredBehaviour = 0)
    {
        _currentDodoAcceleration = 0;
        //If a chosen behaviour has been given
        if (desiredBehaviour != 0)
        {
            _currentBehaviour = desiredBehaviour;
        }
        else //Get a new behaviour
        {
            //If current behaviour is Moving Forwards (1)
            if (_currentBehaviour != 1)
            {
                // Then it is transitional movement - Will MoveForwards for 1 second.
                _currentBehaviour = 1;
                behaviourChangeSpeed = 1;
                b_isTransitionMovement = true;
            }
            else 
            {
                _currentBehaviour = Random.Range(1, 4);
            }
        }
        
        
    }


    void Move()
    {
        _currentDodoAcceleration += dodoAcceleration / 100;
        _currentDodoAcceleration = Mathf.Clamp(_currentDodoAcceleration, 0, 1);
        if (_behaviourTimer < 0.1 && !_hasMoved)
        {
            ChangeMovementBehaviour();
            _hasMoved = true;
            
        }
        else if (_behaviourTimer > 0.1)
        {
            _hasMoved = false;
        }
        else if (_behaviourTimer > behaviourChangeSpeed - 0.1)
        {
            b_isTransitionMovement = false;
            behaviourChangeSpeed = 5;
        }
        
        switch (_currentBehaviour)
        {
            //Move Forwards
            case 1:
                MoveForwards();
                break;
            //Move Towards Cliff
            case 2:
                MoveTowardCliff();
                break;
            //Move Towards Mountain
            case 3:
                MoveTowardMountain();
                break;
        }
        //Current Acceleration %
        _currentDodoAcceleration = Mathf.Clamp(_currentDodoAcceleration, 0, 1);

    }
    void MoveForwards()
    {
        float sinWave = _wobbleWidth * Mathf.Sin(Time.time * _wobbleFrequency);
        transform.position += _sideVector3 * sinWave;

    }
    
    void MoveTowardCliff()
    { ;
        if (transform.position.x <= _cliffBoundPos.x + deccelerationOffset.x)
        {
            _currentDodoAcceleration -= dodoAcceleration / 100;
        }
        transform.position +=  -_sideVector3 * dodoSpeed * _currentDodoAcceleration;
        if (transform.position.x <= _cliffBoundPos.x)
        {
            ChangeMovementBehaviour(3);
        }
    }
    
    void MoveTowardMountain()
    { ;
        if (transform.position.x >= _mountainBoundPos.x - deccelerationOffset.x)
        {
            _currentDodoAcceleration -= dodoAcceleration / 100;
            
        }

        transform.position += _sideVector3 * dodoSpeed * _currentDodoAcceleration;
        if (transform.position.x >= _mountainBoundPos.x)
        {
            ChangeMovementBehaviour(2);
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        switch (col.tag)
        {
            case "DeathHazard": HitDeathHazard(col); break;
            case "BypassableHazard": HitBypassableHazard(col); break;
            case "Bridge": MountBridge(col); break;
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        switch (col.tag)
        {
            case "Bridge": DismountBridge(col); break;
        }
    }

    private void HitDeathHazard(Collider2D col)
    {
        DamagePlayer(col.name);
    }

    private void HitBypassableHazard(Collider2D col)
    {
        if (!isOnBridge)
        {
            DamagePlayer(col.name);
        }
    }

    private void MountBridge(Collider2D col)
    {
        isOnBridge = true;
        transform.position += new Vector3(0f, 0.1f, 0f);
        col.gameObject.GetComponent<PlayerInteractable>().DodoInteract(true);
    }

    private void DismountBridge(Collider2D col)
    {
        isOnBridge = false;
        transform.position -= new Vector3(0f, 0.1f, 0f);
        col.gameObject.GetComponent<PlayerInteractable>().DodoInteract(false);
    }

    void DamagePlayer(string source)
    {
        Debug.Log($"You took damage from {source} and died");
    }
}
