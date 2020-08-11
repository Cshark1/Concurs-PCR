<<<<<<< HEAD
﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
=======
﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject[] _spawnPoints;
    [SerializeField] private GameObject[] _powerUpsPrefabs;
    [SerializeField] private GameObject _GameOverText;
    [SerializeField] private GameObject _WinMessageText;
    [SerializeField] private GameObject _menu;
    [SerializeField] private GameObject _devMenu;
    [SerializeField] private GameObject _tutorial;
    
    private Player _player;
    private int _level;

    private void Start()
    {
        _player = GameObject.Find("Player").GetComponent<Player>();

        if (_player == null)
        {
            Debug.LogError("_player is NULL");
        }

        _level = _player.GetLevel();
        
        CheckIfTutorialCompleted();

        SpawnPowerUp();
    }

    private void SpawnPowerUp()
    {
        switch (_level)
        {
            case 0:
            {
                if (PlayerPrefs.GetInt("_firstLevelCompleted") == 0)
                {
                    SpawnSpecificPowerUp(PlayerPrefs.GetInt("_fistPowerUpID"), _spawnPoints[0]);
                    SpawnSpecificPowerUp(PlayerPrefs.GetInt("_secondPowerUpID"), _spawnPoints[1]);
                    break;
                }
                if (PlayerPrefs.GetInt("_fistPowerUpCollected") == 0)
                {
                    SpawnSpecificPowerUp(PlayerPrefs.GetInt("_fistPowerUpID"), _spawnPoints[0]);
                }

                if (PlayerPrefs.GetInt("_secondPowerUpCollected") == 0)
                {
                    SpawnSpecificPowerUp(PlayerPrefs.GetInt("_secondPowerUpID"), _spawnPoints[1]);
                }
                break;
            }
            case 1:
            {
                if (PlayerPrefs.GetInt("_secondLevelCompleted") == 0)
                {
                    SpawnSpecificPowerUp(PlayerPrefs.GetInt("_ThirdPowerUpID"), _spawnPoints[0]);
                    SpawnSpecificPowerUp(PlayerPrefs.GetInt("_ForthPowerUpID"), _spawnPoints[1]);
                    break;
                }
                if (PlayerPrefs.GetInt("_thirdPowerUpCollected") == 0)
                {
                    SpawnSpecificPowerUp(PlayerPrefs.GetInt("_ThirdPowerUpID"), _spawnPoints[0]);
                }

                if (PlayerPrefs.GetInt("_forthPowerUpCollected") == 0)
                {
                    SpawnSpecificPowerUp(PlayerPrefs.GetInt("_ForthPowerUpID"), _spawnPoints[1]);
                }
                break;
            }
            case 2:
            {
                if (PlayerPrefs.GetInt("_thirdLevelCompleted") == 0)
                {
                    SpawnSpecificPowerUp(PlayerPrefs.GetInt("_FithPowerUpID"), _spawnPoints[0]);
                    SpawnSpecificPowerUp(PlayerPrefs.GetInt("_SixtPowerUpID"), _spawnPoints[1]);
                    break;
                }
                if (PlayerPrefs.GetInt("_fithPowerUpCollected") == 0)
                {
                    SpawnSpecificPowerUp(PlayerPrefs.GetInt("_ThirdPowerUpID"), _spawnPoints[0]);
                }

                if (PlayerPrefs.GetInt("_SixtPowerUpCollected") == 0)
                {
                    SpawnSpecificPowerUp(PlayerPrefs.GetInt("_ForthPowerUpID"), _spawnPoints[1]);
                }
                break;
            }
            default:Debug.LogError("invalid level"); break;
        }
    }

    private void SpawnSpecificPowerUp(int powerUpId, GameObject spawnPoint)
    {
        GameObject gameObject;
        
        Debug.Log(powerUpId);
        gameObject = Instantiate(_powerUpsPrefabs[powerUpId],spawnPoint.transform.position, Quaternion.identity);
        
        Debug.Log(_powerUpsPrefabs[powerUpId].name + " spawn at " + spawnPoint.name);
        
        gameObject.transform.SetParent(spawnPoint.transform);
>>>>>>> 91c88eb00048ade7cd67544636a59d2f3c800c12
    }

    private void checkForExit()
    {
<<<<<<< HEAD
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
=======
        if (Input.GetKeyDown(KeyCode.Escape) && PlayerPrefs.GetInt("_tutorialCompleted") == 1)
        {
            ShowHideMainMenu();
        }
    }

    IEnumerator WaitForRestart()
    {
        while (true)
        {
            if (Input.GetKey(KeyCode.R))
            {
                switch (_level)
                {
                    case 0:SceneManager.LoadScene(1); break;
                    case 1:SceneManager.LoadScene(2); break;
                    case 2:SceneManager.LoadScene(3); break;
                }
            }

            yield return null;
        }
    }
    
    IEnumerator WaitForMainMenu()
    {
        while (true)
        {
            if (Input.GetKey(KeyCode.M))
            {
                SceneManager.LoadScene(0);
            }

            yield return null;
        }
    }

    private void CheckIfTutorialCompleted()
    {
        if (PlayerPrefs.GetInt("_tutorialCompleted") == 0)
        {
            _tutorial.SetActive(true);
            StartCoroutine(WaitForClose());
            StartCoroutine(WaitForAutoClose());
        }
    }

    IEnumerator WaitForClose()
    {
        while (true)
        {
            if (Input.GetKey(KeyCode.Escape))
            {
                PlayerPrefs.SetInt("_tutorialCompleted", 1);
                _tutorial.SetActive(false);
            }

            yield return null;
        }
    }

    IEnumerator WaitForAutoClose()
    {
        yield return new WaitForSeconds(60);
        PlayerPrefs.SetInt("_tutorialCompleted", 1);
        _tutorial.SetActive(false);
    }
    
    private void EndTutorial()
    {
        
    }

    public void OnGameOver()
    {
        _GameOverText.SetActive(true);
        StartCoroutine(WaitForRestart());
    }

    public void OnLevelComplete()
    {
        _WinMessageText.SetActive(true);
        StartCoroutine(WaitForMainMenu());
    }

    public void ShowSavedGame()
    {
        Debug.Log("_jumpHeightCollected = " + PlayerPrefs.GetInt("_jumpHeightCollected"));
        Debug.Log("_speedCollected = " + PlayerPrefs.GetInt("_speedCollected"));
        Debug.Log("_razaLanterneCollected = " + PlayerPrefs.GetInt("_razaLanterneCollected"));
        Debug.Log("_isDoubleJumpActive = " + PlayerPrefs.GetInt("_isDoubleJumpActive"));
        Debug.Log("_fistPowerUpID = " + PlayerPrefs.GetInt("_fistPowerUpID"));
        Debug.Log("_secondPowerUpID = " + PlayerPrefs.GetInt("_secondPowerUpID"));
        Debug.Log("_ThirdPowerUpID = " + PlayerPrefs.GetInt("_ThirdPowerUpID"));
        Debug.Log("_ForthPowerUpID = " + PlayerPrefs.GetInt("_ForthPowerUpID"));
        Debug.Log("_FithPowerUpID = " + PlayerPrefs.GetInt("_FithPowerUpID"));
        Debug.Log("_SixtPowerUpID = " + PlayerPrefs.GetInt("_SixtPowerUpID"));
        Debug.Log("_fistPowerUpCollected = " + PlayerPrefs.GetInt("_fistPowerUpCollected"));
        Debug.Log("_secondPowerUpCollected = " + PlayerPrefs.GetInt("_secondPowerUpCollected"));
        Debug.Log("_thirdPowerUpCollected = " + PlayerPrefs.GetInt("_thirdPowerUpCollected"));
        Debug.Log("_forthPowerUpCollected = " + PlayerPrefs.GetInt("_forthPowerUpCollected"));
        Debug.Log("_fithPowerUpCollected = " + PlayerPrefs.GetInt("_fithPowerUpCollected"));
        Debug.Log("_SixtPowerUpCollected = " + PlayerPrefs.GetInt("_SixtPowerUpCollected"));
        Debug.Log("_firstLevelCompleted = " + PlayerPrefs.GetInt("_firstLevelCompleted"));
        Debug.Log("_secondLevelCompleted = " + PlayerPrefs.GetInt("_secondLevelCompleted"));
        Debug.Log("_thirdLevelCompleted = " + PlayerPrefs.GetInt("_thirdLevelCompleted"));
        Debug.Log("_speed = " + PlayerPrefs.GetFloat("_speed"));
        Debug.Log("_jumpHeight = " + PlayerPrefs.GetFloat("_jumpHeight"));
        Debug.Log("_razaLanterne = " + PlayerPrefs.GetFloat("_razaLanterne"));
    }

    public void ResetSaveGame()
    {
        PlayerPrefs.DeleteAll();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void ShowHideMainMenu()
    {
        if (Time.timeScale == 1)
        {
            _menu.gameObject.SetActive(true);
            Time.timeScale = 0f;
        }else if (Time.timeScale == 0)
        {
            _menu.gameObject.SetActive(false);
            Time.timeScale = 1f;
        }
    }

    public void ShowHideDevMenu()
    {
        if (_devMenu.activeSelf)
        {
            _devMenu.SetActive(false);
            _menu.SetActive(true);
        }else if (!_devMenu.activeSelf)
        {
            _devMenu.SetActive(true);
            _menu.SetActive(false);
        }
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(0);
    }
    
    // Update is called once per frame
    void Update()
    {
        checkForExit();
>>>>>>> 91c88eb00048ade7cd67544636a59d2f3c800c12
    }
}
