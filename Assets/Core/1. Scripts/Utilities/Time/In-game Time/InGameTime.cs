using System;
using UnityEngine;

public class InGameTime : MonoBehaviour
{
    //[SerializeField] private float durationOfOneInGameYear;
    //[SerializeField] private float durationOfOneInGameDay;
    [SerializeField] private float durationOfOneInGameHour = 25;
    [SerializeField] private float durationOfOneInGameMinute;

    [SerializeField] private int _initialNumberOfDaysInYear = 2;
    private static int _numberOfDaysInYear;
    public static int NumberOfDaysInYear = 10;

    [SerializeField] private static int _year;
    [SerializeField] private static int _dayInThisYear;
    [SerializeField] private static int _day;
    [SerializeField] private static int _hour = 6;
    [SerializeField] private static int _minute;

    private float counter = 0;

    //private static Action<InGameTimeDate> ;
    public static Action OnYearChange;
    public static Action OnDayChange;
    public static Action OnDaysInYearChange;
    public static Action OnHourChange;
    public static Action OnMinuteChange;

    private void Start()
    {
        durationOfOneInGameMinute = durationOfOneInGameHour / 60;
        _numberOfDaysInYear = _initialNumberOfDaysInYear;
    }

    public static int Year
    {
        get
        {
            return _day;
        }
        set
        {
            _day = value;
            OnYearChange?.Invoke();
        }
    }

    public static int DayInThisYear
    {
        get
        {
            return _dayInThisYear;
        }
        set
        {
            _dayInThisYear = value;

            if (_dayInThisYear >= _numberOfDaysInYear)
            {
                Year++;
                _dayInThisYear -= _numberOfDaysInYear;
            }

            OnDaysInYearChange?.Invoke();
        }
    }

    public static int Day
    {
        get
        {
            return _day;
        }
        set
        {
            DayInThisYear += value - _day;
            _day = value;

            OnDayChange?.Invoke();
        }
    }

    public static int Hour
    {
        get
        {
            return _hour;
        }
        set
        {
            _hour = value;
            if (_hour >= 24)
            {
                Day++;
                _hour -= 24;
            }
            OnHourChange?.Invoke();
        }
    }

    public static int Minute
    {
        get
        {
            return _minute;
        }
        set
        {
            _minute = value;
            if (_minute >= 60)
            {
                Hour++;
                _minute -= 60;
            }
            OnMinuteChange?.Invoke();
        }
    }

    private void Update()
    {
        counter += Time.deltaTime * InGameSpeed.Speed;
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
