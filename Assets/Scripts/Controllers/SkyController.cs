using UnityEngine;

public class SkyController : MonoBehaviour
{

    public BoxCollider2D collider;

    public Rigidbody2D rb;

    [SerializeField] private Transform center;
    private float Z;

    private float width;

    private float scrollSpeed = -2f;
    
    void Start()
    {
        collider = GetComponent <BoxCollider2D>();
        rb = GetComponent <Rigidbody2D>();

        width = collider.size.x * transform.localScale.x;
        collider.enabled = false;

        rb.velocity = new Vector2(scrollSpeed, 0);

        Z = transform.position.z;
    }

    void Update()
    {
        if (transform.position.x < center.position.x - width)
        {
            Vector3 resetAdjustment = new Vector3(width * 2f, 0, 0);
            transform.position = transform.position + resetAdjustment;
        }
    }
}
