using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VolumeController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
    }

    public void OnValueChanged(float sliderValue)
    {
        AudioListener.volume = sliderValue;
    }
}
