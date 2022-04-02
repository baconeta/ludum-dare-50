using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Tilemaps;

public class SpawnController : MonoBehaviour
{
    [SerializeField] private GameObject chunkObject;
    [SerializeField] private GameObject spawnPosition;
    private Vector3 _offset = new Vector3(0.25f, 0.125f);
    private bool _shouldSpawn = true;
    private ObstacleController _obstacleController;

    
    // Start is called before the first frame update
    void Start()
    {
        _obstacleController =  FindObjectOfType<ObstacleController>();
        SpawnChunk();
    }

    // Update is called once per frame
    void Update()
    {
        //Get Tilemap Collider2D from WorldChunks - If no collider, spawn new chunk
        Collider2D hit = Physics2D.OverlapPoint(spawnPosition.transform.position + _offset);
        if (!hit && _shouldSpawn)
        {
            SpawnChunk();
            _shouldSpawn = false;
        }

        if (Time.time % 0.2 > 0.15)
        {
            _shouldSpawn = true;
        }
    }
    
    private void SpawnChunk()
    {
        //Instantiate chunk
        GameObject newChunk = Instantiate(chunkObject, this.transform);
        //Instantiate Object
        //TODO: Create object intermittently
        _obstacleController.SpawnObstacle(newChunk);
    }

    public Vector3 GetSpawnPosition()
    {
        return spawnPosition.transform.position;
    }


}
