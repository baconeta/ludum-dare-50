using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DodoController : MonoBehaviour
{
    [SerializeField] private float dodoSpeed;
    private float timer;
    private Vector3 dodoOrigin;
    
    // Start is called before the first frame update
    void Start()
    {
        dodoOrigin = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.fixedDeltaTime;
        transform.position = dodoOrigin + (new Vector3(-Mathf.Sin(timer), Mathf.Sin(timer) / 2) * dodoSpeed * Time.fixedDeltaTime);
    }
}
