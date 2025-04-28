using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Year
{
    private List<Month> months = new List<Month>();

	private int yearNumber;

    public Year(int yearNumber)
    {
        this.yearNumber = yearNumber;
    }

    public Year() { }

    public int YearNumber
	{
		get { return yearNumber; }
		set { yearNumber = value; }
	}

    public void AddMonth(Month month) 
    {
        if (month != null && !months.Contains(month) && months.Count + 1 <= 12)
        {
            months.Add(month);
        }
        else { Debug.Log("The year is full! See you next year!"); }
    }

    public List<Month> GetMonthsList() { return this.months; }
}
