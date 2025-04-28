using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using TMPro;
using UnityEngine.UIElements;
using UnityEngine.UI;
using Image = UnityEngine.UIElements.Image;
using System;

public class CalendarCrudController : MonoBehaviour
{
    private const int currentYearCONST = 2024;
    DateTime currentDateTime = DateTime.Now;
    
    [SerializeField] private List<Year> years = new List<Year>();
    private Year currentYear;

    private DayBlockMono dayBlockMono;

    [SerializeField] private List<GameObject> calendarBlocksGrid = new List<GameObject>();
    [SerializeField] private GameObject dayBlockPrefab;
    [SerializeField] private GameObject contentGrid;
    [SerializeField] private TextMeshProUGUI monthYearTextHolder;
    [SerializeField] private TextMeshProUGUI selectedDateTextHolder;

    [SerializeField] private Sprite selectedDayBlockSprite;
    [SerializeField] private Sprite orignalDayBlockSprite;

    [SerializeField] private Color selectedDayBlockColor;
    [SerializeField] private Color orignalDayBlockCrolor;

    [SerializeField] private CalendarEnum.Months currentMonthName;
    [SerializeField] private CalendarEnum.Days currentDayName;
    private int currentMonthDaysCount;
    private int currentMonthNumber;

    private string selectedDateString;
    private string selectedMonthName;
    private string selectedDayName;
    private int selectedDayNumber;

    private List<GameObject> currentMonthDaysToPlace = new List<GameObject>();
    private List<GameObject> extraDays = new List<GameObject>();
    [SerializeField] private List<GameObject> notUsedDayBlocks = new List<GameObject>();

    private int currnetDayBlockValue;

    private void Start()
    {
        StartUp();
        //GetAtomaticalyDateData();
        GenerateYearMonths(currentYear);
        GenerateGrid();
        Fill(GetMonthNumber(currentMonthName));
    }
    private void StartUp() 
    {
        currentMonthNumber = GetMonthNumber(currentMonthName);
        Year year = new Year(2024);
        years.Add(year);
        currentYear = years.FirstOrDefault();
    }
    private void AddYear(Year yearToAdd) 
    {
        if (yearToAdd != null) 
        {  
            years.Add(yearToAdd);
        }
    }

    private void RemoveYear(Year yearToRemove)
    {
        if (yearToRemove != null && years.Contains(yearToRemove))
        {
            years.Remove(yearToRemove);
        }
    }

    private void GenerateYearMonths(Year selectedYear) 
    { 
        for (int i = 1; i <= 12; i++) 
        { 
            Month month = new Month(i, GetMonthName(i));
            month.SetStarterDay(currentDayName);
            if (!selectedYear.GetMonthsList().Contains(month))
            {
                selectedYear.AddMonth(month);
            }
        }
    }

    private void GenerateGrid()
    {
        GameObject dayBlock;
        for (int i = 0; i < 35; i++)
        {
            int staticValue = i + 1;
            dayBlock = Instantiate(dayBlockPrefab);
            dayBlock.transform.SetParent(contentGrid.transform);

            CalendarEnum.Days staticDayName = GetBlockName(staticValue);
            if (dayBlock.GetComponent<DayBlockMono>() != null)
            {
                dayBlockMono = dayBlock.GetComponent<DayBlockMono>();
                dayBlockMono.SetDayName(staticDayName);
                dayBlockMono.SetDayValue(staticValue);
                dayBlockMono.SetDayNumber(0);
            }
            
            calendarBlocksGrid.Add(dayBlock);
        }
    }

