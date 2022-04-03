using Controllers;
using UnityEngine;

public class Sky : MonoBehaviour
{
    public WorldController worldController;
    public BoxCollider2D collider;
    public Rigidbody2D rb;

    [SerializeField] private Transform center;
    [Tooltip("How much faster than the Dodo does the sky move.")]
    [SerializeField] private float passiveSkySpeed = 0.5f;

    private float originalZ;
    private float width;

    
    void Start()
    {
        worldController = FindObjectOfType<WorldController>();
        collider = GetComponent <BoxCollider2D>();
        rb = GetComponent <Rigidbody2D>();

        width = collider.size.x * transform.localScale.x;
        collider.enabled = false;

        originalZ = transform.position.z;
    }

    void Update()
    {
        float speed = worldController.getWorldSpeed() + passiveSkySpeed;
        rb.velocity = new Vector2(-speed, 0);

        if (transform.position.x < center.position.x - width)
        {
            Vector3 resetAdjustment = new Vector3(width * 2f, 0, 0);
            transform.position = transform.position + resetAdjustment;
        }
    }
}
