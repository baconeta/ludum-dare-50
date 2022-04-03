using System;
using Props;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Controllers;
using UnityEngine;
using UnityEngine.UIElements;
using Random = UnityEngine.Random;

public class Dodo : MonoBehaviour
{
    [Tooltip("World Controller used in Scene")]
    [SerializeField] WorldController _wc;
    //Speed and Acceleration
    [SerializeField] private float dodoSpeed;
    [SerializeField] private float dodoAccelerationSpeed;
    private float dodoDefaultSpeed;
    private float _currentDodoAcceleration;

    //Sniff
    [SerializeField] Vector3 forwardVector = new Vector3(0.5f, -0.25f);
    [SerializeField] float sniffRange;
    SmellController dodoSniffer;

    //Movement and Behaviours
    [Tooltip("How often the behaviour automatically changes")]
    [SerializeField] private float behaviourChangeSpeed = 5;
    private int _currentBehaviour = 1;
    private bool _hasMoved = false;
    private float _behaviourTimer;

    private bool b_isTransitionMovement;
    [Tooltip("How wide the Dodo wobbles")]
    [SerializeField]private float _wobbleWidth;
    [Tooltip("How quickly the Dodo wobbles")]
    [SerializeField] private float _wobbleFrequency;

    //Vector line that Dodo Moves on
    private Vector3 _sideVector3 = new Vector3(.5f, 0.25f) / 100;

    //Boundaries
    [SerializeField] Transform _cliffBound;
    private Vector3 _cliffBoundPos;
    [SerializeField] Transform _mountainBound;
    private Vector3 _mountainBoundPos;

    //Bridges
    private bool isOnBridge;
    
    // Start is called before the first frame update
    private void Start()
    {
        if (_wc == default)
        {
            //World Controller is not set
            throw new NullReferenceException();
        }

        dodoDefaultSpeed = dodoSpeed;
        dodoSniffer = GetComponentInChildren<SmellController>();
        dodoSniffer.GetComponent<CircleCollider2D>().radius = sniffRange;
        _cliffBoundPos = _cliffBound.position;
        _mountainBoundPos = _mountainBound.position;
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        if (!isOnBridge)
        {
            _behaviourTimer = Time.time % behaviourChangeSpeed;
            Move();
        }
    }

    //Input desiredBehaviour to choose a behaviour
    private void ChangeMovementBehaviour(int desiredBehaviour = 0)
    {
        _currentDodoAcceleration = 0.01f;
        //If a chosen behaviour has been given
        if (desiredBehaviour != 0)
        {
            _currentBehaviour = desiredBehaviour;
        }
        else //Get a new behaviour
        {
            //If current behaviour is not Moving Forwards (1)
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

    private void Move()
    {
        _currentDodoAcceleration += dodoAccelerationSpeed / 100;
        _currentDodoAcceleration = Mathf.Clamp(_currentDodoAcceleration, 0, 1);

        //If there is a viable smell object - Move towards it.
        GameObject smelledObject = dodoSniffer.getSmelledObject();
        if (smelledObject != null)
        {
            MoveTowardsSmellable(smelledObject);
            SlowWorldSpeed();
        }
        else //No smell object, find a new movement
        {
            if (_behaviourTimer < 0.1 && !_hasMoved)
            {
                ChangeMovementBehaviour();
                _hasMoved = true;
            }
            if (_behaviourTimer > 0.1)
            {
                _hasMoved = false;
            }
            if (_behaviourTimer > behaviourChangeSpeed - 0.1)
            {
                b_isTransitionMovement = false;
                behaviourChangeSpeed = 5;
            }
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

    }
    private void MoveForwards()
    {
        float sinWave = _wobbleWidth * Mathf.Sin(Time.time * _wobbleFrequency);
        transform.position += _sideVector3 * sinWave;

    }

    private void MoveTowardCliff()
    {

        transform.position +=  -_sideVector3 * (dodoSpeed * _currentDodoAcceleration);
        //If at bounds, inverse direction
        if (transform.position.x <= _cliffBoundPos.x)
        {
            transform.position = _cliffBoundPos;
            // move to Mountain
            ChangeMovementBehaviour(3);
        }
    }

    private void MoveTowardMountain()
    {
        transform.position += _sideVector3 * (dodoSpeed * _currentDodoAcceleration);
        //If at bounds, inverse direction
        if (transform.position.x >= _mountainBoundPos.x)
        {
            transform.position = _mountainBoundPos;

            // move to cliff
            ChangeMovementBehaviour(2);
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        switch (col.tag)
        {
            case "DeathHazard": DamagePlayer(col.name); break;
            case "BypassableHazard": HitBypassableHazard(col); break;
            case "Bridge": MountBridge(col); break;
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        switch (col.tag)
        {
            case "Bridge": DismountBridge(col); break;
        }
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

    void MoveTowardsSmellable(GameObject smelledObject)
    {
        smelledObject.GetComponent<PlayerInteractable>().DodoInteract(true);
        Vector3 currentPos = transform.position;
        Vector3 directionOfSmell = currentPos - smelledObject.transform.position;

        //Is smelled object to Dodo's left or right
        float smellCrossProduct = Vector3.Cross(directionOfSmell.normalized, forwardVector.normalized).z;
        //if CrossProduct is > 0, move towards mountains
        if (smellCrossProduct > 0 && _currentBehaviour != 3)
        {
            ChangeMovementBehaviour(3);
        }
        else if(smellCrossProduct < 0 && _currentBehaviour != 2)//Move towards cliff
        {
            ChangeMovementBehaviour(2);
        }
    }

    private void SlowWorldSpeed()
    {
        
    }
}
