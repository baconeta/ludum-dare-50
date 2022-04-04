using Controllers;
using Props;
using UnityEngine;

public class SaberToothedTiger : Smashable
{
    private Dodo _dodoToChase;
    private Transform _cliffBound;
    Vector3 _edgeVector = new Vector3(1f, -0.5f, 0f);

    // Animation
    private Animator _anim;
    private static readonly int RunningSpeed = Animator.StringToHash("runningSpeed");

    // Speed and movement values
    [Tooltip("This is how much faster the tiger is than the world (at time of spawn).")] [SerializeField]
    private float speedAboveWorld;

    private float _fullSaberSpeed;
    private Vector3 _directionOfDodo;

    [SerializeField] private float sidewaysSpeed = 2f;

    //Vector line that Tiger will moves on TODO this is wrong because it depends where the tiger is on the screen...
    private Vector3 _sideVector3 = new Vector3(.5f, 0.25f) / 100;
    [SerializeField] private Vector3 tigerForwardVector = new Vector3(1f, -.5f);


    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        // On spawn we want the tiger to start running towards the dodo.
        _fullSaberSpeed = _worldController.getWorldSpeed() + speedAboveWorld;
        _dodoToChase = FindObjectOfType<Dodo>();
        _anim = GetComponent<Animator>();
        _anim.SetFloat(RunningSpeed,
            speedAboveWorld + _worldController.getWorldSpeed() / (_worldController.getWorldSpeedPercentage()+0.01f) / 2.5f);
        _cliffBound = FindObjectOfType<PredatorController>().transform.GetChild(1);
    }

    // Update is called once per frame
    protected override void Update()
    {
        MoveTowardsDodo();
        Vector3 movementVector = _worldController.getMoveDirection() *
                                 (Time.deltaTime * (_fullSaberSpeed - _worldController.getWorldSpeed()));
        transform.position += movementVector;
    }

    private void MoveTowardsDodo()
    {
        Vector3 currentPos = transform.position;
        Vector3 _tigerDirection = _cliffBound.position - currentPos;

        float crossFromEdge = Vector3.Cross(_edgeVector.normalized, _tigerDirection.normalized).z;
        
        //if tiger hits edge, move away.
        if (crossFromEdge > 0)//if edge cross product < 0, move upwards
        {
            Debug.Log("Moving from edge");
            transform.position += _sideVector3 * sidewaysSpeed;
        }
        else
        {
            _directionOfDodo = currentPos - _dodoToChase.transform.position;
            
            //Is Dodo to the left or right of the tiger
            float smellCrossProduct = Vector3.Cross(_directionOfDodo.normalized, tigerForwardVector.normalized).z;
            
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
}