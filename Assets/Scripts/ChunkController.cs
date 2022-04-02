using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using Random = UnityEngine.Random;

public class ChunkController : MonoBehaviour
{
    
    [SerializeField] private WorldController worldController;
    [SerializeField] private SpawnController spawnController;
    [SerializeField] private float removePosition;
    [SerializeField] private TileBase[] tileArray;
    
    [SerializeField] int maxWidth = 10;
    [SerializeField] int minWidth = 4;
    private int chunkLength = 5;

    // Start is called before the first frame update
    void Start()
    {
        
        int chunkWidth = Random.Range(minWidth, maxWidth);
        Tilemap tileMap = GetComponent<Tilemap>();
        for (int y = 0; y < chunkLength; y++)
        {
            for (int x = 0; x < chunkWidth; x++)
            {
                TileBase randomTile = tileArray[Random.Range(0, tileArray.Length)];
               tileMap.SetTile(new Vector3Int(x, y, 0), randomTile);
            }
        }
        
        worldController = FindObjectOfType<WorldController>();
        spawnController = FindObjectOfType<SpawnController>();
        transform.position = spawnController.GetSpawnPosition();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = transform.position - worldController.getMoveDirection() * worldController.getWorldSpeed() * Time.fixedDeltaTime;
        if (transform.position.x < removePosition)
        {
            Die();
        }
    }

    void Die()
    {
        Destroy(this.gameObject);
    }

    
}
