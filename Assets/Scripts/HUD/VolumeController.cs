using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MuteButton : MonoBehaviour
{
    AudioSource m_BackgroundMusic;
    // Start is called before the first frame update
    void Start()
    {
        m_BackgroundMusic = GetComponent<AudioSource>();
        
    }

    // Update is called once per frame
    void Update()
    {
        float volume = m_BackgroundMusic.volume;
        m_BackgroundMusic.volume = volume + (float) 0.05;
    }

    public void OnValueChanged(float sliderValue)
    {
    }
}
