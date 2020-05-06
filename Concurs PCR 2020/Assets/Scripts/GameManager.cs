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
        
        Debug.Log(_level);
        
        SpawnPowerUp();
    }

    private void SpawnPowerUp()
    {
        Debug.Log(PlayerPrefs.GetInt("_firstLevelCompleted"));
        switch (_level)
        {
            case 0:
            {
                if (PlayerPrefs.GetInt("_firstLevelCompleted") == 0)
                {
                    Debug.Log("da ftr");
                    SpawnSpecificPowerUp(PlayerPrefs.GetInt("_fistPowerUpID"), _spawnPoints[0]);
                    SpawnSpecificPowerUp(PlayerPrefs.GetInt("_secondPowerUpID"), _spawnPoints[1]);
                    break;
                }
                if (PlayerPrefs.GetInt("_fistPowerUpCollected") == 0)
                {
                    SpawnSpecificPowerUp(PlayerPrefs.GetInt("_fistPowerUpID"), _spawnPoints[0]);
                }

                if (PlayerPrefs.GetInt("_secondPowerUpID") == 0)
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
            Application.Quit();
        }
    }

    IEnumerator WaitForRestart()
    {
        while (true)
        {
            if (Input.GetKey(KeyCode.R))
            {
                SceneManager.LoadScene(1);
            }

            yield return null;
        }
    }

    public void OnGameOver()
    {
        _GameOverText.SetActive(true);
        StartCoroutine(WaitForRestart());
    }

    // Update is called once per frame
    void Update()
    {
        checkForExit();
    }
}
