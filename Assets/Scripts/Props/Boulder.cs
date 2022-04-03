using UnityEngine;

namespace Props
{
    public class Boulder : MonoBehaviour
    {
        [SerializeField] private Sprite[] possibleSprites;
        private SpriteRenderer _sr;

        // Start is called before the first frame update
        private void Start()
        {
            // Choose a sprite from all possible sprites on this object
            _sr = GetComponent<SpriteRenderer>();
            if (_sr == default)
            {
                Debug.Log("No sprite rendered on " + name + " object.");
            }
            else
            {
                if (possibleSprites.Length == 0)
                {
                    Debug.Log("Add possible sprites to boulder object.");
                }

                _sr.sprite = possibleSprites.ChooseRandom();
            }
        }

        public void StopAllAnims()
        {
            GetComponent<Animator>().enabled = false;
        }
    }
}