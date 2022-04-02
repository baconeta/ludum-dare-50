using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ObstacleController : MonoBehaviour
{
    [SerializeField] private GameObject[] obstacleTypes;

    public void SpawnObstacle(GameObject parent)
    {
        //TODO: Randomise
        var obstacleType = obstacleTypes.First();

        Instantiate(obstacleType, parent.transform);
    }
}
