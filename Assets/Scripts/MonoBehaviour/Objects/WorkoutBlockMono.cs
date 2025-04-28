using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WorkoutBlockMono : MonoBehaviour
{
    [SerializeField]
    private GameObject workoutManagerPanel;

    [SerializeField]
    private List<GameObject> exerciseBlocks = new List<GameObject>();

    [SerializeField] private GameObject createExercisePanelHolder;
    [SerializeField] private TMP_InputField exerciseNameInputTextHolder;
    [SerializeField] private TextMeshProUGUI workoutNameTextHolder;
    [SerializeField] private TextMeshProUGUI workoutDateTextHolder;
    [SerializeField] private GameObject workoutScrollPanelHolder;

    private void Start()
    {
        RefreshLayoutGroup();
    }

    public TextMeshProUGUI GetWorkoutNameTextHolder() { return workoutNameTextHolder; }
    public TextMeshProUGUI GetWorkoutDateTextHolder() { return workoutDateTextHolder; }
    public GameObject GetWorkoutScrollPanelHolder() { return workoutScrollPanelHolder; }
    public GameObject GetCreateExercisePanelHolder() { return createExercisePanelHolder; }
    public TMP_InputField GetExerciseNameInputTextHolder() { return exerciseNameInputTextHolder;  }

    public void SetWorkoutManagerPanel(GameObject panel)
    {
        if (panel != null)
        {
            this.workoutManagerPanel = panel;
        }
        else
        {
            Debug.Log("panel was null (incorrect data input)!");
        }
    }

    public GameObject GetWorkoutManagerPanel() { return workoutManagerPanel; }

    public void AddExerciseBlock(GameObject exerciseBlock) {
        print("added exercise BLOCK to the list!");
        if (exerciseBlock != null)
        {
            exerciseBlocks.Add(exerciseBlock);
            print("added");
        }
        else { Debug.Log("exercise block was null!"); }
    }

    public void RemoveExerciseBlock() 
    {
        if (exerciseBlocks.Count >= 1)
        {
            GameObject exerciseToRemove = exerciseBlocks.Last();
            exerciseBlocks.Remove(exerciseToRemove);
            Destroy(exerciseToRemove);
        }
        else
        {
            Debug.Log("no elements in the list");
        }
    }

    public void RefreshLayoutGroup()
    {
        LayoutRebuilder.ForceRebuildLayoutImmediate(workoutScrollPanelHolder.transform
            .GetChild(0).GetChild(0).GetChild(0)
            .GetComponent<VerticalLayoutGroup>().GetComponent<RectTransform>());

    }
}
