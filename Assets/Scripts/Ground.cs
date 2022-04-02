using UnityEngine;

public class Ground : MonoBehaviour
{
    private WorldController _worldController;

    // Start is called before the first frame update
    private void Start()
    {
        _worldController = FindObjectOfType<WorldController>();
    }

    public WorldController GetWorldCollider()
    {
        return _worldController;
    }
}