using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainController : MonoBehaviour, IFunctions
{
    private GameObject mainPanel;
    private GameObject upperMainMenuPanel;
    private GameObject addWorkoutPanel;
    private GameObject confirmationPanel;
    private GameObject statsPanel;
    private GameObject settingsPanel;

    private WorkoutCrudController workoutCrudController;
    private ExerciseCrudController exerciseCrudController;
    private SetCrudController setCrudController;
    private CalendarCrudController calendarCrudController;

    [SerializeField]
    private GameObject targetedWorkout;

    [SerializeField]
    private GameObject targetedExercise;

    [SerializeField] private Animator createWorkoutAnimatorController;    
    [SerializeField] private Animator exitPanelAnimatorController;    
    [SerializeField] private Animator removeWorkoutAnimatorController;    
    [SerializeField] private Animator settingsPanelAnimatorController;    
    [SerializeField] private Animator statsPanelAnimatorController;    

    private void Start()
    {
        if (HandlerClass.GetAddWorkoutPanel() != null)
        {
            addWorkoutPanel = HandlerClass.GetAddWorkoutPanel();
        }
        else 
        {
            Debug.Log("add workout panel was not found!");
        }
        if (HandlerClass.GetSettingsPanel() != null)
        {
            settingsPanel = HandlerClass.GetSettingsPanel();
        }
        else
        {
            Debug.Log("settings panel was not found!");
        }
        if (HandlerClass.GetStatsPanel() != null)
        {
            statsPanel = HandlerClass.GetStatsPanel();
        }
        else
        {
            Debug.Log("stats panel was not found!");
        }
        if (HandlerClass.GetConfimationPanelHolder() != null)
        {
            confirmationPanel = HandlerClass.GetConfimationPanelHolder();
        }
        else
        {
            Debug.Log("confirm panel was not found!");
        }
        if (HandlerClass.GetMainMenuPanel() != null)
        {
            mainPanel = HandlerClass.GetMainMenuPanel();
        }
        else
        {
            Debug.Log("main menu panel was not found!");
        }
        if (HandlerClass.GetUpperMainMenuPanel() != null)
        {
            upperMainMenuPanel = HandlerClass.GetUpperMainMenuPanel();
        }
        else
        {
            Debug.Log("upper main menu panel was not found!");
        }
        if (FindObjectOfType<WorkoutCrudController>() != null)
        {
            workoutCrudController = FindObjectOfType<WorkoutCrudController>();
        }
        else
        {
            Debug.Log("workout crud controller was not found!");
        }
        if (FindObjectOfType<ExerciseCrudController>() != null)
        {
            exerciseCrudController = FindObjectOfType<ExerciseCrudController>();
        }
        else
        {
            Debug.Log("exercise crud controller was not found!");
        }
        if (FindObjectOfType<SetCrudController>() != null)
        {
            setCrudController = FindObjectOfType<SetCrudController>();
        }
        else
        {
            Debug.Log("set crud controller was not found!");
        }
        if (FindObjectOfType<CalendarCrudController>() != null)
        {
            calendarCrudController = FindObjectOfType<CalendarCrudController>();
        }
        else
        {
            Debug.Log("calendar controller was not found!");
        }

    }

    public void SetCurrentTargetWorkout(GameObject workout) {
        if (workout != null)
        {
            targetedWorkout = workout;
        }
        else {
            Debug.Log("workout is null (incorrect data input)!");
        }
    }
    public GameObject GetCurrentTargetWorkout() {  return targetedWorkout; }
    public void ClearCurrentTargetWorkoutProperty() { targetedWorkout = null; }

    public void SetCurrentTargetExercise(GameObject exercise)
    {
        if (exercise != null)
        {
            targetedExercise = exercise;
        }
        else
        {
            Debug.Log("exercise is null (incorrect data input)!");
        }
    }
    public GameObject GetCurrentTargetExercise() { return targetedExercise; }
    public void ClearCurrentTargetExerciseProperty() { targetedExercise = null; }

    public void AddExercise()
    {
        exerciseCrudController.AddExercise();
    }

    public void AddSet(GameObject targetExercise)
    {
        setCrudController.AddSet(targetExercise);
        targetExercise.GetComponent<ExerciseMono>().GetRepGridHolder().SetActive(true);
        exerciseCrudController.RefreshLayoutGroup();
        exerciseCrudController.RefreshAgain();
    }

    public void AddWorkout()
    {
        workoutCrudController.AddWorkout();
        CloseAddWorkoutMenu();
    }

    public void CloseAddWorkoutMenu()
    {
        createWorkoutAnimatorController.SetBool("flag2", true);
        createWorkoutAnimatorController.SetBool("flag", false);
        StartCoroutine(Deley(addWorkoutPanel, 0.5f, false));  
        mainPanel.SetActive(true);
        upperMainMenuPanel.SetActive(true);
    }

    public void CloseFilterMenu()
    {
        //
    }

    public void CloseWorkoutBlockPanel(GameObject panel)
    {
        GameObject workoutManagerPanel = panel;
        if (panel != null)
        {
            workoutManagerPanel.SetActive(false);
        }
        mainPanel.SetActive(true);
    }

    public void ExitApplication()
    {
        HandlerClass.GetExitingAppPanelHolder().SetActive(true);
        exitPanelAnimatorController.SetBool("flag1", true);
        exitPanelAnimatorController.SetBool("flag2", false);
    }

    public void OpenAddWorkoutMenu()
    {
        addWorkoutPanel.SetActive(true);
        createWorkoutAnimatorController.SetBool("flag", true);
        createWorkoutAnimatorController.SetBool("flag2", false);
        mainPanel.SetActive(false);
        upperMainMenuPanel.SetActive(false);
    }

    public void OpenFilterMenu()
    {
        //
    }

    private bool settingsFlag = false;
    public void OpenCloseSettingsMenu()
    {
        if (!settingsFlag)
        {
            // open
            settingsPanel.SetActive(true);
            settingsPanel.transform.GetChild(0).gameObject.SetActive(true);
            settingsPanelAnimatorController.SetBool("flag1", true);
            settingsPanelAnimatorController.SetBool("flag2", false);
            settingsFlag = true;
        }
        else if (settingsFlag) 
        {
            // close
            settingsPanelAnimatorController.SetBool("flag2", true);
            settingsPanelAnimatorController.SetBool("flag1", false);
            settingsPanel.transform.GetChild(0).gameObject.SetActive(false);
            StartCoroutine(Deley(settingsPanel, 0.7f, false));
            settingsPanel.SetActive(true);
            settingsFlag = false;
        }
    }

    public void OpenWorkoutBlockPanel(GameObject panel)
    {
        GameObject workoutManagerPanel = panel;
        if (panel != null) {
            workoutManagerPanel.SetActive(true);
        }
        mainPanel.SetActive(false);
    }

    public void RemoveExercise()
    {
        exerciseCrudController.RemoveExercise();
    }

    public void RemoveSet(GameObject targetExercise)
    {
        if (targetExercise != null)
        {
            setCrudController.RemoveSet(targetExercise);
            targetExercise.GetComponent<ExerciseMono>().GetRepGridHolder().SetActive(true);
            exerciseCrudController.RefreshLayoutGroup();
            exerciseCrudController.RefreshAgain();
            exerciseCrudController.RefreshAgain();
        }
    }

    public void RemoveWorkout(GameObject selectedWorkout)
    {
        confirmationPanel.SetActive(true);
        removeWorkoutAnimatorController.SetBool("flag1", true);
        removeWorkoutAnimatorController.SetBool("flag2", false);
        targetedWorkout = selectedWorkout;
    }

    public void OpenAddExerciseMenu()
    {
        targetedWorkout
            .GetComponent<WorkoutMono>()
            .GetWorkoutManagerPanel()
            .GetComponent<WorkoutBlockMono>()
            .GetCreateExercisePanelHolder()
            .SetActive(true);
    }

    public void CloseAddExerciseMenu()
    {
        targetedWorkout
            .GetComponent<WorkoutMono>()
            .GetWorkoutManagerPanel()
            .GetComponent<WorkoutBlockMono>()
            .GetCreateExercisePanelHolder()
            .SetActive(false);
    }

    public void AcceptWorkoutRemoval()
    {
        removeWorkoutAnimatorController.SetBool("flag1", false);
        removeWorkoutAnimatorController.SetBool("flag2", true);
        workoutCrudController.RemoveWorkout();
        StartCoroutine(Deley(confirmationPanel, 0.5f, false));
    }

    public void DeclineWorkoutRemoval()
    {
        removeWorkoutAnimatorController.SetBool("flag1", false);
        removeWorkoutAnimatorController.SetBool("flag2", true);
        StartCoroutine(Deley(confirmationPanel, 0.5f, false));
    }

    public void AcceptExitingApp()
    {
        SLS.Save();
        Application.Quit();
    }

    public void DeclineExitingApp()
    {
        exitPanelAnimatorController.SetBool("flag1", false);
        exitPanelAnimatorController.SetBool("flag2", true);
        StartCoroutine(Deley(HandlerClass.GetExitingAppPanelHolder(), 0.5f, false));
        SLS.Save();
    }

    private IEnumerator Deley(GameObject block, float seconds, bool flag) 
    {
        yield return new WaitForSeconds(seconds);

        block.SetActive(flag);
    }

    public void NextMonth()
    {
        calendarCrudController.NextMonth();
    }

    public void PreviousMonth()
    {
        calendarCrudController.PreviousMonth();
    }

    public void GetSelectedDate(GameObject dayBlock)
    {
        calendarCrudController.Reciever(dayBlock);
    }

    private bool statsFlag;
    public void OpenCloseStatsPanel() 
    {
        if (!statsFlag) 
        { 
            // open
            statsPanel.SetActive(true);
            statsPanelAnimatorController.SetBool("flag2", false);
            statsPanelAnimatorController.SetBool("flag1", true);
            statsFlag = true;
        }
        else if (statsFlag)
        {
            // close
            statsPanelAnimatorController.SetBool("flag2", true);
            statsPanelAnimatorController.SetBool("flag1", false);
            StartCoroutine(Deley(statsPanel, 0.5f, false));
            statsFlag = false;
        }
    }
}
