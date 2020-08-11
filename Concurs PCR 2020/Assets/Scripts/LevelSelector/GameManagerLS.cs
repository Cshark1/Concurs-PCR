using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManagerLS : MonoBehaviour
{
    [SerializeField] private Button[] _LevelButtons; 
    
    // Start is called before the first frame update
    private void Start()
    {
        CheckIfLevelIsUnlocked(); 
    }

    private void CheckIfLevelIsUnlocked()
    {
        if (PlayerPrefs.GetInt("_firstLevelCompleted") == 1)
        {
            _LevelButtons[0].interactable = true;
        }
        
        if (PlayerPrefs.GetInt("_secondLevelCompleted") == 1)
        {
            _LevelButtons[1].interactable = true;
        }
    }

    public void Level1()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(1);
    }
    public void Level2()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(2);
    }
    public void Level3()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(3);
    }

    public void Back()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }
}
