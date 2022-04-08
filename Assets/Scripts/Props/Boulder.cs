using UnityEngine;

namespace Props
{
    public class Boulder : MonoBehaviour
    {
        [SerializeField] private Sprite[] possibleSprites;
        private AudioSource[] _boulderSounds;
        private SpriteRenderer _sr;
        private Collider2D _collider;

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

            _boulderSounds = GetComponents<AudioSource>();
            _collider = GetComponent<Collider2D>();
        }

        public void StopAllAnims()
        {
            GetComponent<Animator>().enabled = false;
            _collider.enabled = true;
        }

        public void DropBoulder()
        {
            _boulderSounds.ChooseRandom().Play();
        }
    }
}