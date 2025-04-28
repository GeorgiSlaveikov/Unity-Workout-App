using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Month
{
    private List<Day> daysInMonth = new List<Day>();

    private CalendarEnum.Days starterDay;
    private CalendarEnum.Months monthName;
    private int monthNumber;

    public Month(int monthNumber, CalendarEnum.Months monthName)
    {
        MonthNumber = monthNumber;
        MonthName = monthName;
    }

    public Month() { }

    public int MonthNumber
    {
        get { return monthNumber; }
        set { monthNumber = value; }
    }

    public CalendarEnum.Months MonthName
    {
        get { return monthName; }
        set { monthName = value; }
    }

    public void AddDayToMonth(Day dayToAdd)
    {
        if (dayToAdd != null)
        {
            if (!daysInMonth.Contains(dayToAdd))
            {
                daysInMonth.Add(dayToAdd);
            }
            else { Debug.Log("This month already contains this day! Try something different!)"); }
        }
        else { Debug.Log("Incorrect data input!"); }
    }

    public List<Day> GetMonthDayList()
    {
        return this.daysInMonth;
    }

    public void SetStarterDay(CalendarEnum.Days day) { this.starterDay = day; }

    public CalendarEnum.Days GetStarterDay() { return this.starterDay; }
}
