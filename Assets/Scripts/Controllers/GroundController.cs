using UnityEngine;

namespace Controllers
{
    /// <summary>
    /// Sits in the world, generating new ground objects when the previous one has moved enough distance
    /// </summary>
    ///
    public class GroundController : MonoBehaviour
    {
        // Start is called before the first frame update
        private BoxCollider2D _boxCollider;
        private float _lastGroundWidth;
        private GameObject _lastGroundPiece;
        private GameObject _nextGroundPiece;
        [SerializeField] private GameObject spawnPosition;
        [SerializeField] private GameObject instantiatePosition;
        [SerializeField] private GameObject groundObjectToSpawn;

        void Start()
        {
            _nextGroundPiece = Instantiate(groundObjectToSpawn, instantiatePosition.transform);
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
            _nextGroundPiece = Instantiate(groundObjectToSpawn, instantiatePosition.transform);
        }

        // Update is called once per frame
        void Update()
        {
            //the x position of the last ground object (centre pos)
            float xPosLastGround = _lastGroundPiece.transform.position.x;

            if (xPosLastGround <= 1.01 * (spawnPosition.transform.position.x - _lastGroundWidth))
            {
                SpawnNewGround();
            }
        }
    }
}