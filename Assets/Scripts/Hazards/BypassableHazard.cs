using JetBrains.Annotations;
using UnityEngine;

public class BypassableHazard : Hazard
{
    [Tooltip("The object the player can use to bypass the hazard. Optional.")]
    [CanBeNull]
    public GameObject bypassObject;
    public Effect collisionEffect;

    public enum Effect
    {
        Damage,
        Slow
    }
}
