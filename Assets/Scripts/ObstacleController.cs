using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleController : MonoBehaviour
{
    
    [SerializeField] private float obstacleSpeed;
    private Vector3 obstacleOrigin;
    private float timer;

    // Start is called before the first frame update
    void Start()
    {
        obstacleOrigin = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.fixedDeltaTime;
        transform.localPosition = new Vector3(-Mathf.Sin(timer), Mathf.Sin(timer) / 2) * obstacleSpeed * Time.fixedDeltaTime;
    }
}