    private void GenrateCurrentMonth(Month selectedMonth)
    {
        Month usedMonth = currentYear.GetMonthsList().Find(m => m == selectedMonth);
        if (selectedMonth == null) { Debug.Log("Input data is not correct!"); }

        GameObject staticDayBlock = null;
        if (calendarBlocksGrid.Find(x => x.GetComponent<DayBlockMono>().GetDayName() == usedMonth.GetStarterDay()) != null)
        {
            staticDayBlock = calendarBlocksGrid.Find(x => x.GetComponent<DayBlockMono>().GetDayName() == usedMonth.GetStarterDay());
        }
        else { Debug.Log("No such thing!"); }

        if (calendarBlocksGrid.Where(x => x.GetComponent<DayBlockMono>().GetDayValue()
            >= staticDayBlock.GetComponent<DayBlockMono>().GetDayValue()).ToList().Count > 0 &&
            calendarBlocksGrid.Where(x => x.GetComponent<DayBlockMono>().GetDayValue()
            >= staticDayBlock.GetComponent<DayBlockMono>().GetDayValue()).ToList() != null)
        {
            currentMonthDaysToPlace = calendarBlocksGrid.Where(x => x.GetComponent<DayBlockMono>().GetDayValue()
                >= staticDayBlock.GetComponent<DayBlockMono>().GetDayValue()).ToList();
        }

        GameObject dayBlock;
        currentMonthDaysCount = GetMonthDaysCount(usedMonth);
        int count = currentMonthDaysCount;
        if (currentMonthDaysToPlace.Count < currentMonthDaysCount)
        {
            int temp = currentMonthDaysCount - currentMonthDaysToPlace.Count;
            for (int i = 35; i < 35 + temp; i++)
            {
                dayBlock = Instantiate(dayBlockPrefab);
                dayBlock.transform.SetParent(contentGrid.transform);

                int staticValue = i + 1;
                CalendarEnum.Days staticDayName = GetBlockName(staticValue);
                if (dayBlock.GetComponent<DayBlockMono>() != null)
                {
                    dayBlockMono = dayBlock.GetComponent<DayBlockMono>();
                    dayBlockMono.SetDayName(staticDayName);
                    dayBlockMono.SetDayValue(staticValue);
                }

                currentMonthDaysToPlace.Add(dayBlock);
                extraDays.Add(dayBlock);
            }
        }

        for (int i = 1; i <= count; i++)
        {
            currentMonthDaysToPlace[i - 1].GetComponent<DayBlockMono>().GetNumberTextHolder().text = i.ToString();
            currentMonthDaysToPlace[i - 1].GetComponent<DayBlockMono>().SetDayNumber(i);
            currentMonthDaysToPlace[i - 1].GetComponent<DayBlockMono>().SetUsedFlag(true);

            Day day = new Day(i, currentMonthDaysToPlace[i - 1].GetComponent<DayBlockMono>().GetDayName());
            day.SetStaticValue(currentMonthDaysToPlace[i - 1].GetComponent<DayBlockMono>().GetDayValue());

            usedMonth.AddDayToMonth(day);

            currnetDayBlockValue = currentMonthDaysToPlace[i - 1].GetComponent<DayBlockMono>().GetDayValue();
        }

        UpdateTextUI();
        ManageEmptyDayBlocks();
    }

    private void ManageEmptyDayBlocks() 
    { 
        var tempList = new List<GameObject>(); 
        foreach (GameObject go in calendarBlocksGrid) 
        {
            if (!go.GetComponent<DayBlockMono>().GetUsedFalg())
            {
                go.GetComponent<UnityEngine.UI.Image>().color = new Color(
                    go.GetComponent<UnityEngine.UI.Image>().color.r,
                    go.GetComponent<UnityEngine.UI.Image>().color.g, 
                    go.GetComponent<UnityEngine.UI.Image>().color.b, 0.5f); ;
            }
        }
    }
    private void Fill(int monthNumber) 
    {
        Month currentMonth = currentYear.GetMonthsList().Find(x => x.MonthNumber == monthNumber);
        GenrateCurrentMonth(currentMonth);
    }

