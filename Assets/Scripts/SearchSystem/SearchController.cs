using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using TMPro;

public class SearchController : MonoBehaviour
{
    private bool searchFlag = false;

    [SerializeField] private List<GameObject> workoutBlocks;
    [SerializeField] private TMP_InputField searchBarInputText;

    private App appController;

    private string searchData;

    void Start()
    {
        workoutBlocks = new List<GameObject>();

        if (FindObjectOfType<App>() != null) 
        { 
            this.appController = FindObjectOfType<App>();
        }
    }

    private void SearchEngine() 
    {
        searchData = searchBarInputText.text;
        print(searchData);
        int searchDataLength = searchData.Length;

        int searchedElements = 0;

        print(workoutBlocks.Count);
        foreach (GameObject element in workoutBlocks) 
        {
            searchedElements++;

            if (element.GetComponent<WorkoutMono>().GetWorkoutClass() != null && searchDataLength != 0)
            {
                if (element.GetComponent<WorkoutMono>().GetWorkoutClass().WorkoutName.Length >= searchDataLength)
                {
                    if (searchData.ToLower() == element.GetComponent<WorkoutMono>().GetWorkoutClass().WorkoutName.Substring(0, searchDataLength))
                    {
                        element.SetActive(true);
                    }
                    else
                    {
                        element.SetActive(false);
                    }
                }
            }
            else { SetScrollMenuActivity(false); }
        }
    }

    private void SetScrollMenuActivity(bool flag)
    {
        foreach (GameObject element in workoutBlocks)
        {
            element.SetActive(flag);
        }
    }

    private void GetWorkoutBlockList() 
    {
        workoutBlocks = appController.GetWorkoutBlocksList();
    }

    public void InputSearchDataIntextField() 
    {
        //if (!this.searchFlag)
        //{
        //    this.searchFlag = true;
        //    GetWorkoutBlockList();
        //    SearchEngine();
        //}

        print("search");
        GetWorkoutBlockList();
        SearchEngine();
    }

    public void SelectTextInputField()
    {
        print("select");
        GetWorkoutBlockList();
        SetScrollMenuActivity(false);
    }

    public void DisselectTextInputField() 
    {
        print("disselect");
        //if (this.searchFlag)
        //{
        //    SetScrollMenuActivity(true);
        //    workoutBlocks = new List<GameObject>();
        //    searchBarInputText.text = "";
        //    this.searchFlag = false;
        //}
        SetScrollMenuActivity(true);
        workoutBlocks = new List<GameObject>();
        searchBarInputText.text = "";
    }
}
