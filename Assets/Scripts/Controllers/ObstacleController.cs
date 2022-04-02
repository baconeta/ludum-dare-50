using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;

public class ObstacleController : MonoBehaviour
{
    [SerializeField] private GameObject[] obstacleTypes;

    public void SpawnObstacle(GameObject parent)
    {
        var obstacleType = GetRandomObstacleType();
        var obstacle = Instantiate(obstacleType, parent.transform);

        var location = GetRandomOffset(parent);
        obstacle.transform.position += location;
    }

    private GameObject GetRandomObstacleType()
    {
        return obstacleTypes[Random.Range(0, obstacleTypes.Length)];
    }

    private Vector3 GetRandomOffset(GameObject parentChunk)
    {
        //TODO: Don't directly access other components of random types!!
        var chunk = parentChunk.GetComponent<Chunk>();
        return chunk.GetRandomTileOffset();
    }
}
