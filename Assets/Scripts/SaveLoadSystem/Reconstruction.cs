using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using TMPro;
using UnityEngine;

public class Reconstruction : MonoBehaviour
{
    private GameObject workoutBlockPrefab;
    private GameObject workoutBlockManagerPanelPrefab;
    private GameObject workoutExerciseBlockPrefab;
    private GameObject setBlockPrefab;

    private GameObject workoutBlockPanelHaoler;
    private GameObject scrollMenuContent;
    private GameObject scrollExerciseMenuContent;
    private GameObject setRepGridHadler;

    //workout
    private TextMeshProUGUI workoutNameTextHolder;
    private TextMeshProUGUI workoutDateTextHolder;
    private TextMeshProUGUI workoutExerciseCountTextHolder;

    //workout manager panel
    private TextMeshProUGUI workoutBlockManagerPanelNameTextHolder;
    private TextMeshProUGUI workoutBlockManagerPanelDateTextHolder;

    //exercise
    private TextMeshProUGUI exerciseNameTextHolder;
    private TextMeshProUGUI exerciseNumberTextHolder;

    //set
    private TextMeshProUGUI setNumberTextHolder;
    private TMP_InputField setRepNumberInputTextHandler;
    private TMP_InputField setIntensityValueInputTextHandler;

    private App appController;

    private WorkoutMono workoutMono;
    private WorkoutBlockMono workoutBlockMono;
    private ExerciseMono exerciseMono;
    private SetMono setMono;

