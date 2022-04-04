using JetBrains.Annotations;
using UnityEngine;

public class BypassableHazard : MonoBehaviour
{
    [Tooltip("The object the player can use to bypass the hazard. Optional.")]
    [CanBeNull]
    public GameObject bypassObject;
    public Effect collisionEffect;
    
    private void onGameReset()
    {
        transform.position = new Vector3(1000,1000,0);
    }

    public enum Effect
    {
        Damage,
        Slow
    }
}
