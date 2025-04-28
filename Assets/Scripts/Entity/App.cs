using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class App : MonoBehaviour
{
    private List<Workout> workouts = new List<Workout>();
    private List<GameObject> workoutBlocks = new List<GameObject>();
    private List<GameObject> workoutBlockManagerPanels = new List<GameObject>();

    public List<Workout> GetWorkoutList() { return this.workouts; }

    public void AddWorkout(Workout workout) 
    {
        if (workout != null)
        {
            this.workouts.Add(workout);
        }
        else {
            Debug.Log("workout was null (Incorrect data input)!");
        }
    }

    public void RemoveWorkout(Workout workout) 
    {
        if (workout != null) {
            if (workouts.Count - 1 >= 0)
            {
                workouts.Remove(workout);
                print(workouts.Count);
            }
        }
    }

    public void AddWorkoutBlock(GameObject workoutBlock) 
    {
        if (workoutBlock != null)
        {
            this.workoutBlocks.Add(workoutBlock);
        }
        else
        {
            Debug.Log("workout block was null (Incorrect data input)!");
        }
    }

    public void RemoveWorkoutBlock(GameObject workoutBlock)
    {
        if (workoutBlock != null)
        {
            if (workoutBlocks.Count - 1 >= 0)
            {
                workoutBlocks.Remove(workoutBlock);
            }
        }
    }

    public void AddWorkoutBlockManagerPanel(GameObject workoutBlockManagerPanel)
    {
        if (workoutBlockManagerPanel != null)
        {
            this.workoutBlockManagerPanels.Add(workoutBlockManagerPanel);
        }
        else
        {
            Debug.Log("workout block manager panel was null (Incorrect data input)!");
        }
    }

    public void RemoveWorkoutBlockManagerPanel(GameObject workoutBlockManagerPanel)
    {
        if (workoutBlockManagerPanel != null)
        {
            if (workoutBlockManagerPanels.Count - 1 >= 0)
            {
                workoutBlockManagerPanels.Remove(workoutBlockManagerPanel);
            }
        }
    }

    public List<GameObject> GetWorkoutBlocksList() 
    {
        return this.workoutBlocks;
    }
}
