using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting.Dependencies.NCalc;
using UnityEngine;

[System.Serializable]
public class Exercise
{
    private string exerciseName;
    private int exerciseNumber;
    private int setsCount;

    private List<Set> sets;

    public Exercise(string exerciseName)
    {
        ExerciseName = exerciseName;
        sets = new List<Set>();
        setsCount = sets.Count;
    }

    public Exercise()
    {
        ExerciseName = "Default Exercise";
        exerciseNumber = 1;
        setsCount = 0;
        sets = new List<Set>();
        setsCount = sets.Count;
    }

    public int GetSetCount() { return setsCount; }

    public List<Set> GetSetList() { return sets; }

    public int GetExerciseNumber() { return exerciseNumber; }

    public void SetExerciseNumber(int value) { this.exerciseNumber = value; }


    public string ExerciseName
    {
        get { return exerciseName; }
        set { exerciseName = value; }
    }

    public void AddSet(Set set)
    {
        if (set != null)
        {
            this.sets.Add(set);
            setsCount = sets.Count();
        }
    }

    public void RemoveSet()
    {
        if (sets.Count > 0) 
        {
            var setToRemove = this.sets.Last();
            if (setToRemove != null)
            {
                this.sets.Remove(setToRemove);
                setsCount = sets.Count();
            }
        }
    }
}
