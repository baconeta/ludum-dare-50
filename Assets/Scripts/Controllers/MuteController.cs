using System;
using UnityEngine;
using UnityEngine.UI;

namespace Controllers
{
    public class MuteController : MonoBehaviour
    {
        public Sprite unmutedImage;
        public Sprite mutedImage;
        private Toggle _isMuted;

        [SerializeField] private Slider volumeSlider;

        private SpriteRenderer _spriteRenderer;

        // Start is called before the first frame update
        private void Start()
        {
            _isMuted = GetComponent<Toggle>();
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _isMuted.isOn = false;
        }

        public void MuteButton()
        {
            if (_isMuted.isOn)
            {
                _spriteRenderer.sprite = mutedImage;
                AudioListener.volume = 0;
            }
            else
            {
                _spriteRenderer.sprite = unmutedImage;
                AudioListener.volume = volumeSlider.value;
            }
        }
    }
}