using Controllers;
using UnityEngine;

public class SaberToothedTiger : MonoBehaviour
{
    private float _fullSaberSpeed;

    [Tooltip("This is how much faster the tiger is than the world (at time of spawn).")] [SerializeField]
    private float speedAboveWorld;

    private WorldController _worldController;

    private Dodo _dodoToChase;
    private Vector3 _directionOfFocus;
    [SerializeField] private float sidewaysSpeed = 2f;

    //Vector line that Tiger will moves on TODO this is wrong because it depends where the tiger is on the screen...
    private Vector3 _sideVector3 = new Vector3(.5f, 0.25f) / 100;
    [SerializeField] Vector3 tigerForwardVector = new Vector3(1f, -.5f);

    // Start is called before the first frame update
    private void Start()
    {
        _worldController = FindObjectOfType<WorldController>();
        // On spawn we want the tiger to start running towards the dodo.
        _fullSaberSpeed = _worldController.getWorldSpeed() + speedAboveWorld;
        _dodoToChase = FindObjectOfType<Dodo>();
    }

    // Update is called once per frame
    private void Update()
    {
        MoveTowardsDodo();
        Vector3 movementVector = _worldController.getMoveDirection() *
                                 (Time.deltaTime * (_fullSaberSpeed - _worldController.getWorldSpeed()));
        transform.position += movementVector;
    }

    void MoveTowardsDodo()
    {
        Vector3 currentPos = transform.position;
        _directionOfFocus = currentPos - _dodoToChase.transform.position;

        //Is smelled object to Dodo's left or right
        float smellCrossProduct = Vector3.Cross(_directionOfFocus.normalized, tigerForwardVector.normalized).z;
        //if CrossProduct is > 0, move towards mountains
        if (smellCrossProduct > 0)
        {
            transform.position += _sideVector3 * sidewaysSpeed;
        }
        else if (smellCrossProduct < 0) //Move towards cliff
        {
            transform.position += -_sideVector3 * sidewaysSpeed;
        }
    }
}