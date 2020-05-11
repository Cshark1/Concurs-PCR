using System;
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
    }

    private void checkForExit()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
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
    }
}
