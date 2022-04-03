using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BypassableHazard : MonoBehaviour
{
    [Tooltip("The object the player can use to bypass the hazard. Optional.")]
    [CanBeNull]
    public GameObject bypassObject;
}