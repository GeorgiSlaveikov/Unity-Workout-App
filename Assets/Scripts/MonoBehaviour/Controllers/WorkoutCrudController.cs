using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WorkoutCrudController : MonoBehaviour
{
    private CalendarCrudController calendarCrudController;

    private GameObject workoutBlockPrefab;
    private GameObject workoutManagerPanelPrefab;
    private GameObject workoutBlockPanelHolder;
    private GameObject scrollMenuContent;
    private GameObject addWorkoutPanel;
    private DaysEnum.Days day;

    private WorkoutMono workoutMono;
    private AddWorkoutMono addWorkoutMono;
    private WorkoutBlockMono workoutBlockMono;

    private TextMeshProUGUI workoutNameTextHolder;
    private TextMeshProUGUI workoutDateTextHolder;
    private TextMeshProUGUI workoutExerciseCountTextHolder;

    private TextMeshProUGUI workoutBlockManagerPanelNameTextHolder;
    private TextMeshProUGUI workoutBlockManagerPanelDateTextHolder;

    private TMP_InputField workoutNameInputText;

    private void Start()
    {
        calendarCrudController = FindObjectOfType<CalendarCrudController>();
        InitializeProperty();
    }

    private void InitializeProperty()
    {
        if (HandlerClass.GetWorkoutBlockPrefab() != null) { workoutBlockPrefab = HandlerClass.GetWorkoutBlockPrefab(); }
        else { Debug.Log("workout block prefab was not found!"); }
        if (HandlerClass.GetWorkoutBlockPanelPrefab() != null) { workoutManagerPanelPrefab = HandlerClass.GetWorkoutBlockPanelPrefab(); }
        else { Debug.Log("workout block panel manager prefab was not found!"); }
        if (HandlerClass.GetMainMenuPanel() != null) { scrollMenuContent = HandlerClass.GetMainMenuPanel().transform.GetChild(0).GetChild(0).GetChild(0).GetChild(0).gameObject; }
        else { Debug.Log("main menu panel was not found!"); }
        if (HandlerClass.GetAddWorkoutPanel() != null) { addWorkoutPanel = HandlerClass.GetAddWorkoutPanel(); }
        else { Debug.Log("add workout panel was not found!"); }
        if (HandlerClass.GetWorkoutBlockPanelHolder() != null) { workoutBlockPanelHolder = HandlerClass.GetWorkoutBlockPanelHolder(); }
        else { Debug.Log("add workout panel was not found!"); }
    }
    public void AddWorkout()
    {
        GameObject workoutPhysicleObject = Instantiate(workoutBlockPrefab);
        workoutPhysicleObject.transform.SetParent(scrollMenuContent.transform);
        GameObject workoutManagerPanel = Instantiate(workoutManagerPanelPrefab, workoutBlockPanelHolder.transform.position, Quaternion.identity);
        workoutManagerPanel.transform.SetParent(workoutBlockPanelHolder.transform);
        workoutManagerPanel.SetActive(false);

        if (workoutPhysicleObject.GetComponent<WorkoutMono>() != null)
        {
            workoutMono = workoutPhysicleObject.GetComponent<WorkoutMono>();
        }
        else
        {
            Debug.Log("workout mono was no found!");
        }

        if (workoutManagerPanel.GetComponent<WorkoutBlockMono>() != null)
        {
            workoutBlockMono = workoutManagerPanel.GetComponent<WorkoutBlockMono>();
        }
        else
        {
            Debug.Log("workout block mono was no found!");
        }

        if (addWorkoutPanel.GetComponent<AddWorkoutMono>() != null)
        {
            addWorkoutMono = addWorkoutPanel.GetComponent<AddWorkoutMono>();
        }
        else
        {
            Debug.Log("add workout panel mono was no found!");
        }

        workoutMono.SetWorkoutManagerPanel(workoutManagerPanel);
        workoutBlockMono.SetWorkoutManagerPanel(workoutManagerPanel);

        workoutNameInputText = addWorkoutMono.GetWorkoutNameTextInputHolder();

        workoutNameTextHolder = workoutMono.GetWorkoutNameTextHolder();
        workoutDateTextHolder = workoutMono.GetWorkoutDateTextHolder();
        workoutExerciseCountTextHolder = workoutMono.GetWorkoutExerciseCountTextHolder();

        workoutBlockManagerPanelNameTextHolder = workoutBlockMono.GetWorkoutNameTextHolder();
        workoutBlockManagerPanelDateTextHolder = workoutBlockMono.GetWorkoutDateTextHolder();

        string workoutName = workoutNameInputText.text;
        string workoutFullDate = calendarCrudController.GetFinalDateString();
        string workoutDay = calendarCrudController.GetDay();
        int exerciseCount = 0;

        workoutNameTextHolder.text = workoutName;
        workoutDateTextHolder.text = workoutFullDate;
        workoutExerciseCountTextHolder.text = $"Exercise count: {exerciseCount}";

        workoutBlockManagerPanelNameTextHolder.text = workoutName;
        workoutBlockManagerPanelDateTextHolder.text = workoutFullDate;

        Workout workout = new Workout(workoutName, workoutFullDate, workoutDay);
        workoutMono.SetWorkoutClass(workout);
        App app;
        if (FindObjectOfType<App>() != null) {
            app = FindObjectOfType<App>();
            app.AddWorkout(workout);
            app.AddWorkoutBlock(workoutPhysicleObject);
            app.AddWorkoutBlockManagerPanel(workoutManagerPanel);
        }

        workoutNameInputText.text = "";

        SLS.Save();
    }

    public void RemoveWorkout() 
    {
        MainController mainController = FindFirstObjectByType<MainController>();
        GameObject selectedWorkout = mainController.GetCurrentTargetWorkout();
        Workout workoutToRemove = selectedWorkout.GetComponent<WorkoutMono>().GetWorkoutClass();
        App app;
        if (FindObjectOfType<App>() != null)
        {
            print("remove workout!");
            app = FindObjectOfType<App>();
            app.RemoveWorkout(workoutToRemove);
            app.RemoveWorkoutBlock(selectedWorkout);
            app.RemoveWorkoutBlockManagerPanel(selectedWorkout.GetComponent<WorkoutMono>().GetWorkoutManagerPanel());

            Destroy(selectedWorkout);
            Destroy(selectedWorkout.GetComponent<WorkoutMono>().GetWorkoutManagerPanel());

            SLS.Save();
        }
    }
}