    public void NextMonth() 
    {
        Month tempMonth;
        int nextMonthNumber = 0;
        if (currentMonthNumber + 1 <= 12)
        {
            tempMonth = currentYear.GetMonthsList().Find(m => m.MonthNumber == (currentMonthNumber + 1));
            currentDayName = GetDayName(currnetDayBlockValue + 1);
            currentMonthNumber++;
            currentMonthName = GetMonthName(currentMonthNumber);
            nextMonthNumber = currentMonthNumber;
            tempMonth.SetStarterDay(currentDayName);
        }
        else if (currentMonthNumber + 1 > 12)
        {
            Year newYear = new Year(currentYear.YearNumber+1);
            currentYear = newYear;
            AddYear(newYear);

            GenerateYearMonths(currentYear);
            tempMonth = currentYear.GetMonthsList().Find(m => m.MonthNumber == 1);
            currentDayName = GetDayName(currnetDayBlockValue + 1);
            currentMonthNumber = 1;
            currentMonthName = CalendarEnum.Months.January;
            nextMonthNumber = currentMonthNumber;
            tempMonth.SetStarterDay(currentDayName);
        }

        ClearGrid();
        Fill(nextMonthNumber);
    }

    public void PreviousMonth() 
    {
        Month tempMonth;
        int nextMonthNumber = 0;
        if (currentMonthNumber - 1 >= 1)
        {
            tempMonth = currentYear.GetMonthsList().Find(m => m.MonthNumber == (currentMonthNumber - 1));

            currentDayName = GetDayName(tempMonth.GetMonthDayList().First().GetStaticValue());
            currentMonthNumber--;
            currentMonthName = GetMonthName(currentMonthNumber);
            nextMonthNumber = currentMonthNumber;
            tempMonth.SetStarterDay(currentDayName);
        }
        else if (currentMonthNumber - 1 < 1) 
        {
            if (currentYear.YearNumber > currentYearCONST)
            {
                Year year = years.Find(x => x.YearNumber == (currentYear.YearNumber - 1));
                print(year.YearNumber);
                tempMonth = year.GetMonthsList().Find(m => m.MonthNumber == 12);

                currentDayName = GetDayName(tempMonth.GetMonthDayList().First().GetStaticValue());
                currentMonthNumber = 12;
                currentMonthName = GetMonthName(currentMonthNumber);
                nextMonthNumber = currentMonthNumber;
                currentYear = year;
                tempMonth.SetStarterDay(currentDayName);
            }
            else if (currentYear.YearNumber == currentYearCONST) { Debug.Log("cant go back in time that much!"); }
        }
        

        ClearGrid();
        Fill(nextMonthNumber);
    }

    public void Reciever(GameObject selectedBlock) 
    {
        DayBlockMono dayBlockMonoTemp = selectedBlock.GetComponent<DayBlockMono>();
        foreach (var item in calendarBlocksGrid)
        {
            if (item.GetComponent<DayBlockMono>() != null && item.GetComponent<DayBlockMono>().GetUsedFalg() == true)
            {
                item.GetComponent<UnityEngine.UI.Image>().sprite = orignalDayBlockSprite;
                item.GetComponent<UnityEngine.UI.Image>().color = orignalDayBlockCrolor;
            }
        }

        selectedBlock.GetComponent<UnityEngine.UI.Image>().sprite = selectedDayBlockSprite;
        selectedBlock.GetComponent<UnityEngine.UI.Image>().color = selectedDayBlockColor;

        
        selectedDayName = dayBlockMonoTemp.GetDayName().ToString();
        selectedMonthName = currentMonthName.ToString().Substring(0, 3);
        selectedDayNumber = dayBlockMonoTemp.GetDayNumber();
        int year = currentYear.YearNumber;

        selectedDateString = $"{selectedDayName}, {selectedMonthName} {selectedDayNumber}, {year}";
        selectedDateTextHolder.text = selectedDateString;
    }

    private void GetAtomaticalyDateData() 
    {
        int year = currentDateTime.Year;
        int month = currentDateTime.Month;
        int day = currentDateTime.Day;

        print($"{year} {month} {day}");
        string dayOfWeek = currentDateTime.DayOfWeek.ToString();

        currentYear.YearNumber = year;
        currentMonthName = GetMonthName(month);
        currentMonthNumber = month;

        DateTime date = new DateTime(year, month, 1);
        currentDayName = GetDayNameFromString(date.DayOfWeek.ToString());
    }

