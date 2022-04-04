using Controllers;
using UnityEngine;
using UnityEngine.UI;

public class DodoFacts : MonoBehaviour
{
    [SerializeField] private Sprite[] imageFacts;

    // Start is called before the first frame update
    public void ChooseImage()
    {
        GetComponent<Image>().sprite = imageFacts.ChooseRandom();
    }
}
