using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;
using UnityEngine.UIElements;
using Debug = UnityEngine.Debug;
using Random = UnityEngine.Random;

public class Player : MonoBehaviour
{
    [SerializeField] private float _speed = 210f; //to save
    [SerializeField] private float _jumpHeight = 7f; //to save
    [SerializeField] private GameObject[] _lanterne;
    [SerializeField] private float _razaLanterne = 10f; //to save
    [SerializeField] private float _lightOuterRadiusIncrement = 0.26f;
    [SerializeField] private int _level;
    private Rigidbody2D _rb;
    private LanternLight _lanternLight1;
    private LanternLight _lanternLight2;
    private Animator _animator;
    private GameManager _gameManager;
    private bool _isGrounded = true;
    private bool _hasDoubleJumped = false;
    private bool _lanternSelector = false;
    private Vector2 _worldPosition;
    //To save
    private int _jumpHeightCollected = 0;
    private int _speedCollected = 0;
    private int _razaLanterneCollected = 0;
    
    private int _isDoubleJumpActive = 0;
    
    //done
    private int _fistPowerUpID = -1;
    private int _secondPowerUpID = -1;
    private int _ThirdPowerUpID = -1;
    private int _ForthPowerUpID = -1;
    private int _FithPowerUpID = -1;
    private int _SixtPowerUpID = -1;
    
    private int _fistPowerUpCollected = 0;
    private int _secondPowerUpCollected = 0;
    private int _thirdPowerUpCollected = 0;
    private int _forthPowerUpCollected = 0;
    private int _fithPowerUpCollected = 0;
    private int _SixtPowerUpCollected = 0;
    
    private int _firstLevelCompleted = 0;
    private int _secondLevelCompleted = 0;
    private int _thirdLevelCompleted = 0;
    //until here

    // Start is called before the first frame update
    private void Start()
    {
        AsignVariables();
        CheckIfSaveExist();
        
        _lanterne[1].SetActive(false);
        _lanterne[0].SetActive(true);
    }

    private void CheckIfSaveExist()
    {
        if (PlayerPrefs.HasKey("_firstLevelCompleted")) LoadGame();
        else GenerateGameSave();
    }

    private void GenerateGameSave()
    {
        GeneratePowerUpIds();
        SaveGame();
    }

    private void GeneratePowerUpIds()
    {
        _fistPowerUpID = PowerUpRandomiser();
        _secondPowerUpID = PowerUpRandomiser();
        _ThirdPowerUpID = PowerUpRandomiser();
        _ForthPowerUpID = PowerUpRandomiser();
        _FithPowerUpID = PowerUpRandomiser();
        _SixtPowerUpID = PowerUpRandomiser();
    }

    private int PowerUpRandomiser()
    {
        bool valid = false;
        int PowerUpId = -1;
            
        while (!valid)
        {
            PowerUpId = Random.Range(0, 3);

            valid = CheckIfPowerUpIdIsValid(PowerUpId);
        }

        return PowerUpId;
    }

    private bool CheckIfPowerUpIdIsValid(int Id)
    {
        byte appearance = 0;

        if (Id == _fistPowerUpID) appearance++;
        if (Id == _secondPowerUpID) appearance++;
        if (Id == _ThirdPowerUpID) appearance++;
        if (Id == _ForthPowerUpID) appearance++;
        if (Id == _FithPowerUpID) appearance++;
        if (Id == _SixtPowerUpID) appearance++;

        if (appearance > 2) return true;
        return false;
    }

