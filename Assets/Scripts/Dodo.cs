using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using Props;
using Controllers;
using UnityEngine;
using Random = UnityEngine.Random;

public class Dodo : MonoBehaviour
{
    //Components and Necessary Assets
    [Tooltip("World Controller used in Scene")]
    [SerializeField] private WorldController _wc;

    private StatsController statsController;

    [SerializeField] Vector3 dodoForwardVector = new Vector3(1f, -.5f);
    [SerializeField] Vector3 dodoLeftVector = new Vector3(1f, 0.5f);

    //Speed and Acceleration
    [SerializeField] private float dodoSpeed;
    [SerializeField] private float dodoAccelerationSpeed;
    private float dodoDefaultSpeed;
    [SerializeField] private float dodoOnLogSpeed = 0.3f;
    private float _currentDodoAcceleration;

    //Sniff
    [SerializeField] float sniffRange;
    private SmellController dodoSniffer;
    [SerializeField] GameObject focusedObject;
    private float distanceTofocusedObject;
    private float focusedObjectPreviousDistance;
    private Vector3 directionOfFocus;

    //Eating
    private bool _isEating = false;

    [Tooltip("Speed at which the dodo (de/ac)celerates surrounding eating.")] [SerializeField]
    float dodoPostEatAcceleration = 0.005f;

    [Tooltip("Range at which the dodo will eat.")] [SerializeField]
    private float eatRange = 1;

    //Movement and Behaviours
    [Tooltip("How often the behaviour automatically changes")] [SerializeField]
    private float behaviourChangeSpeed = 5;

    private int _currentBehaviour = 1;
    private bool _hasMoved = false;
    private float _behaviourTimer;

    private bool b_isTransitionMovement;

    [Tooltip("How wide the Dodo wobbles")] [SerializeField]
    private float _wobbleWidth;

    [Tooltip("How quickly the Dodo wobbles")] [SerializeField]
    private float _wobbleFrequency;

    //Vector line that Dodo Moves on
    private Vector3 _sideVector3 = new Vector3(.5f, 0.25f) / 100;

    //Boundaries
    [SerializeField] Transform _cliffBound;
    private Vector3 _cliffBoundPos;
    [SerializeField] Transform _mountainBound;
    private Vector3 _mountainBoundPos;

    //Bridges
    private bool _isOnBridge;
    private GameObject _BridgeObjectDodoIsOn;

    //Animation
    private Animator _anim;
    private static readonly int DodoSpeed = Animator.StringToHash("dodoSpeed");
    private static readonly int DodoOnBridge = Animator.StringToHash("isOnBridge");

