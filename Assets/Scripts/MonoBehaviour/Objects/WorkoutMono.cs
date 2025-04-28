using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WorkoutMono : MonoBehaviour
{
    [SerializeField]
    private Workout workout;

    [SerializeField]
    private GameObject workoutManagerPanel;

    [SerializeField]
    private TextMeshProUGUI workoutNameTextHolder;
    [SerializeField]
    private TextMeshProUGUI workoutDateTextHolder;
    [SerializeField]
    private TextMeshProUGUI workoutExerciseCountTextHolder;

    public TextMeshProUGUI GetWorkoutNameTextHolder() { return workoutNameTextHolder; }
    public TextMeshProUGUI GetWorkoutDateTextHolder() { return workoutDateTextHolder; }
    public TextMeshProUGUI GetWorkoutExerciseCountTextHolder() { return workoutExerciseCountTextHolder; }

    public void SetWorkoutManagerPanel(GameObject panel) 
    {
        if (panel != null)
        {
            this.workoutManagerPanel = panel;
        }
        else {
            Debug.Log("panel was null (incorrect data input)!");
        }
    }

    public GameObject GetWorkoutManagerPanel() {  return workoutManagerPanel; }

    public void SetWorkoutClass(Workout workout)
    {
        if (workout != null)
        {
            this.workout = workout;
        }
        else
        {
            Debug.Log("workout was null (incorrect data input)!");
        }
    }

    public Workout GetWorkoutClass() { return workout; }

}
