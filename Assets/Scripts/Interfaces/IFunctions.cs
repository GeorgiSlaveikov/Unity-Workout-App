using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IFunctions
{
    void OpenAddWorkoutMenu();
    void CloseAddWorkoutMenu();
    void OpenCloseSettingsMenu();
    void OpenFilterMenu();
    void CloseFilterMenu();
    void OpenWorkoutBlockPanel(GameObject panel);
    void CloseWorkoutBlockPanel(GameObject panel);
    void AddWorkout();
    void RemoveWorkout(GameObject selectedWorkout);
    void AddExercise();
    void RemoveExercise();
    void AddSet(GameObject targetExercise);
    void RemoveSet(GameObject targetExercise);
    void ExitApplication();
    void OpenAddExerciseMenu();
    void CloseAddExerciseMenu();
    void AcceptWorkoutRemoval();
    void DeclineWorkoutRemoval();
    void AcceptExitingApp();
    void DeclineExitingApp();
    void NextMonth();
    void PreviousMonth();
    void GetSelectedDate(GameObject dayBlock);

    void OpenCloseStatsPanel();
}
