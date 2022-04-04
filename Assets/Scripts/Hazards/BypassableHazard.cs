using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BypassableHazard : MonoBehaviour
{
    [Tooltip("The object the player can use to bypass the hazard. Optional.")]
    [CanBeNull]
    public GameObject bypassObject;
    
    private void onGameReset()
    {
        transform.position = new Vector3(1000,1000,0);
    }
}
