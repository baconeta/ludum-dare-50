using UnityEngine;
using Random = UnityEngine.Random;

namespace Controllers
{
    public class SpawnController : MonoBehaviour
    {
        [SerializeField]
        [Min(0.1f)]
        [Tooltip("The number of chunks for each Obstacle. Less than 1 will create multiple obstacles per chunk.")]
        private float chunkToObstacleRatio;

        [SerializeField] private GameObject chunkObject;
        [SerializeField] private GameObject spawnPosition;
        private ObstacleController _obstacleController;
        private int _chunksAddedSinceLastObstacle = 0;
        private Vector3 _offset = new Vector3(0.25f, 0.125f);
        private bool _shouldSpawn = true;

    
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
            _chunksAddedSinceLastObstacle++;

            SpawnObstacles(newChunk);
        }

        private void SpawnObstacles(GameObject parent)
        {
            var spawnCount = CalculateNumberOfObstaclesToSpawn();
            for (var i = 0; i < spawnCount; i++)
            {
                _obstacleController.SpawnObstacle(parent);
                _chunksAddedSinceLastObstacle = 0;
            }
        }

        private int CalculateNumberOfObstaclesToSpawn()
        {
            var randomisedRatio = chunkToObstacleRatio + (chunkToObstacleRatio * 0.4 * Random.value);
            var spawnCount = (int)(_chunksAddedSinceLastObstacle / randomisedRatio);

            if (spawnCount > 0)
            {
                Debug.Log($"Spawning {spawnCount} obstacles after {_chunksAddedSinceLastObstacle} chunks due to randomised ratio {randomisedRatio}");
            }

            return spawnCount;
        }

        public Vector3 GetSpawnPosition()
        {
            return spawnPosition.transform.position;
        }


    }
}
