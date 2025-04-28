using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Day
{
    private int staticValue;
    private int dayNumber;
    private CalendarEnum.Days dayName;

    public Day(int dayNumber, CalendarEnum.Days dayName)
    {
        this.dayNumber = dayNumber;
        this.dayName = dayName;
    }

    public Day() { }

    public CalendarEnum.Days DayName
    {
        get { return dayName; }
        set { dayName = value; }
    }

    public int DayNumber
    {
        get { return dayNumber; }
        set { dayNumber = value; }
    }

    public void SetStaticValue(int value)
    {
        this.staticValue = value;
    }

    public int GetStaticValue() { return this.staticValue; }
}
