using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChunkController : MonoBehaviour
{
    
    [SerializeField] private float removePosition = 0.5f;

    [SerializeField] private WorldController worldController;

    [SerializeField] private SpawnController spawnController;
    // Start is called before the first frame update
    void Start()
    {
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
