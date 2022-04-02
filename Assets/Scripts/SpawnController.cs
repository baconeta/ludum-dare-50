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
        c = GetComponent<BoxCollider2D>();
        SpawnChunk();
    }

    // Update is called once per frame
    void Update()
    {
        if (m_SpawnTimer > spawnFrequency)
        {
            m_SpawnTimer = 0;
            SpawnChunk();
        }
        m_SpawnTimer += Time.deltaTime;
    }
    
    private void SpawnChunk()
    {
        GameObject newChunk = Instantiate(chunkObject, this.transform);
        Instantiate(obstacleObject, newChunk.transform);
    }

    public Vector3 GetSpawnPosition()
    {
        return spawnPosition.transform.position;
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        Debug.Log("Spawn chunk");
        SpawnChunk();
    }
}
