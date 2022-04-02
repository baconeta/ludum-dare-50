using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class SpawnController : MonoBehaviour
{
    [SerializeField] private GameObject chunkObject;
    [SerializeField] private GameObject obstacleObject;
    [SerializeField] private GameObject spawnPosition;
    Collider2D c;

    
    // Start is called before the first frame update
    void Start()
    {
        SpawnChunk();
    }

    // Update is called once per frame
    void Update()
    {
        //Get Tilemap Collider2D from WorldChunks - If no collider, spawn new chunk
        Collider2D hit = Physics2D.OverlapPoint(spawnPosition.transform.position);
        if (!hit)
        {
            SpawnChunk();
        }
    }
    
    private void SpawnChunk()
    {
        //Instantiate chunk
        GameObject newChunk = Instantiate(chunkObject, this.transform);
        //Instantiate Object
        Instantiate(obstacleObject, newChunk.transform);
    }

    public Vector3 GetSpawnPosition()
    {
        return spawnPosition.transform.position;
    }


}
