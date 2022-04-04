using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using Props;
using Controllers;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.U2D;
using static BypassableHazard;
using Random = UnityEngine.Random;

public class Dodo : MonoBehaviour
{
    //Components and Necessary Assets
    [Tooltip("World Controller used in Scene")] [SerializeField]
    private WorldController _wc;

    private StatsController _statsController;

    [SerializeField] private Vector3 dodoForwardVector = new Vector3(1f, -.5f);
    [SerializeField] private Vector3 dodoLeftVector = new Vector3(1f, 0.5f);
    private Transform _originTransform;
    private bool _isGameRunning = true;

    //Speed and Acceleration
    [SerializeField] private float dodoSpeed;
    [SerializeField] private float dodoAccelerationSpeed;
    private float dodoDefaultSpeed;
    [SerializeField] private float dodoOnLogSpeed = 0.3f;
    [SerializeField] private float dodoInMudSpeed = 0.5f;
    private float _currentDodoAcceleration;

    //Sniff
    [SerializeField] float sniffRange;
    private SmellController dodoSniffer;
    [SerializeField] GameObject focusedObject;
    private float distanceTofocusedObject;
    private float focusedObjectPreviousDistance;
    private Vector3 directionOfFocus;

    //State
    private bool _isEating = false;
    private bool _isInMud = false;

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

    [Tooltip("The height that the dodo snaps up when getting on the log. Typically < 0.5")] [SerializeField]
    private float _bridgeJumpStrength;

    [Tooltip("The speed at which dodo moves downwards after passing halfway on the log.")] [SerializeField]
    private float _dodoBridgeWalkOffSpeed;

    [Tooltip("The speed at which dodo moves upwards before passing halfway on the log.")] [SerializeField]
    private float _dodoBridgeWalkOnSpeed;

    //Animation
    private Animator _anim;
    private static readonly int DodoSpeed = Animator.StringToHash("dodoSpeed");
    private static readonly int DodoOnBridge = Animator.StringToHash("isOnBridge");
    private static readonly int DodoEating = Animator.StringToHash("isEating");
    private static readonly int DeadDodo = Animator.StringToHash("deadDodo");

    // Start is called before the first frame update
    private void Start()
    {
        if (_wc == default)
        {
            //World Controller is not set
            throw new Exception("World Controller not set on Dodo Object!");
        }

        _statsController = FindObjectOfType<StatsController>();

        dodoDefaultSpeed = dodoSpeed;
        dodoSniffer = GetComponentInChildren<SmellController>();
        dodoSniffer.GetComponent<CircleCollider2D>().radius = sniffRange;
        _cliffBoundPos = _cliffBound.position;
        _mountainBoundPos = _mountainBound.position;
        _anim = GetComponent<Animator>();
        _originTransform = transform;
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        if (!_isGameRunning)
        {
            return;
        }

        if (_isOnBridge)
        {
            WalkThePlank();
            return;
        }

        if (_isEating)
        {
            _anim.SetBool(DodoEating, true);
            return;
        }

        _behaviourTimer = Time.time % behaviourChangeSpeed;
        Move();
        float currentDodoSpeed = _wc.getWorldSpeed();
        _anim.SetBool(DodoEating, false);
        _anim.SetFloat(DodoSpeed, currentDodoSpeed);
    }

