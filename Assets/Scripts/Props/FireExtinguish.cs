using Props;
using System.Collections;
using System.Collections.Generic;
using System.Transactions;
using UnityEngine;

//TODO: Change to PlayerScrubbable
public class FireExtinguish : PlayerScrubbable
{
    [SerializeField] protected float valueToExtinguish;
    protected override void Start()
    {
        base.Start();
        scrubAmountRequired = valueToExtinguish;
    }
    
    
}
