using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ObstacleController : MonoBehaviour
{
    [SerializeField] private GameObject[] obstacleTypes;

    public void SpawnObstacle(GameObject parent)
    {
        var obstacleType = GetRandomObstacleType();

        Instantiate(obstacleType, parent.transform);
    }

    private GameObject GetRandomObstacleType()
    {
        return obstacleTypes[Random.Range(0, obstacleTypes.Length)];
    }
}
