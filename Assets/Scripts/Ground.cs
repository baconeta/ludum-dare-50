using UnityEngine;

public class Ground : MonoBehaviour
{
    private WorldController _worldController;
    [SerializeField] private Sprite[] possibleGroundPieces;

    // Start is called before the first frame update
    private void Start()
    {
        _worldController = FindObjectOfType<WorldController>();
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        sr.sprite = possibleGroundPieces[Random.Range(0, possibleGroundPieces.Length)];
    }

    public WorldController GetWorldCollider()
    {
        return _worldController;
    }
}