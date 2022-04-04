using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hazard : MonoBehaviour
{
    private void onGameReset()
    {
        transform.position = new Vector3(1000,1000,0);
    }
}
