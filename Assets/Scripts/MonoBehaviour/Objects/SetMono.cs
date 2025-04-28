using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SetMono : MonoBehaviour
{
    [SerializeField]
    private Set set;

    [SerializeField]
    private GameObject exerciseBlock;

    [SerializeField]
    private TextMeshProUGUI setNumberTextHolder;
    [SerializeField]
    private TMP_InputField setIntensityTextInputHolder;
    [SerializeField]
    private TMP_InputField setRepTextInputHolder;

    public TextMeshProUGUI GetSetNumberTextHolder() { return setNumberTextHolder; }
    public TMP_InputField GetSetRepTextInputHolder() {  return setRepTextInputHolder; }
    public TMP_InputField GetSetIntensityTextInputHolder() {  return setIntensityTextInputHolder; }

    public void SetSetClass(Set set)
    {
        if (set != null)
        {
            this.set = set;
        }
        else
        {
            Debug.Log("set was null (incorrect data input)!");
        }
    }

    public Set GetSetClass() { return set; }

    public void SetExerciseBlock(GameObject exercise)
    {
        if (exercise != null)
        {
            this.exerciseBlock = exercise;
        }
        else
        {
            Debug.Log("exercise was null (incorrect data input)!");
        }
    }

    public GameObject GetExerciseBlock() { return exerciseBlock; }
}
