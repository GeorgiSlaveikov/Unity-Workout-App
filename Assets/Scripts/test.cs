using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class test : MonoBehaviour
{
    App appController;
    List<Workout> workouts;

    private void Start()
    {
        appController = FindObjectOfType<App>();
        if (appController != null)
        {
            workouts = appController.GetWorkoutList();
        }
    }

    private void GetList()
    {
        if (appController != null)
        {
            workouts = appController.GetWorkoutList();
        }
    }

    private void Update()
    {
        if (UnityEngine.Input.GetKeyDown("l"))
        {
            if (workouts.Count != 0)
            {
                PrintList(workouts);
            }
        }
    }

    private void PrintList(List<Workout> listToPrint)
    {
        StringBuilder stringBuilder = new StringBuilder();

        string exercisesData;
        string workoutData;
        string setData;
        foreach (var workout in listToPrint)
        {
            workoutData = $"{workout.WorkoutName}-{workout.WorkoutDate}({workout.WorkoutDay}-(Exercises: {workout.GetExerciseCount()}))";
            var exercisesList = workout.GetExercisesList();
            stringBuilder.AppendLine(workoutData);
            foreach (var exercise in exercisesList)
            {
                exercisesData = $"({exercise.GetExerciseNumber()})-{exercise.ExerciseName}-Sets count: {exercise.GetSetCount()}";
                stringBuilder.AppendLine(exercisesData);
                var setsList = exercise.GetSetList();
                foreach (var set in setsList) 
                {
                    setData = $"({set.GetSetNumber()})-{set.SetReps}";
                    stringBuilder.AppendLine(setData);
                }
            }
        }
        print(stringBuilder.ToString());
    }
}
