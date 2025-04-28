using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldMono : MonoBehaviour
{
    SetCrudController setCrudController;
    SearchController searchController;
    private bool flag = true;
    private void Start()
    {
        if (FindObjectOfType<SetCrudController>() != null)
        {
            setCrudController = FindObjectOfType<SetCrudController>();
        }
        if (FindObjectOfType<SearchController>() != null)
        {
            searchController = FindObjectOfType<SearchController>();
        }
    }
    public void UpdateSetValue(GameObject targetSet)
    {
        if (targetSet != null && flag == true)
        {
            setCrudController.SetSetRepValue(targetSet);
            setCrudController.SetSetIntensityValue(targetSet);
        }
    }

    public void SetBoolFlagState(bool state) { flag = state; }

    public void ActivateSearch() 
    {
        print("on value change");
        searchController.InputSearchDataIntextField();
    }

    public void SelectSearchBar() { searchController.SelectTextInputField(); }

    public void DeactivateSearch()
    {
        searchController.DisselectTextInputField();
    }
}
