using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.U2D.Path;
using UnityEngine;
using UnityEngine.Tilemaps;
using Random = UnityEngine.Random;

public class Chunk : MonoBehaviour
{
    [SerializeField] private WorldController worldController;
    [SerializeField] private SpawnController spawnController;
    [SerializeField] private TileBase[] tileArray;
    [SerializeField] private float removePosition;

    [SerializeField] int maxWidth = 10;
    [SerializeField] int minWidth = 4;
    private float chunkLength = 5;
    private float chunkWidth;
    private Tilemap tileMap;
    private Vector3 offset = new Vector3(2, -1f);

    // Start is called before the first frame update
    void Start()
    {
        chunkWidth = Random.Range(minWidth, maxWidth);
        tileMap = GetComponent<Tilemap>();
        for (int y = 0; y < chunkLength; y++)
        {
            for (int x = 0; x < chunkWidth; x++)
            {
                TileBase randomTile = tileArray[Random.Range(0, tileArray.Length)];
                tileMap.SetTile(new Vector3Int(x, y, 0), randomTile);
            }
        }

        setColliderCorners();

        worldController = FindObjectOfType<WorldController>();
        spawnController = FindObjectOfType<SpawnController>();
        transform.position = spawnController.GetSpawnPosition() + offset;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position -=
            worldController.getMoveDirection() * Time.fixedDeltaTime * worldController.getWorldSpeed();
        if (transform.position.x < removePosition)
        {
            Die();
        }
    }

    void Die()
    {
        Destroy(this.gameObject);
    }

    void setColliderCorners()
    {
        int pointIndex = 0;
        PolygonCollider2D collider = GetComponent<PolygonCollider2D>();
        Vector2[] points =
        {
            new Vector2(0, 0),
            new Vector2(-chunkLength / 2, chunkLength / 4),
            new Vector2(chunkWidth / 2 - chunkLength / 2, chunkWidth / 4 + chunkLength / 4),
            new Vector2(chunkWidth / 2, chunkWidth / 4)
        };
        collider.SetPath(0, points);
    }

    public Vector3 GetRandomTileOffset()
    {
        var x = Random.Range(0, (int)chunkWidth);
        var y = Random.Range(0, (int)chunkLength);
        var tileOffset = new Vector3(x, y, 0);
        return tileOffset;
    }
}