    private void SaveGame()
    {
        PlayerPrefs.SetInt("_jumpHeightCollected",_jumpHeightCollected);
        PlayerPrefs.SetInt("_speedCollected", _speedCollected);
        PlayerPrefs.SetInt("_razaLanterneCollected", _razaLanterneCollected);
        PlayerPrefs.SetInt("_isDoubleJumpActive", _isDoubleJumpActive);
        PlayerPrefs.SetInt("_fistPowerUpID", _fistPowerUpID);
        PlayerPrefs.SetInt("_secondPowerUpID", _secondPowerUpID);
        PlayerPrefs.SetInt("_ThirdPowerUpID", _ThirdPowerUpID);
        PlayerPrefs.SetInt("_ForthPowerUpID", _ForthPowerUpID);
        PlayerPrefs.SetInt("_FithPowerUpID", _FithPowerUpID);
        PlayerPrefs.SetInt("_SixtPowerUpID", _SixtPowerUpID);
        PlayerPrefs.SetInt("_fistPowerUpCollected", _fistPowerUpCollected);
        PlayerPrefs.SetInt("_secondPowerUpCollected", _secondPowerUpCollected);
        PlayerPrefs.SetInt("_thirdPowerUpCollected", _thirdPowerUpCollected);
        PlayerPrefs.SetInt("_forthPowerUpCollected", _forthPowerUpCollected);
        PlayerPrefs.SetInt("_fithPowerUpCollected", _fithPowerUpCollected);
        PlayerPrefs.SetInt("_SixtPowerUpCollected", _SixtPowerUpCollected);
        PlayerPrefs.SetInt("_firstLevelCompleted", _firstLevelCompleted);
        PlayerPrefs.SetInt("_secondLevelCompleted", _secondLevelCompleted);
        PlayerPrefs.SetInt("_thirdLevelCompleted", _thirdLevelCompleted);
        PlayerPrefs.SetFloat("_speed", _speed);
        PlayerPrefs.SetFloat("_jumpHeight", _jumpHeight);
        PlayerPrefs.SetFloat("_razaLanterne", _razaLanterne);
    }
    
    private void LoadGame()
    {
        _jumpHeightCollected = PlayerPrefs.GetInt("_jumpHeightCollected");
        _speedCollected = PlayerPrefs.GetInt("_speedCollected");
        _razaLanterneCollected = PlayerPrefs.GetInt("_razaLanterneCollected");
        _isDoubleJumpActive = PlayerPrefs.GetInt("_isDoubleJumpActive");
        _fistPowerUpID = PlayerPrefs.GetInt("_fistPowerUpID");
        _secondPowerUpID = PlayerPrefs.GetInt("_secondPowerUpID");
        _ThirdPowerUpID = PlayerPrefs.GetInt("_ThirdPowerUpID");
        _ForthPowerUpID = PlayerPrefs.GetInt("_ForthPowerUpID");
        _FithPowerUpID = PlayerPrefs.GetInt("_FithPowerUpID");
        _SixtPowerUpID = PlayerPrefs.GetInt("_SixtPowerUpID");
        _fistPowerUpCollected = PlayerPrefs.GetInt("_fistPowerUpCollected");
        _secondPowerUpCollected = PlayerPrefs.GetInt("_secondPowerUpCollected");
        _thirdPowerUpCollected = PlayerPrefs.GetInt("_thirdPowerUpCollected");
        _forthPowerUpCollected = PlayerPrefs.GetInt("_forthPowerUpCollected");
        _fithPowerUpCollected = PlayerPrefs.GetInt("_fithPowerUpCollected");
        _SixtPowerUpCollected = PlayerPrefs.GetInt("_SixtPowerUpCollected");
        _firstLevelCompleted = PlayerPrefs.GetInt("_firstLevelCompleted");
        _secondLevelCompleted = PlayerPrefs.GetInt("_secondLevelCompleted");
        _thirdLevelCompleted = PlayerPrefs.GetInt("_thirdLevelCompleted");
        _speed = PlayerPrefs.GetFloat("_speed");
        _jumpHeight = PlayerPrefs.GetFloat("_jumpHeight");
        _razaLanterne = PlayerPrefs.GetFloat("_razaLanterne");
    }

