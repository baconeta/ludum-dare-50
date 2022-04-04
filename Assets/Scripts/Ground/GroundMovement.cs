using Controllers;
using Props;
using UnityEngine;

namespace Ground
{
    public class GroundMovement : MonoBehaviour
    {
        private Collider2D _Collider;
        private Ground _ground;
        private WorldController _worldController;

        // Start is called before the first frame update
        private void Start()
        {
            if (!gameObject.GetComponent<PlayerDraggable>())
            {
                // Activate the ground movement here
                _ground = FindObjectOfType<Ground>();
                _worldController = _ground.GetWorldCollider();
            }

            _Collider = GetComponent<Collider2D>();
            _Collider.enabled = false;
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