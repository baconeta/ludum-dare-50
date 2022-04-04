using UnityEngine;

namespace Props
{
    public class PlayerDraggable : PlayerInteractable
    {
        private Collider2D _collider2D;
        protected Vector2 mouseOffset;
        private bool _isFallingFromMountain;
        private float _mountainEdgeCrossProduct;
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
            _collider2D = GetComponent<Collider2D>();
            sr = GetComponent<SpriteRenderer>();
            _dodoObject = FindObjectOfType<Dodo>().gameObject;
            
        }

        // TODO Move onMouseDown & onMouseUp to PlayerInteractable.cs.
        protected virtual void OnMouseDown()
        {
            if (CanPlayerInteract && !IsDodoInteracting)
            {
                IsPlayerInteracting = true;
                EnableCollisions(false);
                
                Vector3 mousePos = ConvertMouseToWorldPosition(Input.mousePosition);
                Vector3 loc = transform.position;
                mouseOffset = (Vector2) loc - (Vector2) mousePos;
            }
        }

        protected virtual void OnMouseUp()
        {
            if (IsPlayerInteracting)
            {
                IsPlayerInteracting = false;
                EnableCollisions(true);
            }
            checkFallOffCliff();
            checkFallFromMountain();
        }

        private void checkFallFromMountain()
        {
            float _mountainEdgeCrossProduct = getCrossProduct("mountain");
            if (_mountainEdgeCrossProduct > 0)
            {
                gameObject.AddComponent<Rigidbody2D>();
                _isFallingFromMountain = true;
            }
        }

        private void checkFallOffCliff()
        {
            
            float edgeObjectCross = getCrossProduct("cliff");
            if (edgeObjectCross < 0)
            {
                gameObject.AddComponent<Rigidbody2D>();
            }

        }

        private float getCrossProduct(string side)
        {
            Vector3 edgeVector = new Vector3(1f, -0.5f, 0f);
            Vector3 edgePoint;
            Vector3 directionOfObject;
            switch(side)
            {
                case "cliff":
                    edgePoint = _worldController.transform.GetChild(4).position;
                    directionOfObject = transform.position - edgePoint;
                    return Vector3.Cross(edgeVector.normalized, directionOfObject.normalized).z;
                case "mountain":
                    edgePoint = _worldController.transform.GetChild(5).position;
                    directionOfObject = transform.position - edgePoint;
                    return Vector3.Cross(edgeVector.normalized, directionOfObject.normalized).z;
            }
            return 0;

        }

        // Update is called once per frame
        protected override void Update()
        {
            if (transform.position.x < 0)
                Destroy(gameObject);
            // If the player is clicking on the object.
            if (IsPlayerInteracting)
            {
                // when you click and hold down, it follows the mouse cursor, and then drops on release.
                Vector3 mousePos = ConvertMouseToWorldPosition(Input.mousePosition);
                Vector3 loc = transform.position;
                loc.x = mousePos.x;
                loc.y = mousePos.y;
                transform.position = loc + (Vector3) mouseOffset;
            }
            else
            {
                bool shouldCheckCross = true;
                if (GetComponent<HeavyLift>())
                {
                    if (GetComponent<HeavyLift>().isRockLifted())
                    {
                        shouldCheckCross = false;
                        sr.sortingOrder = 4;
                    }
                }
                if (shouldCheckCross)
                {
                    _distanceToDodo = Vector3.Distance(_dodoObject.transform.position, transform.position);
                    if (_distanceToDodo < _rangeToCheckCrossProduct)
                    {
                        _directionFromDodo = _dodoObject.transform.position - transform.position;
                        dodoCrossProduct = Vector3.Cross(_directionFromDodo.normalized, _dodoForwardVector.normalized).z;
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
            if (gameObject.GetComponent<Rigidbody2D>())
            {
                if (_isFallingFromMountain && getCrossProduct("mountain") < 0)
                {
                    _isFallingFromMountain = false;
                    Destroy(gameObject.GetComponent<Rigidbody2D>());
                }
            }
            
        }

        protected void EnableCollisions(bool enabled)
        {
            _collider2D.enabled = enabled;
        }
    }
}