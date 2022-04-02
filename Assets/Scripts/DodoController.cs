using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class DodoController : MonoBehaviour
{
    [SerializeField] private float dodoSpeed;
    private float timer;
    private Vector3 dodoOrigin;
    [SerializeField] private Transform cliffBounds;
    [SerializeField] private Transform mountainBounds;
    private Vector3 m_ForwardDirection = new Vector3(1f, -.5f);
    private Vector3 m_CliffDirection = new Vector3(-1f, -.5f);
    private Vector3 m_MountainDirection = new Vector3(1f, .5f);
  


    // Start is called before the first frame update
    void Start()
    {
        dodoOrigin = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.Lerp(cliffBounds.position, mountainBounds.position, Mathf.PingPong(Time.time, 1));
    }


}
