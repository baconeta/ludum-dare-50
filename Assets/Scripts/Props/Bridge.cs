using UnityEngine;

namespace Props
{
    public class Bridge : PlayerDraggable
    {
        private AudioSource _audioSource;

        protected override void Start()
        {
            _audioSource = GetComponent<AudioSource>();
            base.Start();
            // Left just for LD speed.
        }

        protected override void OnObjectLanded()
        {
            // Play wooden thud
            _audioSource.Play();
        }
    }
}