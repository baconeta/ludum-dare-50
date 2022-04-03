using Controllers;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Ground
{
    public class Ground : MonoBehaviour
    {
        private WorldController _worldController;
        [SerializeField] private Sprite[] possibleGroundPieces;
        [SerializeField] private float removeXPosition;

        // Start is called before the first frame update
        private void Awake()
        {
            _worldController = FindObjectOfType<WorldController>();
            SpriteRenderer sr = GetComponent<SpriteRenderer>();
            sr.sprite = possibleGroundPieces.ChooseRandom();
        }

        public WorldController GetWorldCollider()
        {
            return _worldController;
        }

        private void Update()
        {
            if (transform.position.x < removeXPosition)
            {
                Die();
            }
        }

        private void Die()
        {
            Destroy(gameObject);
        }
    }
}