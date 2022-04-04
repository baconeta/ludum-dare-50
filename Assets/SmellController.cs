using UnityEngine;
using Props;

public class SmellController : MonoBehaviour
{

    private AudioSource _squawk;
    private GameObject smellObject;

    private void Start()
    {
        _squawk =  GetComponent<AudioSource>();
    }
    
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (!GetComponentInParent<Dodo>().isEating())
        {
            if (col.tag is "Smell")
            {
                smellObject = col.gameObject;
                if (!_squawk.isPlaying)
                {
                    _squawk.Play();
                }
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag is "Smell" && other.gameObject == smellObject)
        {
            smellObject.GetComponent<PlayerInteractable>().DodoInteract(false);
            smellObject = null;
        }
    }

    public GameObject getSmelledObject()
    {
        return smellObject;
    }
}