    private Workout workoutToReconstruct;
    public void Reconstruct(Workout workout)
    {
        InstantiateProperty();
        workoutToReconstruct = workout;

        //workout reconstruction
        GameObject workoutBlock = Instantiate(workoutBlockPrefab);
        workoutBlock.transform.SetParent(scrollMenuContent.transform);
        GameObject workoutBlockManagerPanel = Instantiate(workoutBlockManagerPanelPrefab, workoutBlockPanelHaoler.transform.position, Quaternion.identity);
        workoutBlockManagerPanel.transform.SetParent(workoutBlockPanelHaoler.transform);

        if (workoutBlock.GetComponent<WorkoutMono>() != null)
        {
            workoutMono = workoutBlock.GetComponent<WorkoutMono>();
        }
        else { Debug.Log("workoutMono not found!"); }
        if (workoutBlockManagerPanel.GetComponent<WorkoutBlockMono>() != null)
        {
            workoutBlockMono = workoutBlockManagerPanel.GetComponent<WorkoutBlockMono>();
        }
        else { Debug.Log("workoutBlockMono not found!"); }

        workoutMono.SetWorkoutManagerPanel(workoutBlockManagerPanel);
        workoutBlockMono.SetWorkoutManagerPanel(workoutBlockManagerPanel);

        workoutNameTextHolder = workoutMono.GetWorkoutNameTextHolder();
        workoutDateTextHolder = workoutMono.GetWorkoutDateTextHolder();
        workoutExerciseCountTextHolder = workoutMono.GetWorkoutExerciseCountTextHolder();

        workoutBlockManagerPanelNameTextHolder = workoutBlockMono.GetWorkoutNameTextHolder();
        workoutBlockManagerPanelDateTextHolder = workoutBlockMono.GetWorkoutDateTextHolder();

        string workoutName = workoutToReconstruct.WorkoutName;
        string workoutDate = workoutToReconstruct.WorkoutDate;
        string workoutDay = workoutToReconstruct.WorkoutDay;
        string workoutDateDay = workoutDate;
        int exerciseCount = workoutToReconstruct.GetExerciseCount();

        workoutNameTextHolder.text = workoutName;
        workoutDateTextHolder.text = workoutDateDay;
        workoutExerciseCountTextHolder.text = $"Exercise count: {exerciseCount}";

        workoutBlockManagerPanelNameTextHolder.text = workoutName;
        workoutBlockManagerPanelDateTextHolder.text = workoutDateDay;

        workoutMono.SetWorkoutClass(workoutToReconstruct);

        appController.AddWorkout(workoutToReconstruct);
        appController.AddWorkoutBlock(workoutBlock);
        appController.AddWorkoutBlockManagerPanel(workoutBlockManagerPanel);

        workoutBlockMono.RefreshLayoutGroup();


        //exercises reconstruction
        scrollExerciseMenuContent = workoutBlockMono.GetWorkoutScrollPanelHolder().transform.GetChild(0).GetChild(0).GetChild(0).gameObject;
        List<Exercise> exercises = workoutToReconstruct.GetExercisesList();
        for (int i = 0; i < exerciseCount; i++)
        {
            GameObject exerciseBlock = Instantiate(workoutExerciseBlockPrefab);
            exerciseBlock.transform.SetParent(scrollExerciseMenuContent.transform);

            if (exerciseBlock.GetComponent<ExerciseMono>() != null)
            {
                exerciseMono = exerciseBlock.GetComponent<ExerciseMono>();
            }
            else { Debug.Log("exercise mono not found!"); }

            exerciseNameTextHolder = exerciseMono.GetExerciseNameTextHolder();
            exerciseNumberTextHolder = exerciseMono.GetExerciseNumberTextHolder();

            string exerciseName = exercises[i].ExerciseName;
            int exerciseNumber = exercises[i].GetExerciseNumber();
            int setCount = exercises[i].GetSetCount();

            exerciseNameTextHolder.text = exerciseName;
            exerciseNumberTextHolder.text = $"{exerciseNumber}.";

            workoutBlockMono.AddExerciseBlock(exerciseBlock);
            exerciseMono.SetExerciseClass(exercises[i]);

            //set reconstruction
            setRepGridHadler = exerciseMono.GetRepGridHolder();
            List<Set> sets = exercises[i].GetSetList();
            
            for (int j = 0; j < setCount; j++)
            {
                GameObject setBlock = Instantiate(setBlockPrefab);
                setBlock.transform.SetParent(setRepGridHadler.transform);

                if (setBlock.GetComponent<SetMono>() != null)
                {
                    setMono = setBlock.GetComponent<SetMono>();
                }
                else { Debug.Log("set mono not found!"); }

                setNumberTextHolder = setMono.GetSetNumberTextHolder();
                setRepNumberInputTextHandler = setMono.GetSetRepTextInputHolder();
                setIntensityValueInputTextHandler = setMono.GetSetIntensityTextInputHolder();

                int setNumber = sets[j].GetSetNumber();
                int setReps = sets[j].SetReps;
                string setIntensity = sets[j].SetIntensity;

                FieldMono fieldMonoSetRepInput = setRepNumberInputTextHandler.GetComponent<FieldMono>();
                fieldMonoSetRepInput.SetBoolFlagState(false);
                FieldMono fieldMonoSetIntensityInput = setIntensityValueInputTextHandler.GetComponent<FieldMono>();
                fieldMonoSetIntensityInput.SetBoolFlagState(false);

                setNumberTextHolder.text = $"({setNumber})";
                setRepNumberInputTextHandler.text = setReps.ToString();
                setIntensityValueInputTextHandler.text = setIntensity;

                exerciseMono.AddSetBlock(setBlock);
                setMono.SetExerciseBlock(exerciseBlock);
                setMono.SetSetClass(sets[j]);

                fieldMonoSetRepInput.SetBoolFlagState(true);
                fieldMonoSetIntensityInput.SetBoolFlagState(true);
            }
        }
    }

    private void InstantiateProperty() 
    {
        appController = FindObjectOfType<App>();
        workoutToReconstruct = new Workout();

        if (HandlerClass.GetWorkoutBlockPrefab() != null) { workoutBlockPrefab = HandlerClass.GetWorkoutBlockPrefab(); }
        else { Debug.Log("workout block prefab not found!"); }
        if (HandlerClass.GetWorkoutBlockPanelPrefab() != null) { workoutBlockManagerPanelPrefab = HandlerClass.GetWorkoutBlockPanelPrefab(); }
        if (HandlerClass.GetExerciseBlockPrefab() != null) { workoutExerciseBlockPrefab = HandlerClass.GetExerciseBlockPrefab(); }
        if (HandlerClass.GetSetBlockPrefab() != null) { setBlockPrefab = HandlerClass.GetSetBlockPrefab(); }

        if (HandlerClass.GetWorkoutBlockPanelHolder() != null) { workoutBlockPanelHaoler = HandlerClass.GetWorkoutBlockPanelHolder(); }
        if (HandlerClass.GetMainMenuPanel() != null)
        {
            scrollMenuContent = HandlerClass.GetMainMenuPanel()
                .transform.GetChild(0).GetChild(0).GetChild(0).GetChild(0).gameObject;
        }
    }
}
