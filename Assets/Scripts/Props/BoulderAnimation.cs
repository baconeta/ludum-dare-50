using UnityEngine;

namespace Props
{
    public class BoulderAnimation : MonoBehaviour
    {
        [SerializeField] private Animator boulderAnimation;
        [SerializeField] private Animator shadowAnimation;
        
        // Start is called before the first frame update
        public void LiftBoulderAnims()
        {
            // boulderAnimation play forward
            boulderAnimation.enabled = true;
            shadowAnimation.enabled = true;

            boulderAnimation.Play("Boulder");
            shadowAnimation.Play("BoulderShadow");
        }
        public void DropBoulderAnims()
        {
            // boulderAnimation player in reverse
            
            boulderAnimation.Play("BoulderReverse");
            shadowAnimation.Play("ShadowReverse");
        }

        public void StopAllAnims()
        {
            boulderAnimation.enabled = false;
            shadowAnimation.enabled = false;
        }
    }
}
