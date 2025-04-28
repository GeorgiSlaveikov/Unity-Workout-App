using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DayBlockMono : MonoBehaviour
{
    [SerializeField] private CalendarEnum.Days dayName;
    [SerializeField] private int dayNumber;
    [SerializeField] private int dayValue;
    [SerializeField] private TextMeshProUGUI dayNumberTextHolder;
    [SerializeField] private bool used = false;

    private Day dayClass;
    public TextMeshProUGUI GetNumberTextHolder() { return this.dayNumberTextHolder; }

    public CalendarEnum.Days GetDayName() { return this.dayName; }
    public void SetDayName(CalendarEnum.Days dayName) { this.dayName = dayName; }

    public int GetDayNumber() { return this.dayNumber; }
    public void SetDayNumber(int dayNumber) { this.dayNumber = dayNumber; }

    public int GetDayValue() { return this.dayValue; }
    public void SetDayValue(int dayValue) { this.dayValue = dayValue; }

    public Day GetDayClass() { return this.dayClass; }
    public void SetDayClass(Day dayClass) { this.dayClass = dayClass; }

    public bool GetUsedFalg() { return this.used; }
    public void SetUsedFlag(bool flag) { this.used = flag; }
}
