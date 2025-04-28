using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SLS
{
    private static SaveLoadSystem SaveLoadSystem = GameObject.FindObjectOfType<SaveLoadSystem>();
    public static void Save() 
    {
        SaveLoadSystem.SaveAppData();
    }

    public static void Load() 
    { 
        SaveLoadSystem.LoadAppData();
    }   
}
