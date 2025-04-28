using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;
using System.Globalization;
using System.Linq;
using UnityEngine.UI;

public class ExerciseCrudController : MonoBehaviour
{
    private GameObject exerciseBlockPrefab;
    private GameObject scrollMenuContent;
    private GameObject currentTargetWorkout;

    private MainController mainController;
    private WorkoutMono workoutMono;
    private WorkoutBlockMono workoutBlockMono;

    private TextMeshProUGUI exerciseNumberTextHolder;
    private TextMeshProUGUI exerciseNameTextHolder;

    private TMP_InputField exerciseNameInputTextHolder;

    private ExerciseMono exerciseMono;

    private void Start()
    {
        InitializeProperty();
    }

    private void InitializeProperty()
    {
        if (FindObjectOfType<MainController>() != null) { mainController = FindObjectOfType<MainController>(); }
        else { Debug.Log("main controller was not found!"); }
        if (HandlerClass.GetExerciseBlockPrefab() != null) { exerciseBlockPrefab = HandlerClass.GetExerciseBlockPrefab(); }
        else { Debug.Log("exercise block prefab was not found!"); }
    }
    public void AddExercise()
    {
        if (mainController.GetCurrentTargetWorkout() != null)
        {
            currentTargetWorkout = mainController.GetCurrentTargetWorkout();
            if (currentTargetWorkout.GetComponent<WorkoutMono>() != null)
            {
                workoutMono = currentTargetWorkout.GetComponent<WorkoutMono>();
                GameObject currentTargetWorkoutBlockPanelManager;
                if (workoutMono.GetWorkoutManagerPanel() != null)
                {
                    currentTargetWorkoutBlockPanelManager = workoutMono.GetWorkoutManagerPanel();
                    if (currentTargetWorkoutBlockPanelManager.GetComponent<WorkoutBlockMono>() != null)
                    {
                        workoutBlockMono = currentTargetWorkoutBlockPanelManager.GetComponent<WorkoutBlockMono>();
                        scrollMenuContent = workoutBlockMono.GetWorkoutScrollPanelHolder().transform.GetChild(0).GetChild(0).GetChild(0).gameObject;
                    }
                    else {
                        Debug.Log("workout block mono was not found!");
                    }
                }
                else {
                    Debug.Log("workout manager panel was not found!");
                }
            }
            else
            {
                Debug.Log("workout mono was not found!");
            }
        }
        else
        {
            Debug.Log("exercise mono was not found!");
        }

        GameObject execisePhysicleObject = Instantiate(exerciseBlockPrefab);
        execisePhysicleObject.transform.SetParent(scrollMenuContent.transform);
        currentTargetWorkout.GetComponent<WorkoutMono>().GetWorkoutManagerPanel().GetComponent<WorkoutBlockMono>().AddExerciseBlock(execisePhysicleObject);

        if (execisePhysicleObject.GetComponent<ExerciseMono>() != null) {
            exerciseMono = execisePhysicleObject.GetComponent<ExerciseMono>();

            exerciseNumberTextHolder = exerciseMono.GetExerciseNumberTextHolder();
            exerciseNameTextHolder = exerciseMono.GetExerciseNameTextHolder();
            exerciseNameInputTextHolder = currentTargetWorkout.GetComponent<WorkoutMono>().GetWorkoutManagerPanel()
                .GetComponent<WorkoutBlockMono>().GetExerciseNameInputTextHolder();
        }
        else {
            Debug.Log("exercise mono was no found!");
        }

        string exerciseName = exerciseNameInputTextHolder.text;
        exerciseNameTextHolder.text = exerciseName;

        Workout targetWorkoutClass = workoutMono.GetWorkoutClass();
        Exercise exercise = new Exercise(exerciseName);

        exerciseMono.SetExerciseClass(exercise);
        targetWorkoutClass.AddExercise(exercise);
        workoutBlockMono.AddExerciseBlock(execisePhysicleObject);

        string exerciseNumber = targetWorkoutClass.GetExerciseCount().ToString();
        exerciseNumberTextHolder.text = exerciseNumber;
        exercise.SetExerciseNumber(int.Parse(exerciseNumber));

        exerciseNameInputTextHolder.text = "";

        SLS.Save();
    }

    public void RemoveExercise()
    {
        if (mainController.GetCurrentTargetWorkout() != null)
        {
            currentTargetWorkout = mainController.GetCurrentTargetWorkout();
            if (currentTargetWorkout.GetComponent<WorkoutMono>() != null)
            {
                workoutMono = currentTargetWorkout.GetComponent<WorkoutMono>();
                Workout targetWorkoutClass = workoutMono.GetWorkoutClass();
                targetWorkoutClass.RemoveExercise();
                currentTargetWorkout.GetComponent<WorkoutMono>().GetWorkoutManagerPanel()
                    .GetComponent<WorkoutBlockMono>().RemoveExerciseBlock();

                SLS.Save();
            }
            else
            {
                Debug.Log("workout mono was not found!");
            }
        }
        else
        {
            Debug.Log("exercise mono was not found!");
        }
    }

    public void RefreshAgain() 
    {
        StartCoroutine(Delay(0.01f));
    }

    private IEnumerator Delay(float seconds) 
    { 
        yield return new WaitForSeconds(seconds);

        RefreshLayoutGroup();
    }

    public void RefreshLayoutGroup()
    {
        GameObject targetWorkout = mainController.GetCurrentTargetWorkout();
        workoutMono = targetWorkout.GetComponent<WorkoutMono>();
        GameObject currentTargetWorkoutBlockPanelManager = workoutMono.GetWorkoutManagerPanel();
        workoutBlockMono = currentTargetWorkoutBlockPanelManager.GetComponent<WorkoutBlockMono>();
        scrollMenuContent = workoutBlockMono.GetWorkoutScrollPanelHolder().transform.GetChild(0).GetChild(0).GetChild(0).gameObject;

        LayoutRebuilder.ForceRebuildLayoutImmediate(scrollMenuContent.GetComponent<VerticalLayoutGroup>()
            .GetComponent<RectTransform>());
    }
}