    // Start is called before the first frame update
    private void Start()
    {
        if (_wc == default)
        {
            //World Controller is not set
            throw new Exception("World Controller not set on Dodo Object!");
        }

        statsController = FindObjectOfType<StatsController>();

        dodoDefaultSpeed = dodoSpeed;
        dodoSniffer = GetComponentInChildren<SmellController>();
        dodoSniffer.GetComponent<CircleCollider2D>().radius = sniffRange;
        _cliffBoundPos = _cliffBound.position;
        _mountainBoundPos = _mountainBound.position;
        _anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        if (_isOnBridge)
        {
            return;
        }

        if (_isEating)
        {
            return;
        }

        _behaviourTimer = Time.time % behaviourChangeSpeed;
        Move();
        float currentDodoSpeed = _wc.getWorldSpeed();
        _anim.SetFloat(DodoSpeed, currentDodoSpeed);
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
        focusedObject = dodoSniffer.getSmelledObject();

        if (focusedObject != null)
        {
            MoveTowardsSmellable();
            SlowWorldSpeed();
        }
        else //No smell object, find a new movement
        {
            if (_wc.getWorldSpeedPercentage() < 1f)
            {
                _wc.setWorldSpeedPercentage(Mathf.Clamp(_wc.getWorldSpeedPercentage() + dodoPostEatAcceleration, 0.2f,
                    1));
            }

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
        transform.position += -_sideVector3 * (dodoSpeed * _currentDodoAcceleration);
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
            case "DeathHazard":
                DamagePlayer(col.name);
                break;
            case "BypassableHazard":
                HitBypassableHazard(col);
                break;
            case "Bridge":
                MountBridge(col);
                break;
            case "Tiger":
                DamagePlayer(col.name);
                break;
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        switch (col.tag)
        {
            case "Bridge":
                DismountBridge(col);
                break;
        }
    }

    private void HitBypassableHazard(Collider2D col)
    {
        if (!_isOnBridge)
        {
            DamagePlayer(col.name);
        }
    }

    private void MountBridge(Collider2D col)
    {
        _BridgeObjectDodoIsOn = col.gameObject;
        _wc.setWorldSpeedPercentage(dodoOnLogSpeed);
        _isOnBridge = true;
        _BridgeObjectDodoIsOn.GetComponent<PlayerInteractable>().DodoInteract(true);
        _anim.SetBool(DodoOnBridge, true);
        StartCoroutine(WalkOverBridge());
    }

    private void DismountBridge(Collider2D col)
    {
        statsController.IncrementBridgesCrossed();
        _isOnBridge = false;
        _BridgeObjectDodoIsOn.GetComponent<PlayerInteractable>().DodoInteract(false);
        _anim.SetBool(DodoOnBridge, true);
    }

    void DamagePlayer(string source)
    {
        Debug.Log($"You took damage from {source} and died");
        FindObjectOfType<GameController>().EndGame();
    }

    void MoveTowardsSmellable()
    {
        Vector3 currentPos = transform.position;
        directionOfFocus = currentPos - focusedObject.transform.position;

        //Is smelled object to Dodo's left or right
        float smellCrossProduct = Vector3.Cross(directionOfFocus.normalized, dodoForwardVector.normalized).z;
        //if CrossProduct is > 0, move towards mountains
        if (smellCrossProduct > 0 && _currentBehaviour != 3)
        {
            ChangeMovementBehaviour(3);
        }
        else if (smellCrossProduct < 0 && _currentBehaviour != 2) //Move towards cliff
        {
            ChangeMovementBehaviour(2);
        }
    }

    private void SlowWorldSpeed()
    {
        //Get distance last of last frame
        focusedObjectPreviousDistance = distanceTofocusedObject;

        distanceTofocusedObject = Vector3.Distance(transform.position, focusedObject.transform.position);

        //If Focus/Left CrossProduct is < 0 then dodo has passed focus AND out of eatRange
        if (Vector3.Cross(directionOfFocus.normalized, dodoLeftVector.normalized).z > 0 && distanceTofocusedObject > eatRange)
        {
            //Dodo is not interacting with object
            focusedObject.GetComponent<PlayerInteractable>().DodoInteract(false);
            focusedObject = null;
        }
        else if (!_isEating)
        {
            if (distanceTofocusedObject < eatRange) // Then Feast!!!!
            {

                setEatingStatus(true);
                _wc.setWorldSpeedPercentage(0);
                GetComponentInChildren<DodoEat>().EatMelon();
            }
            else if (distanceTofocusedObject < 3) //focus is getting closer, slow down speed
            {
                focusedObject.GetComponent<PlayerInteractable>().DodoInteract(true);
                _wc.setWorldSpeedPercentage(Mathf.Clamp(_wc.getWorldSpeedPercentage() - dodoPostEatAcceleration, 0.2f * distanceTofocusedObject,
                    1));
            }
            else // Focus is too far away, keep speed max or accelerate
            {
                _wc.setWorldSpeedPercentage(Mathf.Clamp(_wc.getWorldSpeedPercentage() + dodoPostEatAcceleration, 0.2f,
                    1));
            }
        }
    }

    public void setEatingStatus(bool newIsEating)
    {
        if (newIsEating is false)
        {
            statsController.IncrementFoodEaten();
            focusedObject = null;
        }

        _isEating = newIsEating;
    }

    public GameObject getFocusedObject()
    {
        return (focusedObject);
    }

    public bool isEating()
    {
        return _isEating;
    }

    //Changes transform to move on a curve over log
    IEnumerator WalkOverBridge()
    {
        yield return new WaitForSeconds(1);
    }

}
