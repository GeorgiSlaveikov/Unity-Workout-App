using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[System.Serializable]
public class Workout
{
    private string workoutName;
    private string workoutDate;
    private string workoutDay;
    private List<Exercise> exercises;
    private int exerciseCount;

    public Workout(string workoutName, string workoutDate, string workoutDay)
    {
        WorkoutDate = workoutDate;
        WorkoutDay = workoutDay;
        WorkoutName = workoutName;
        exercises = new List<Exercise>();
        exerciseCount = exercises.Count;
    }

    public Workout()
    {
        workoutName = "Default Workout";
        workoutDate = "dd.mm.yyyy";
        workoutDay = "day";
        exerciseCount = 0;
    }

    public string WorkoutDate
    {
        get { return workoutDate; }
        set { workoutDate = value; }
    }

    public string WorkoutDay
    {
        get { return workoutDay; }
        set { workoutDay = value; }
    }

    public string WorkoutName
    {
        get { return workoutName; }
        set { workoutName = value; }
    }

    public List<Exercise> GetExercisesList() { return exercises; }

    public int GetExerciseCount() { return exerciseCount; }

    public void AddExercise(Exercise exercise)
    {
        if (exercise != null)
        {
            Debug.Log($"added exercise {exercise.ExerciseName}");
            exercises.Add(exercise);
            exerciseCount = exercises.Count;
        }
    }

    public void RemoveExercise()
    {
        if (exercises.Count > 0)
        {
            var exerciseToRemove = exercises.Last();
            if (exerciseToRemove != null)
            {
                Debug.Log($"removed exercise {exerciseToRemove.ExerciseName}");
                exercises.Remove(exerciseToRemove);
                exerciseCount = exercises.Count;
            }
        }
        else {
            Debug.Log("no elements in the list");
        }
    }
}
