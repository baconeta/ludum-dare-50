using Ground;
using UnityEngine;

namespace Controllers
{
    /// <summary>
    /// Sits in the world, generating new ground objects when the previous one has moved enough distance
    /// </summary>
    ///
    public class GroundController : MonoBehaviour
    {
        private BoxCollider2D _boxCollider;
        private float _lastGroundWidth;
        private GameObject _lastGroundPiece;
        private GameObject _nextGroundPiece;
        private ObstacleController _obstacleController;
        private int _chunksAddedSinceLastObstacle = 0;
        [SerializeField] private bool debug;
        [SerializeField] private GameObject spawnPosition;
        [SerializeField] private GameObject instantiatePosition;
        [SerializeField] private GameObject standardGroundObjectToSpawn;
        [SerializeField] private GameObject largeGroundObjectToSpawn;

        [SerializeField]
        [Min(0.1f)]
        [Tooltip("The number of chunks for each Obstacle. Less than 1 will create multiple obstacles per chunk.")]
        private float chunkToObstacleRatio;

        void Start()
        {
            _obstacleController = FindObjectOfType<ObstacleController>();
            _nextGroundPiece = Instantiate(standardGroundObjectToSpawn, instantiatePosition.transform);
            SpawnNewGround();
        }

        private void SpawnNewGround()
        {
            // place previous ground piece into the right location
            _nextGroundPiece.transform.position = spawnPosition.transform.position;
            _lastGroundPiece = _nextGroundPiece;
            _lastGroundPiece.AddComponent<GroundMovement>();
            _boxCollider = _nextGroundPiece.GetComponent<BoxCollider2D>();
            _lastGroundWidth = _boxCollider.size.x * _nextGroundPiece.transform.localScale.x;

            // Create a new ground piece for the next spawn
            var nextGroundTile = SelectNextGroundTile();
            _nextGroundPiece = Instantiate(nextGroundTile, instantiatePosition.transform);
            _chunksAddedSinceLastObstacle++;
        }

        // Update is called once per frame
        void Update()
        {
            //the x position of the last ground object (centre pos)
            float xPosLastGround = _lastGroundPiece.transform.position.x;
            if (xPosLastGround <= 1.01 * (spawnPosition.transform.position.x - _lastGroundWidth))
            {
                SpawnNewGround();
                SpawnObstacles();
            }
        }

        private void SpawnObstacles()
        {
            var parent = _nextGroundPiece;
            var spawnCount = CalculateNumberOfObstaclesToSpawn();
            for (var i = 0; i < spawnCount; i++)
            {
                _obstacleController.SpawnObstacle(parent, _lastGroundWidth);
                _chunksAddedSinceLastObstacle = 0;
            }
        }

        private int CalculateNumberOfObstaclesToSpawn()
        {
            var randomisedRatio = chunkToObstacleRatio + (chunkToObstacleRatio * 0.4 * Random.value);
            var spawnCount = (int)(_chunksAddedSinceLastObstacle / randomisedRatio);

            if (debug && spawnCount > 0)
            {
                Debug.Log($"Spawning {spawnCount} obstacles after {_chunksAddedSinceLastObstacle} chunks due to randomised ratio {randomisedRatio}");
            }

            return spawnCount;
        }

        private GameObject SelectNextGroundTile()
        {
            return Random.value > 0.7 ? largeGroundObjectToSpawn : standardGroundObjectToSpawn;
        }
    }
}