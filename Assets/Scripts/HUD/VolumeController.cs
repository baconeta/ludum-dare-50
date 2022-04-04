using Controllers;
using UnityEngine;

namespace HUD
{
    public class VolumeController : MonoBehaviour
    {
        private MuteController muteController;
        // Start is called before the first frame update
        void Start()
        {
            muteController = FindObjectOfType<MuteController>();
        }

        public void OnValueChanged(float sliderValue)
        {
            if (!muteController.isMuted)
            {
                AudioListener.volume = sliderValue;
            }
        }
    }
}