    private void UpdateTextUI() 
    {
        string dateOutput = $"{currentMonthName}-{currentYear.YearNumber}";
        monthYearTextHolder.text = dateOutput;
    }

    private CalendarEnum.Days GetDayNameFromString(string dayName) 
    {
        CalendarEnum.Days staticDayName = CalendarEnum.Days.None;
        if (dayName == "Monday") { staticDayName = CalendarEnum.Days.Monday; }
        if (dayName == "Tuesday") { staticDayName = CalendarEnum.Days.Tuesday; }
        if (dayName == "Wednesday") { staticDayName = CalendarEnum.Days.Wednesday; }
        if (dayName == "Thursday") { staticDayName = CalendarEnum.Days.Thursday; }
        if (dayName == "Firday") { staticDayName = CalendarEnum.Days.Friday; }
        if (dayName == "Saturday") { staticDayName = CalendarEnum.Days.Saturday; }
        if (dayName == "Sunday") { staticDayName = CalendarEnum.Days.Sunday; }
        return staticDayName;
    }

    private CalendarEnum.Days GetDayName(int staticValue)
    {
        CalendarEnum.Days staticDayName = CalendarEnum.Days.None;
        if (staticValue == 1 || staticValue == 8 || staticValue == 15 || staticValue == 22 || staticValue == 29 || staticValue == 36) { staticDayName = CalendarEnum.Days.Monday; }
        if (staticValue == 2 || staticValue == 9 || staticValue == 16 || staticValue == 23 || staticValue == 30 || staticValue == 37) { staticDayName = CalendarEnum.Days.Tuesday; }
        if (staticValue == 3 || staticValue == 10 || staticValue == 17 || staticValue == 24 || staticValue == 31 || staticValue == 38) { staticDayName = CalendarEnum.Days.Wednesday; }
        if (staticValue == 4 || staticValue == 11 || staticValue == 18 || staticValue == 25 || staticValue == 32) { staticDayName = CalendarEnum.Days.Thursday; }
        if (staticValue == 5 || staticValue == 12 || staticValue == 19 || staticValue == 26 || staticValue == 33) { staticDayName = CalendarEnum.Days.Friday; }
        if (staticValue == 6 || staticValue == 13 || staticValue == 20 || staticValue == 27 || staticValue == 34) { staticDayName = CalendarEnum.Days.Saturday; }
        if (staticValue == 7 || staticValue == 14 || staticValue == 21 || staticValue == 28 || staticValue == 35) { staticDayName = CalendarEnum.Days.Sunday; }

        return staticDayName;
    }
    private CalendarEnum.Days GetBlockName(int staticValue)
    {
        CalendarEnum.Days staticDayName = CalendarEnum.Days.Monday;
        if (staticValue == 1 || staticValue == 8 || staticValue == 15 || staticValue == 22 || staticValue == 29 || staticValue == 36) { staticDayName = CalendarEnum.Days.Monday; }
        if (staticValue == 2 || staticValue == 9 || staticValue == 16 || staticValue == 23 || staticValue == 30 || staticValue == 37) { staticDayName = CalendarEnum.Days.Tuesday; }
        if (staticValue == 3 || staticValue == 10 || staticValue == 17 || staticValue == 24 || staticValue == 31 || staticValue == 38) { staticDayName = CalendarEnum.Days.Wednesday; }
        if (staticValue == 4 || staticValue == 11 || staticValue == 18 || staticValue == 25 || staticValue == 32) { staticDayName = CalendarEnum.Days.Thursday; }
        if (staticValue == 5 || staticValue == 12 || staticValue == 19 || staticValue == 26 || staticValue == 33) { staticDayName = CalendarEnum.Days.Friday; }
        if (staticValue == 6 || staticValue == 13 || staticValue == 20 || staticValue == 27 || staticValue == 34) { staticDayName = CalendarEnum.Days.Saturday; }
        if (staticValue == 7 || staticValue == 14 || staticValue == 21 || staticValue == 28 || staticValue == 35) { staticDayName = CalendarEnum.Days.Sunday; }

        return staticDayName;
    }

