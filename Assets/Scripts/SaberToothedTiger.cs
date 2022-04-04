using Props;
using UnityEngine;

public class SaberToothedTiger : Smashable
{
    private Dodo _dodoToChase;

    // Animation
    private Animator _anim;
    private static readonly int RunningSpeed = Animator.StringToHash("runningSpeed");

    // Speed and movement values
    [Tooltip("This is how much faster the tiger is than the world (at time of spawn).")] [SerializeField]
    private float speedAboveWorld;

    private float _fullSaberSpeed;
    private Vector3 _directionOfFocus;

    [SerializeField] private float sidewaysSpeed = 2f;

    //Vector line that Tiger will moves on TODO this is wrong because it depends where the tiger is on the screen...
    private Vector3 _sideVector3 = new Vector3(.5f, 0.25f) / 100;
    [SerializeField] private Vector3 tigerForwardVector = new Vector3(1f, -.5f);
    [SerializeField] private GameController _gc;


    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        // On spawn we want the tiger to start running towards the dodo.
        _fullSaberSpeed = _worldController.getBaseWorldSpeedOnlyRamp() + speedAboveWorld;
        _dodoToChase = FindObjectOfType<Dodo>();
        _anim = GetComponent<Animator>();
        _anim.SetFloat(RunningSpeed,
            speedAboveWorld + _worldController.getBaseWorldSpeedOnlyRamp() / 2.5f);
        _gc = FindObjectOfType<GameController>();
    }

    // Update is called once per frame
    protected override void Update()
    {
        if (_gc != default)
        {
            if (!_gc.gameRunning)
            {
                _anim.SetFloat(RunningSpeed, 0f);
                return;
            }
        }

        MoveTowardsDodo();
        Vector3 movementVector = _worldController.getMoveDirection() *
                                 (Time.deltaTime * (_fullSaberSpeed - _worldController.getWorldSpeed()));
        transform.position += movementVector;
    }

    private void MoveTowardsDodo()
    {
        Vector3 currentPos = transform.position;
        _directionOfFocus = currentPos - _dodoToChase.transform.position;

        //Is chased Dodo to the left or right of the tiger
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