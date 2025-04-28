using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Set
{
    private int setNumber;
    private int setReps;
    private string setIntensity;

    public Set(int setReps, string setIntensity)
    {
        SetReps = setReps;
        SetIntensity = setIntensity;
    }

    public Set()
    {
        setNumber = 1;
        setReps = 0;
    }
    public void SetSetNumber(int value) { setNumber = value; }
    public int GetSetNumber() { return setNumber; }

    public int SetReps
    {
        get { return setReps; }
        set { setReps = value; }
    }
    public string SetIntensity
    {
        get { return setIntensity; }
        set { setIntensity = value; }
    }
}
