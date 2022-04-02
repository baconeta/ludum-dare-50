using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class WorldController : MonoBehaviour
{
    private StatsController _statsController;

    [Tooltip("The speed at which the world moves, before any modifiers are applied.")] [SerializeField]
    private float baselineWorldSpeed;
    
    // Modifying this variable will
    [Tooltip("The initial speed at which the world moves. Set to 1.0 for no effect.")] [SerializeField]
    private float initialRampSpeedModifier;
    private float currentRampSpeedModifier;
    public float environmentalSpeedModifier;

    [Tooltip("How many seconds it takes for the rampSpeedModifier to decay to 1.0.")] [SerializeField]
    private float worldSpeedRampDuration;
    private bool ramping = true;
    
    private Vector3 moveDirection = new Vector3(1f, -.5f);


    // Start is called before the first frame update
    void Start()
    {
        _statsController = FindObjectOfType<StatsController>();
        currentRampSpeedModifier = initialRampSpeedModifier;
    }

    // Update is called once per frame
    void Update()
    {
        // Decay ramp modifier to 1 over the course of the ramp duration.
        if (ramping)
        {
            // Check if we have been ramping for long enough.
            if (worldSpeedRampDuration < _statsController.time)
            {
                ramping = false;
            }
            else
            {
                float difference = 1 - initialRampSpeedModifier;
                currentRampSpeedModifier += difference / worldSpeedRampDuration;
            }
        }
    }

    public float getWorldSpeed()
    {
        return baselineWorldSpeed * currentRampSpeedModifier * environmentalSpeedModifier;
    }

    public Vector3 getMoveDirection()
    {
        return moveDirection;
    }
}
