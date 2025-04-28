using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AddWorkoutMono : MonoBehaviour
{
    [SerializeField]
    private TMP_InputField workoutNameTextInputHolder;
    [SerializeField]
    private TMP_InputField workoutDateTextInputHolder;

    public TMP_InputField GetWorkoutNameTextInputHolder() { return workoutNameTextInputHolder; }
    public TMP_InputField GetWorkoutDateTextInputHolder() { return workoutDateTextInputHolder; }
}
