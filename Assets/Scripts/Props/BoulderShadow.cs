using UnityEngine;

namespace Props
{
    public class BoulderShadow : MonoBehaviour
    {
        // Start is called before the first frame update
        public void StopAllAnims()
        {
            GetComponent<Animator>().enabled = false;
        }

        public void EnableShadow()
        {
            GetComponent<SpriteRenderer>().enabled = true;
        }
    }
}