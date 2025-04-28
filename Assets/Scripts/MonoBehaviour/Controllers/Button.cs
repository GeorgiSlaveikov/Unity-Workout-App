using UnityEngine;

public class Button : MonoBehaviour
{
    private MainController mainController;
    private void Start()
    {
        if (FindObjectOfType<MainController>() != null)
        {
            mainController = FindObjectOfType<MainController>();
        }
    }
    public void AddExercise()
    {
        mainController.AddExercise();
        CloseAddExerciseMenu();
    }

    public void AddSet(GameObject exercisePanelBlock)
    {
        mainController.AddSet(exercisePanelBlock);
    }

    public void AddWorkout()
    {
        mainController.AddWorkout();
    }

    public void CloseAddWorkoutMenu()
    {
        mainController.CloseAddWorkoutMenu();
    }

    public void CloseFilterMenu()
    {
        //
    }

    public void OpenCloseSettingsMenu()
    {
        mainController.OpenCloseSettingsMenu();
    }

    public void CloseWorkoutBlockPanel(GameObject panelBlock)
    {
        WorkoutBlockMono workoutBlockMono = null;
        
        if (panelBlock.GetComponent<WorkoutBlockMono>() != null)
        {
            workoutBlockMono = panelBlock.GetComponent<WorkoutBlockMono>();
            if (workoutBlockMono.GetWorkoutManagerPanel() != null)
            {
                GameObject workoutManagerPanel = workoutBlockMono.GetWorkoutManagerPanel();
                mainController.CloseWorkoutBlockPanel(workoutManagerPanel);
                mainController.ClearCurrentTargetWorkoutProperty();
            }
            else
            {
                Debug.Log("incorrect data input!");
            }
        }
        else
        {
            Debug.Log("workout block mono was not found!");
        }
    }

    public void ExitApplication()
    {
        mainController.ExitApplication();
    }

    public void OpenAddWorkoutMenu()
    {
        mainController.OpenAddWorkoutMenu();
    }

    public void AcceptExitingApp() 
    { 
        mainController.AcceptExitingApp();
    }

    public void DeclineExitingApp()
    {
        mainController.DeclineExitingApp();
    }

    public void OpenFilterMenu()
    {
        //
    }

    public void AcceptWorkoutRemoval() 
    {
        mainController.AcceptWorkoutRemoval();
    }
    public void DeclineWorkoutRemoval()
    {
        mainController.DeclineWorkoutRemoval();
    }

    public void OpenWorkoutBlockPanel()
    {
        WorkoutMono workoutMono = null;
        if (this.GetComponent<WorkoutMono>() != null)
        {
            workoutMono = this.GetComponent<WorkoutMono>();
            if (workoutMono.GetWorkoutManagerPanel() != null)
            {
                GameObject workoutManagerPanel = workoutMono.GetWorkoutManagerPanel();
                mainController.OpenWorkoutBlockPanel(workoutManagerPanel);
                mainController.SetCurrentTargetWorkout(this.gameObject);
            }
            else {
                Debug.Log("incorrect data input!");
            }
        }
        else {
            Debug.Log("workout mono was not found!");
        }
    }

    public void RemoveExercise()
    {
        mainController.RemoveExercise();
    }

    public void RemoveSet(GameObject targetExercise)
    {
        mainController.RemoveSet(targetExercise);
    }

    public void RemoveWorkout(GameObject selectedWorkout)
    {
        mainController.RemoveWorkout(selectedWorkout);
    }

    public void OpenAddExerciseMenu() 
    { 
        mainController.OpenAddExerciseMenu();
    }
    public void CloseAddExerciseMenu() 
    { 
        mainController.CloseAddExerciseMenu();
    }

    public void NextMonth() 
    {
        mainController.NextMonth();
    }

    public void PreviousMonth() 
    {
        mainController.PreviousMonth();
    }

    public void GetSelectedDate(GameObject dayBlock) 
    {
        if (dayBlock.GetComponent<DayBlockMono>() != null)
        {
            if (dayBlock.GetComponent<DayBlockMono>().GetUsedFalg() == true)
            {
                mainController.GetSelectedDate(dayBlock);
            }
        }
    }

    public void OpenCloseSetPanelTab(GameObject exerciseBlock) 
    {
        ExerciseMono exerciseMono = exerciseBlock.GetComponent<ExerciseMono>();
        if (exerciseMono != null) 
        {
            var panel = exerciseMono.GetContentHolder();
            exerciseMono.OpenCloseSetPanelTab(panel);
        }
    }

    public void OpenCloseStatsPanel() 
    {
        mainController.OpenCloseStatsPanel();
    }
}
