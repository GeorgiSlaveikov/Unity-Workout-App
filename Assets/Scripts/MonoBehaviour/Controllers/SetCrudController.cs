using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;
using Microsoft.Win32.SafeHandles;
using UnityEngine.UI;

public class SetCrudController : MonoBehaviour
{

    private GameObject setBlockPrefab;
    private GameObject repSetGridHandler;

    private ExerciseMono exerciseMono;
    private SetMono setMono;

    private TextMeshProUGUI setNumberTextHolder;
    private TMP_InputField setRepNumberInputTextHandler;
    private TMP_InputField setIntensityValueInputTextHandler;
    private void Start()
    {
        InitializeProperty();
    }

    private void InitializeProperty()
    {
        if (HandlerClass.GetSetBlockPrefab() != null) { setBlockPrefab = HandlerClass.GetSetBlockPrefab(); }
        else { Debug.Log("set block prefab was not found!"); }
    }

    public void AddSet(GameObject tragetExerciseBlock)
    {
        if (tragetExerciseBlock != null)
        {
            if (tragetExerciseBlock.GetComponentInChildren<ExerciseMono>() != null) 
            {
                exerciseMono = tragetExerciseBlock.GetComponentInChildren<ExerciseMono>();
                repSetGridHandler = exerciseMono.GetRepGridHolder();
            }
            else 
            {
                Debug.Log("exercise mono was not found!");
            }
            
        }
        else {
            Debug.Log("target exercise was null (incorrect data input)!");
        }
        GameObject setPhysicleObject;
        if (exerciseMono.GetSetBlocks().Count < 8)
        {
            setPhysicleObject = Instantiate(setBlockPrefab);
            setPhysicleObject.transform.SetParent(repSetGridHandler.transform);
            exerciseMono.AddSetBlock(setPhysicleObject);

            if (setPhysicleObject.GetComponent<SetMono>() != null)
            {
                setMono = setPhysicleObject.GetComponent<SetMono>();
                setNumberTextHolder = setMono.GetSetNumberTextHolder();
                Exercise targetExercise = exerciseMono.GetExerciseClass();
                if (targetExercise.GetSetCount() < 8)
                {
                    int setReps = 0;
                    string setIntensity = "";
                    Set set = new Set(setReps, setIntensity);
                    targetExercise.AddSet(set);

                    int setNumber = targetExercise.GetSetCount();
                    set.SetSetNumber(setNumber);

                    setMono.SetExerciseBlock(tragetExerciseBlock);
                    setMono.SetSetClass(set);

                    setNumberTextHolder.text = $"({setNumber})";

                    //RefreshLayoutGroup(tragetExerciseBlock);
                    //StartCoroutine(Delay(tragetExerciseBlock));

                    SLS.Save();
                }
                else {
                    Debug.Log("Not enough space!");
                }
            }
            else {
                Debug.Log("set mono was not found!");
            }
            
        }
        else {
            Debug.Log("set grid is full! Not enought space for another set/rep!");
        }
    }

    public void SetSetRepValue(GameObject targetSet) 
    {
        if (targetSet.GetComponent<SetMono>().GetExerciseBlock() != null) 
        {
            setMono = targetSet.GetComponent<SetMono>();

            setRepNumberInputTextHandler = setMono.GetSetRepTextInputHolder();

            Set set = setMono.GetSetClass();

            set.SetReps = int.Parse(setRepNumberInputTextHandler.text);

            print("data saved");
            SLS.Save();
        }
        else { Debug.Log("set mono not found!"); }
    }

    public void SetSetIntensityValue(GameObject targetSet) 
    {
        if (targetSet.GetComponent<SetMono>().GetExerciseBlock() != null)
        {
            setMono = targetSet.GetComponent<SetMono>();

            setIntensityValueInputTextHandler = setMono.GetSetIntensityTextInputHolder();

            if (setIntensityValueInputTextHandler.text != null)
            {
                Set set = setMono.GetSetClass();

                set.SetIntensity = setIntensityValueInputTextHandler.text;
            }

            print("data saved");
            SLS.Save();
        }
        else { Debug.Log("set mono not found!"); }
    }

    public void RemoveSet(GameObject targetExercise) 
    {
        if (targetExercise.GetComponent<ExerciseMono>() != null)
        {
            exerciseMono = targetExercise.GetComponent<ExerciseMono>();
            Exercise exercise = exerciseMono.GetExerciseClass();

            if (exerciseMono.GetSetBlocks().Count() > 0 && exercise.GetSetList().Count > 0)
            {
                var setToDestroy = exerciseMono.GetSetBlocks().Last();
                exerciseMono.GetSetBlocks().Remove(setToDestroy);
                Destroy(setToDestroy);
                exercise.RemoveSet();

                //RefreshLayoutGroup(targetExercise);
                //StartCoroutine(Delay(targetExercise));

                SLS.Save();
            }
            else {
                Debug.Log("no set to remove!");
            }
        }
        else {
            Debug.Log("exercise mono was not found!");
        }
    }
    [SerializeField] private GameObject test;

    private IEnumerator Delay(GameObject targetExercise) 
    { 
        yield return new WaitForSeconds(1f);
        RefreshLayoutGroup(targetExercise);
    }
    public void RefreshLayoutGroup(GameObject targetExercise)
    {
        MainController mainController = FindObjectOfType<MainController>();
        GameObject targetWorkout = mainController.GetCurrentTargetWorkout();
        WorkoutMono workoutMono = targetWorkout.GetComponent<WorkoutMono>();
        GameObject currentTargetWorkoutBlockPanelManager = workoutMono.GetWorkoutManagerPanel();
        WorkoutBlockMono workoutBlockMono = currentTargetWorkoutBlockPanelManager.GetComponent<WorkoutBlockMono>();
        GameObject scrollMenuContent = workoutBlockMono.GetWorkoutScrollPanelHolder().transform.GetChild(0)
            .GetChild(0).GetChild(0).GetChild(0).gameObject;

        LayoutRebuilder.ForceRebuildLayoutImmediate(scrollMenuContent.GetComponent<VerticalLayoutGroup>()
            .GetComponent<RectTransform>());
    }
}
