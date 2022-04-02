using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnController : MonoBehaviour
{
    [SerializeField] private float spawnFrequency = 5;
    [SerializeField] private GameObject chunkObject;
    [SerializeField] private GameObject obstacleObject;
    [SerializeField] private GameObject spawnPosition;
    private float m_SpawnTimer;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        //spawnFrequency = FindObjectOfType<WorldController>().getWorldSpeed();
        
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
    
}
