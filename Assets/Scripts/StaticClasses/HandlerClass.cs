using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class HandlerClass
{
    private static ObjectHandler objectHandlerController = GameObject.FindObjectOfType<ObjectHandler>();

    public static GameObject GetWorkoutBlockPanelPrefab() 
    { 
        if (objectHandlerController != null)
        {
            var workoutBlockPanelPrefab = objectHandlerController.GetWorkoutBlockPanelPrefab();
            return workoutBlockPanelPrefab;
        }
        return null;
    }
    public static GameObject GetWorkoutBlockPrefab()
    {
        if (objectHandlerController != null)
        {
            var workoutBlockPrefab = objectHandlerController.GetWorkoutBlockPrefab();
            return workoutBlockPrefab;
        }
        return null;
    }
    public static GameObject GetExerciseBlockPrefab()
    {
        if (objectHandlerController != null)
        {
            var exerciseBlockPrefab = objectHandlerController.GetExerciseBlockPrefab();
            return exerciseBlockPrefab;
        }
        return null;
    }
    public static GameObject GetSetBlockPrefab()
    {
        if (objectHandlerController != null)
        {
            var setBlockPrefab = objectHandlerController.GetSetBlockPrefab();
            return setBlockPrefab;
        }
        return null;
    }
    public static GameObject GetAddWorkoutPanel()
    {
        if (objectHandlerController != null)
        {
            var addWorkoutPanel = objectHandlerController.GetAddWorkoutPanel();
            return addWorkoutPanel;
        }
        return null;
    }
    public static GameObject GetSettingsPanel()
    {
        if (objectHandlerController != null)
        {
            var settingsPanel = objectHandlerController.GetSettingsPanel();
            return settingsPanel;
        }
        return null;
    }
    public static GameObject GetMainMenuPanel()
    {
        if (objectHandlerController != null)
        {
            var mainMenuPanel = objectHandlerController.GetMainMenuPanel();
            return mainMenuPanel;
        }
        return null;
    }

    public static GameObject GetStatsPanel()
    {
        if (objectHandlerController != null)
        {
            var statsPanel = objectHandlerController.GetStatsPanel();
            return statsPanel;
        }
        return null;
    }
    public static GameObject GetWorkoutBlockPanelHolder()
    {
        if (objectHandlerController != null)
        {
            var workoutBlockPanleHolder = objectHandlerController.GetWorkoutBlockPanelHolder();
            return workoutBlockPanleHolder;
        }
        return null;
    }
    public static GameObject GetConfimationPanelHolder()
    {
        if (objectHandlerController != null)
        {
            var confimationPanelHolder = objectHandlerController.GetRemoveWorkoutConfirmPanel();
            return confimationPanelHolder;
        }
        return null;
    }

    public static GameObject GetUpperMainMenuPanel()
    {
        if (objectHandlerController != null)
        {
            var upperMainMenuPanel = objectHandlerController.GetUpperMainMenuPanel();
            return upperMainMenuPanel;
        }
        return null;
    }

    public static GameObject GetExitingAppPanelHolder()
    {
        if (objectHandlerController != null)
        {
            var exitingAppPanelHolder = objectHandlerController.GetExitingAppPanel();
            return exitingAppPanelHolder;
        }
        return null;
    }
}