    //Ensures dodo walks over the plank
    private void WalkThePlank()
    {
        Vector3 bridgeStartPos = _BridgeObjectDodoIsOn.transform.GetChild(0).position;
        Vector3 bridgeMidPos = _BridgeObjectDodoIsOn.transform.GetChild(1).position;
        Vector3 bridgeEndPos = _BridgeObjectDodoIsOn.transform.GetChild(2).position;


        //Check if Dodo is past halfway on the plank
        if (transform.position.x < bridgeMidPos.x) //Is not over halfway
        {
            transform.position += _sideVector3 * _dodoBridgeWalkOnSpeed;
        }
        else //Dodo is over halfway
        {
            transform.position -= _sideVector3 * _dodoBridgeWalkOffSpeed;
        }

        if (transform.position.x > bridgeEndPos.x)
        {
            DismountBridge(_BridgeObjectDodoIsOn.GetComponent<Collider2D>());
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

        if (_isInMud && _wc.getWorldSpeedPercentage() > dodoInMudSpeed)
        {
            _wc.setWorldSpeedPercentage(dodoInMudSpeed);
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
            case "BypassableHazard":
                LeaveBypassableHazard(col);
                break;
        }
    }

    private void HitBypassableHazard(Collider2D col)
    {
        if (_isOnBridge)
            return;

        var effect = col.GetComponent<BypassableHazard>().collisionEffect;
        switch (effect)
        {
            case Effect.Damage:
                DamagePlayer(col.name);
                break;
            case Effect.Slow:
                _isInMud = true;
                break;
            default:
                throw new Exception($"{nameof(Dodo)}.{nameof(HitBypassableHazard)} doesn't handle {effect}");
        }
    }

    private void LeaveBypassableHazard(Collider2D col)
    {
        var effect = col.GetComponent<BypassableHazard>().collisionEffect;
        switch (effect)
        {
            case Effect.Slow:
                _isInMud = false;
                break;
        }
    }

    private void MountBridge(Collider2D col)
    {
        _BridgeObjectDodoIsOn = col.gameObject;
        BridgeJump();
        _wc.setWorldSpeedPercentage(dodoOnLogSpeed);
        _isOnBridge = true;
        _BridgeObjectDodoIsOn.GetComponent<PlayerInteractable>().DodoInteract(true);
        _anim.SetBool(DodoOnBridge, true);
    }

    private void DismountBridge(Collider2D col)
    {
        BridgeJump(true);
        _statsController.IncrementTrunksTraversed();
        _isOnBridge = false;
        _BridgeObjectDodoIsOn.GetComponent<PlayerInteractable>().DodoInteract(false);
        _anim.SetBool(DodoOnBridge, false);
    }

    private void BridgeJump(bool jumpOff = false)
    {
        if (jumpOff)
        {
            transform.position -= Vector3.up * _bridgeJumpStrength;
        }
        else
        {
            Vector3 bridgeStartPos = _BridgeObjectDodoIsOn.transform.GetChild(0).position;
            Vector3 bridgeMidPos = _BridgeObjectDodoIsOn.transform.GetChild(1).position;
            if (transform.position.x > bridgeStartPos.x)
            {
                transform.position.Set(transform.position.x, bridgeMidPos.y, 0);
            }
            else
            {
                transform.position = bridgeStartPos;
                transform.position += Vector3.up * _bridgeJumpStrength;
            }
        }
    }

    void DamagePlayer(string source)
    {
        Debug.Log($"You took damage from {source} and died");
        _anim.SetFloat(DodoSpeed, 0f);
        _anim.SetBool(DodoOnBridge, false);
        _anim.SetBool(DodoEating, false);
        _anim.SetBool(DeadDodo, true);
        _wc.setWorldSpeedPercentage(0f);
        GameController gc = FindObjectOfType<GameController>();
        gc.gameRunning = false;
        _isGameRunning = false;
        gc.EndGame(2f);
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
            focusedObject.GetComponent<SpriteRenderer>().sortingOrder = 2;
            ChangeMovementBehaviour(3);
        }
        else if (smellCrossProduct < 0 && _currentBehaviour != 2) //Move towards cliff
        {
            focusedObject.GetComponent<SpriteRenderer>().sortingOrder = 4;
            ChangeMovementBehaviour(2);
        }
    }

    private void SlowWorldSpeed()
    {
        //Get distance last of last frame
        focusedObjectPreviousDistance = distanceTofocusedObject;

        distanceTofocusedObject = Vector3.Distance(transform.position, focusedObject.transform.position);

        //If Focus/Left CrossProduct is < 0 then dodo has passed focus
        if (Vector3.Cross(directionOfFocus.normalized, dodoLeftVector.normalized).z > 0)
        {
            if (distanceTofocusedObject > eatRange)
            {
                //Dodo is not focusing object
                focusedObject.GetComponent<PlayerInteractable>().DodoInteract(false);
                focusedObject = null;
            }
        }
        else if (!_isEating)
        {
            if (distanceTofocusedObject < eatRange) // Then Feast!!!!
            {
                _anim.SetBool(DodoEating, true);
                setEatingStatus(true);
                _wc.setWorldSpeedPercentage(0);
                GetComponentInChildren<DodoEat>().EatMelon();
            }
            else if (distanceTofocusedObject < 3) //focus is getting closer, slow down speed
            {
                focusedObject.GetComponent<PlayerInteractable>().DodoInteract(true);
                _wc.setWorldSpeedPercentage(Mathf.Clamp(_wc.getWorldSpeedPercentage() - dodoPostEatAcceleration,
                    0.2f * distanceTofocusedObject,
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
            _statsController.IncrementMelonsMunched();
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

    public void onGameReset()
    {
        // reset his current position back to his original position
        transform.position = _originTransform.position;
        _isEating = false;
        _isInMud = false;
        _currentDodoAcceleration = 0f;
        _currentBehaviour = 1;
        _behaviourTimer = 0f;
        _isGameRunning = true;
    }

    public void onGameEnd()
    {
        // Stop moving the dodo please
        _isGameRunning = false;
        _anim.SetFloat(DodoSpeed, 0f);
    }
}