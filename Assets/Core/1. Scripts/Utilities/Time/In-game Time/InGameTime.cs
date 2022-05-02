using System;
using UnityEngine;

public class InGameTime : MonoBehaviour
{
    [SerializeField] private float durationOfOneInGameDay;
    [SerializeField] private float durationOfOneInGameHour;
    [SerializeField] private float durationOfOneInGameMinute;

    [SerializeField] private static int day;
    [SerializeField] private static int hour = 6;
    [SerializeField] private static int minute;

    private float counter = 0;

    //private static Action<InGameTimeDate> ;
    public static Action OnDayChange;
    public static Action OnHourChange;
    public static Action OnMinuteChange;

    public static int Day
    {
        get
        {
            return day;
        }
        set
        {
            day = value;
            OnDayChange?.Invoke();
        }
    }

    public static int Hour
    {
        get
        {
            return hour;
        }
        set
        {
            hour = value;
            if (hour >= 24)
            {
                Day++;
                hour -= 24;
            }
            OnHourChange?.Invoke();
        }
    }
    public static int Minute
    {
        get
        {
            return minute;
        }
        set
        {
            minute = value;
            if (minute >= 60)
            {
                Hour++;
                minute -= 60;
            }
            OnMinuteChange?.Invoke();
        }
    }


    private void Start()
    {
        durationOfOneInGameMinute = durationOfOneInGameHour / 60;
        durationOfOneInGameDay = durationOfOneInGameHour * 24;
    }

    private void Update()
    {
        counter += Time.deltaTime;
        CheckCounter();
    }

    private void CheckCounter()
    {
        if (counter >= durationOfOneInGameMinute)
        {
            Minute++;
            counter -= durationOfOneInGameMinute;
            CheckCounter();
        }
    }
}
[Serializable]
public struct InGameTimeDate
{
    public int day;
    public int hour;
    public int minute;

    public InGameTimeDate(int day, int hour, int minute)
    {
        this.day = day;
        this.hour = hour;
        this.minute = minute;
    }
}
