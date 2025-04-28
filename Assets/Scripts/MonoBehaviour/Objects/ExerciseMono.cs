using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class ExerciseMono : MonoBehaviour
{
    [SerializeField]
    private Exercise exercise;

    [SerializeField]
    private GameObject currentTargetExercise;

    [SerializeField] private List<GameObject> setBlocks = new List<GameObject>();

    [SerializeField]
    private TextMeshProUGUI exerciseNumberTextHolder;
    [SerializeField]
    private TextMeshProUGUI exerciseNameTextHolder;
    [SerializeField]
    private GameObject repGridHolder;
    [SerializeField]
    private GameObject contentHolder;

    private void Start()
    {
        currentTargetExercise = this.gameObject;
        RefreshLayoutGroup();
    }

    public TextMeshProUGUI GetExerciseNumberTextHolder() { return exerciseNumberTextHolder; }
    public TextMeshProUGUI GetExerciseNameTextHolder() {  return exerciseNameTextHolder; }
    public GameObject GetRepGridHolder() {  return repGridHolder; }
    public GameObject GetContentHolder() {  return contentHolder; }

    public void SetExerciseClass(Exercise exercise)
    {
        if (exercise != null)
        {
            this.exercise = exercise;
        }
        else
        {
            Debug.Log("exercise was null (incorrect data input)!");
        }
    }

    public Exercise GetExerciseClass() { return exercise; }

    public void AddSetBlock(GameObject setBlock)
    {
        if (setBlock != null)
        {
            setBlocks.Add(setBlock);
        }
        else { Debug.Log("setBlock was null (incorrect data input)!"); }
    }

    public void RemoveSetBlock()
    {
        if (setBlocks.Count > 0)
        {
            GameObject setToRemove = setBlocks.Last();
            setBlocks.Remove(setToRemove);
            Destroy(setToRemove);
        }
        else
        {
            Debug.Log("no elements in the list");
        }
    }

    public List<GameObject> GetSetBlocks() { return setBlocks; }

    public GameObject GetCurrentTargetExercise(GameObject exercise) { return currentTargetExercise; }


    public void RefreshLayoutGroup()
    {
        GameObject content = currentTargetExercise;
        LayoutRebuilder.ForceRebuildLayoutImmediate(content
            .GetComponent<VerticalLayoutGroup>().GetComponent<RectTransform>());
    }

    private bool flag = false;
    public void OpenCloseSetPanelTab(GameObject panel)
    {
        ExerciseCrudController exerciseCrudController;
        if (FindObjectOfType<ExerciseCrudController>() != null) {
            exerciseCrudController = FindObjectOfType<ExerciseCrudController>();

            if (!flag)
            {
                panel.SetActive(true);
                exerciseCrudController.RefreshLayoutGroup();
                exerciseCrudController.RefreshAgain();

                flag = true;
            }
            else if (flag)
            {
                panel.SetActive(false);
                exerciseCrudController.RefreshLayoutGroup();
                SLS.Save();

                flag = false;
            }
        }
        else { Debug.Log("Exercise crud controller does not exist!"); }
    }

}
