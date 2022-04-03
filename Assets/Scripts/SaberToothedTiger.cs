using Controllers;
using UnityEngine;

public class SaberToothedTiger : MonoBehaviour
{
    private float _fullSaberSpeed;

    [Tooltip("This is how much faster the tiger is than the world (at time of spawn).")] [SerializeField]
    private float speedAboveWorld;

    private WorldController _worldController;

    // Start is called before the first frame update
    private void Start()
    {
        _worldController = FindObjectOfType<WorldController>();
        // On spawn we want the tiger to start running towards the dodo.
        _fullSaberSpeed = _worldController.getWorldSpeed() + speedAboveWorld;
    }

    // Update is called once per frame
    private void Update()
    {
        transform.position +=
            _worldController.getMoveDirection() *
            (Time.deltaTime * (_fullSaberSpeed - _worldController.getWorldSpeed()));
    }
}