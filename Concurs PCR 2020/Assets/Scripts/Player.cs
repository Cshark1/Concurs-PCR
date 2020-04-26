using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;
using UnityEngine.UIElements;
using Debug = UnityEngine.Debug;

public class Player : MonoBehaviour
{
    [SerializeField] private float _speed = 130f;
    [SerializeField] private float _jumpHeight = 5f;
    [SerializeField] private GameObject[] _lanterne;
    [SerializeField] private float _razaLanterne = 10f;
    [SerializeField] private float _lightOuterRadiusIncrement = 0.26f;
    [SerializeField] private float _characterRotationSpeed = 45f;
    private Rigidbody2D _rb;
    private LanternLight _lanternLight1;
    private LanternLight _lanternLight2;
    private Animator _animator;
    private bool _isgrounded = true;
    private bool _hasdoubleJumped = false;
    private bool _lanternSelector = false;
    private Vector2 _worldPosition;

    // Start is called before the first frame update
    void Start()
    {
        AsignVariables();
        
        _lanterne[1].SetActive(false);
        _lanterne[0].SetActive(true);
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
            Debug.LogError("_lanternLight1 is null");
        }
        
        _lanternLight2 = GameObject.Find("Lanterna PowerUps - Light").GetComponent<LanternLight>();
        
        if (_lanternLight2 == null)
        {
            Debug.LogError("_lanternLight2 is null");
        }
    }

    private void OnCollisionStay2D(Collision2D other)
    {
        _isgrounded = true;
        _hasdoubleJumped = false;
    }

    private void Movement()
    {
        float HorizontalInput = Input.GetAxis("Horizontal");
        
        if (Input.GetKeyDown(KeyCode.Space) && _isgrounded)
        {
            _rb.velocity = new Vector2(_rb.velocity.x, _jumpHeight);
            _isgrounded = false;
            _hasdoubleJumped = false;
        }

        if (Input.GetKeyDown(KeyCode.Space) && !_hasdoubleJumped)
        {
            _rb.velocity = new Vector2(_rb.velocity.x, _jumpHeight);
            _hasdoubleJumped = true;
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

    // Update is called once per frame
    void Update()
    {
        Movement();
        Lanterne();
    }
}
