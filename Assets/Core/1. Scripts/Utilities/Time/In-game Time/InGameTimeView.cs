using UnityEngine;
using TMPro;

public class InGameTimeView : MonoBehaviour
{
    [SerializeField] private TMP_Text timeText;

    private void Update()
    {
        DisplayTime();
    }

    private void DisplayTime()
    {
        string hour = InGameTime.Hour.ToString();
        if (hour.Length == 1)
        {
            hour = "0" + hour;
        }
        string minute = InGameTime.Minute.ToString();
        if (minute.Length == 1)
        {
            minute = "0" + minute;
        }
        timeText.text = $"Day {InGameTime.Day}, {hour}:{minute}";
    }
}