    private void AsignVariables()
    {
        _rb = this.gameObject.GetComponent<Rigidbody2D>();
        
        if(_rb == null)
            Debug.LogError("_rb is NULL");

        _animator = this.gameObject.GetComponent<Animator>();

        if (_animator == null)
        {
            Debug.LogError("_animator is NULL");
        }

        _lanternLight1 = GameObject.Find("Lanterna Platforma - Light").GetComponent<LanternLight>();

        if (_lanternLight1 == null)
        {
            Debug.LogError("_lanternLight1 is NULL");
        }
        
        _lanternLight2 = GameObject.Find("Lanterna PowerUps - Light").GetComponent<LanternLight>();
        
        if (_lanternLight2 == null)
        {
            Debug.LogError("_lanternLight2 is NULL");
        }

        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

        if (_gameManager == null)
        {
            Debug.LogError("_gameManager is NULL");
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (transform.tag == "Finish")
        {
            LevelComplete();
        }

        _isGrounded = true;
        _hasDoubleJumped = false;
    }

    private void LevelComplete()
    {
        switch (_level)
        {
            case 0: _firstLevelCompleted = 1; break;
            case 1: _secondLevelCompleted = 1; break;
            case 2: _thirdLevelCompleted = 1; break;
        }
        
        SaveGame();
    }

    private void Movement()
    {
        float HorizontalInput = Input.GetAxis("Horizontal");
        
        if (Input.GetKeyDown(KeyCode.Space) && _isGrounded)
        {
            _rb.velocity = new Vector2(_rb.velocity.x, _jumpHeight);
            _isGrounded = false;
            _hasDoubleJumped = false;
        } else if (Input.GetKeyDown(KeyCode.Space) && !_hasDoubleJumped)
        {
            _rb.velocity = new Vector2(_rb.velocity.x, _jumpHeight);
            _hasDoubleJumped = true;
        }
        
        _rb.velocity = new Vector2(HorizontalInput * (_speed * Time.smoothDeltaTime),_rb.velocity.y);

        if (HorizontalInput > 0f)
        {
            transform.rotation = new Quaternion(0f, 0f, 0f, 0f);
        }else if (HorizontalInput < 0f)
        {
            transform.rotation = new Quaternion(0f, -180f, 0f, 0f);
        }

        _animator.SetFloat("Speed", _rb.velocity.x);
    }

    private void lanternMouvemrnt()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
    
        if (Physics.Raycast(ray, out hit))
        {
            Vector3 mousePos = hit.point;
            _lanterne[Convert.ToInt32(_lanternSelector)].transform.position = mousePos;
        }
    }
    
    private void Lanterne()
    {
        //Change lantern on pressing the "R" key
        if (Input.GetKeyDown(KeyCode.R))
        {
            _lanternSelector = !_lanternSelector;
            UpdateLantern();
        }
        
        lanternMouvemrnt();
    }

    private void UpdateLantern()
    {
        _lanterne[Convert.ToInt32(_lanternSelector)].SetActive(true);
        _lanterne[Convert.ToInt32(!_lanternSelector)].SetActive(false);
    }

    private void UpdateLanterLightRadius()
    {
        _lanterne[0].transform.localScale = new Vector3(_razaLanterne,_razaLanterne,_razaLanterne);
        _lanterne[1].transform.localScale = new Vector3(_razaLanterne,_razaLanterne,_razaLanterne);
        
        UpdateLanternLight2DRadius();
    }

    private void UpdateLanternLight2DRadius()
    {
        _lanternLight1.IncreaseLightOuterRadius(_lightOuterRadiusIncrement);
        _lanternLight2.IncreaseLightOuterRadius(_lightOuterRadiusIncrement);
    }

    private void OnDeath()
    {
       
    }
    
    private void CheckIfFallen()
    {
        if (transform.position.y < -7f)
        {
            OnDeath();
        }
    }

    public void IncreaseSpeed(float increment)
    {
        _speed += increment;
    }

    public void IncreaseJumpHeight(float increment)
    {
        _jumpHeight += increment;
    }
    
    public void IncreaseLightLevel(float radius)
    {
        _razaLanterne += radius;
        UpdateLanterLightRadius();
    }

    public int GetLevel()
    {
        return _level;
    }
    
    // Update is called once per frame
    void Update()
    {
        Movement();
        Lanterne();
        CheckIfFallen();
    }
}
