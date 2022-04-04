using UnityEngine;

//TODO: Change to PlayerScrubbable
public class FireExtinguish : PlayerScrubbable
{
    [SerializeField] protected float valueToExtinguish;
    [SerializeField] protected Sprite extinguishedSprite;
    private GameObject _dodoObject;
    private Vector3 _dodoForwardVector = new Vector3(1f, -.5f);
    private Vector3 _directionFromDodo;
    private float _distanceToDodo;
    private float _rangeToCheckCrossProduct = 4;
    private float dodoCrossProduct;
    private SpriteRenderer sr;
    
    protected override void Start()
    {
        base.Start();
        scrubAmountRequired = valueToExtinguish;
        scrubbedSprite = extinguishedSprite;
        _dodoObject = FindObjectOfType<Dodo>().gameObject;
        sr = GetComponent<SpriteRenderer>();
    }

    private void FixedUpdate()
    {
        _distanceToDodo = Vector3.Distance(_dodoObject.transform.position, transform.position);
        if (_distanceToDodo < _rangeToCheckCrossProduct)
        {
            _directionFromDodo = _dodoObject.transform.position - transform.position;
            dodoCrossProduct = Vector3.Cross(_directionFromDodo.normalized, _dodoForwardVector.normalized).z;
            Debug.Log(dodoCrossProduct);
            if (dodoCrossProduct > 0)
            {
                sr.sortingOrder = 2;
            }
            else
            {
                sr.sortingOrder = 4;
            }
        }
    }
    
    
}
