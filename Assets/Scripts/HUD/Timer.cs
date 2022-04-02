using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public Text TimerValue;
    private StatsManager _statsManager;

    private void Start()
    {
        _statsManager = FindObjectOfType<StatsManager>();
    }

    private void Update()
    {
        TimerValue.text = _statsManager.GetFormattedTime();
    }
}
