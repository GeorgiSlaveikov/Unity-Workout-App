using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectHandler : MonoBehaviour
{
    [SerializeField]
    public GameObject workoutBlockPrefab;
    [SerializeField]
    public GameObject exerciseBlockPrefab;
    [SerializeField]
    public GameObject setBlockPrefab;
    [SerializeField]
    private GameObject workoutBlockPanelPrefab;

    [SerializeField] private GameObject addWorkoutPanel;
    [SerializeField] private GameObject settingsPanel;
    [SerializeField] private GameObject filterPanel;
    [SerializeField] private GameObject statsPanel;
    [SerializeField] private GameObject mainMenuPanel;
    [SerializeField] private GameObject removeWorkoutConfirmPanel;
    [SerializeField] private GameObject upperMainMenuPanel;
    [SerializeField] private GameObject exitingAppPanel;
    [SerializeField] private GameObject workoutBlockPanelHolder;


    public GameObject GetWorkoutBlockPrefab() { return workoutBlockPrefab; }
    public GameObject GetAddWorkoutPanel() { return addWorkoutPanel; }
    public GameObject GetSettingsPanel() { return settingsPanel; }
    public GameObject GetFilterPanel() { return filterPanel; }
    public GameObject GetStatsPanel() { return statsPanel; }
    public GameObject GetMainMenuPanel() { return mainMenuPanel; }
    public GameObject GetWorkoutBlockPanelPrefab() { return workoutBlockPanelPrefab; }
    public GameObject GetExerciseBlockPrefab() { return exerciseBlockPrefab; }
    public GameObject GetRemoveWorkoutConfirmPanel() { return removeWorkoutConfirmPanel; }
    public GameObject GetUpperMainMenuPanel() { return upperMainMenuPanel; }
    public GameObject GetExitingAppPanel() { return exitingAppPanel; }
    public GameObject GetSetBlockPrefab() { return setBlockPrefab; }
    public GameObject GetWorkoutBlockPanelHolder() { return workoutBlockPanelHolder; }
}
