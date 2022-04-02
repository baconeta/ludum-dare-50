using Controllers;
using UnityEngine;

namespace Ground
{
    public class GroundMovement : MonoBehaviour
    {
        private BoxCollider2D _boxCollider;
        private Ground _ground;
        private WorldController _worldController;

        // Start is called before the first frame update
        private void Awake()
        {
            // Activate the ground movement here
            _ground = FindObjectOfType<Ground>();
            _worldController = _ground.GetWorldCollider();

            _boxCollider = GetComponent<BoxCollider2D>();
            _boxCollider.enabled = false;
        }

        // Update is called once per frame
        private void Update()
        {
            if (!_worldController)
            {
                _worldController = _ground.GetWorldCollider();
            }

            transform.position -=
                _worldController.getMoveDirection() * (Time.deltaTime * _worldController.getWorldSpeed());
        }
    }
}