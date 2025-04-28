using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.Globalization;
using System.IO;

public class SaveLoadSystem : MonoBehaviour
{
    private List<Workout> workouts;
    private App appControlelr;
    private Reconstruction reconstruction;

    private Workout workoutToRecieve;

    private void Start()
    {
        if (FindObjectOfType<App>() != null)
        {
            appControlelr = FindObjectOfType<App>();
        }
    }
    public void SaveAppData()
    {
        workouts = appControlelr.GetWorkoutList();
        BinaryFormatter BinaryFormatter = new BinaryFormatter();
        int count = workouts.Count;

        FileStream fileCountStream = new FileStream(GetCountPath(), FileMode.Create);
        BinaryFormatter.Serialize(fileCountStream, count);
        fileCountStream.Close();

        for (int i = 0; i < count; i++)
        {
            FileStream fileStream = new FileStream(GetMainPath()+i, FileMode.Create);
            BinaryFormatter.Serialize(fileStream, workouts[i]);
            fileStream.Close();
        }
    }

    public void LoadAppData() 
    {
        BinaryFormatter BinaryFormatter = new BinaryFormatter();

        int count = 0;
        if (File.Exists(GetCountPath()))
        {
            FileStream fileCountStream = new FileStream(GetCountPath(), FileMode.Open);
            count = (int)BinaryFormatter.Deserialize(fileCountStream);
            fileCountStream.Close();
        }
        else
        {
            Debug.Log(GetCountPath() + "  path not found!");
        }

        for (int i = 0; i < count; i++)
        {
            if (File.Exists(GetMainPath() + i))
            {
                FileStream fileStream = new FileStream(GetMainPath() + i, FileMode.Open);
                workoutToRecieve = BinaryFormatter.Deserialize(fileStream) as Workout;
                fileStream.Close();

                if (FindObjectOfType<Reconstruction>() != null)
                {
                    reconstruction = FindObjectOfType<Reconstruction>();
                    reconstruction.Reconstruct(workoutToRecieve);
                }
            }
            else 
            {
                Debug.Log(GetMainPath() + "  path not found!");
            }
        }
    }

    public void ClearAppData() 
    {
        BinaryFormatter BinaryFormatter = new BinaryFormatter();
    }

    private string GetMainPath()
    {
        string path = "/appdatasaveload";
        return Application.persistentDataPath + path;
    }

    private string GetCountPath()
    {
        string path = "/appdatacountfile";
        return Application.persistentDataPath + path;
    }
}
