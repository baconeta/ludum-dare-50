using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class SpawnController : MonoBehaviour
{
    [SerializeField] private float spawnFrequency = 5;
    [SerializeField] private GameObject chunkObject;
    [SerializeField] private GameObject obstacleObject;
    [SerializeField] private GameObject spawnPosition;
    private float m_SpawnTimer;

    [SerializeField] Collider2D c;

    private bool m_B_shouldSpawn;
    
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
