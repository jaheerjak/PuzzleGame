using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    [SerializeField] private int CurrentLevel=0;
    [SerializeField] private int prevLevel=0;
    [SerializeField] private GameObject PuzzleManager;
    [SerializeField] private GameObject LevelCompleted;
    [SerializeField] private GameObject nextLevelBtn;

    private void Awake()
    {
        Instance = this;
        ShowCurrentLevel(CurrentLevel);
    }

    public void ShowCurrentLevel(int level)
    {
        CurrentLevel = level;
        LevelCompleted.SetActive(false);
        foreach (Transform child in PuzzleManager.transform)
        {
            child.gameObject.SetActive(false);
        }
        PuzzleManager.transform.GetChild(CurrentLevel).gameObject.SetActive(true);
    }

    public void ShowLevelCompleted() 
    {
        
        LevelCompleted.SetActive(true);
        prevLevel = CurrentLevel;
        if (CurrentLevel < PuzzleManager.transform.childCount)
        {
            
            CurrentLevel++;
            nextLevelBtn.SetActive(true);
        }
        else
        {
            nextLevelBtn.SetActive(false);
        }
        
    }
    public void OnClick_Next()
    {
        ShowCurrentLevel(CurrentLevel);
    } 
    public void OnClick_TestNext()
    {
        CurrentLevel++;
        ShowCurrentLevel(CurrentLevel);
    }

    public void OnClick_Prev()
    {
        CurrentLevel--;
        ShowCurrentLevel(CurrentLevel);
    }

    public void OnClick_PlayAgain()
    {
        ShowCurrentLevel(prevLevel);
    }
    public void OnClick_Home()
    {
        SceneManager.LoadScene("Home");
    }
}
