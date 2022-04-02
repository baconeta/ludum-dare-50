using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public Text TimerValue;
    private StatsController _statsController;

    private void Start()
    {
        _statsController = FindObjectOfType<StatsController>();
    }

    private void Update()
    {
        TimerValue.text = _statsController.GetFormattedTime();
    }
}