    public int GetMonthNumber(CalendarEnum.Months month)
    {
        if (month == CalendarEnum.Months.January) return 1;
        if (month == CalendarEnum.Months.February) return 2;
        if (month == CalendarEnum.Months.March) return 3;
        if (month == CalendarEnum.Months.April) return 4;
        if (month == CalendarEnum.Months.May) return 5;
        if (month == CalendarEnum.Months.June) return 6;
        if (month == CalendarEnum.Months.July) return 7;
        if (month == CalendarEnum.Months.August) return 8;
        if (month == CalendarEnum.Months.September) return 9;
        if (month == CalendarEnum.Months.October) return 10;
        if (month == CalendarEnum.Months.November) return 11;
        if (month == CalendarEnum.Months.December) return 12;
        return 0;
    }

    public CalendarEnum.Months GetMonthName(int month)
    {
        if (month == 1) return CalendarEnum.Months.January;
        if (month == 2) return CalendarEnum.Months.February;
        if (month == 3) return CalendarEnum.Months.March;
        if (month == 4) return CalendarEnum.Months.April;
        if (month == 5) return CalendarEnum.Months.May;
        if (month == 6) return CalendarEnum.Months.June;
        if (month == 7) return CalendarEnum.Months.July;
        if (month == 8) return CalendarEnum.Months.August;
        if (month == 9) return CalendarEnum.Months.September;
        if (month == 10) return CalendarEnum.Months.October;
        if (month == 11) return CalendarEnum.Months.November;
        if (month == 12) return CalendarEnum.Months.December;
        return CalendarEnum.Months.None;
    }

    public int GetMonthDaysCount(Month month)
    {
        int monthNumber = month.MonthNumber;

        if (monthNumber == 1 || monthNumber == 3 || monthNumber == 5
            || monthNumber == 7 || monthNumber == 8 || monthNumber == 10 || monthNumber == 12)
        {
            return 31;
        }
        else if (monthNumber == 4 || monthNumber == 6
            || monthNumber == 9 || monthNumber == 11)
        {
            return 30;
        }
        else if (monthNumber == 2)
        {
            if (currentYear.YearNumber % 4 == 0) { return 29; }
            return 28;
        }
        return 0;
    }

    private void ClearGrid()
    {
        currentMonthDaysToPlace = new List<GameObject>();

        notUsedDayBlocks = new List<GameObject>();

        foreach (var item in calendarBlocksGrid)
        {
            item.GetComponent<DayBlockMono>().GetNumberTextHolder().text = "";
            item.GetComponent<DayBlockMono>().SetDayNumber(0);
            item.GetComponent<UnityEngine.UI.Image>().sprite = orignalDayBlockSprite;
            item.GetComponent<UnityEngine.UI.Image>().color = orignalDayBlockCrolor;
            item.GetComponent<DayBlockMono>().SetUsedFlag(false);
        }

        foreach (var item in extraDays)
        {
            Destroy(item);
        }
        extraDays = new List<GameObject>();
    }

    public string GetFinalDateString() { return this.selectedDateString; }
    public int GetYear() { return this.currentYear.YearNumber; }
    public string GetMonth() { return this.selectedMonthName; }
    public string GetDay() { return this.selectedDayName; }
    public int GetDayNumber() { return this.selectedDayNumber; }


    private void Print() 
    {
        foreach (var year in years)
        {
            print(year.YearNumber);
            var months = year.GetMonthsList();
            foreach (var month in months)
            {
                print(month.MonthName);
                var days = month.GetMonthDayList();
                foreach (var day in days)
                {
                    print($"{day.DayName}-{day.DayNumber}-{month.MonthName}-{month.MonthNumber}-{year.YearNumber}");
                }
            }
        }
    }
}
