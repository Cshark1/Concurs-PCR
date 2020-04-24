﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UIElements;
using Debug = UnityEngine.Debug;

public class Player : MonoBehaviour
{
    [SerializeField] private float _speed = 130f;
    [SerializeField] private float _jumpHeight = 5f;
    [SerializeField] private GameObject[] _lanterne;
    [SerializeField] private float _razaLanterne = 10f;
    private Rigidbody2D _rb;
    private bool _isgrounded = true;
    private bool _hasdoubleJumped = false;
    private bool _lanternSelector = false;
    private Vector2 _worldPosition;

    // Start is called before the first frame update
    void Start()
    {
        UpdateLanterLightRadius();
        
        _rb = this.gameObject.GetComponent<Rigidbody2D>();
        
        if(_rb == null)
            Debug.LogError("_crb is NULL");
    }

    private void OnCollisionStay2D(Collision2D other)
    {
        _isgrounded = true;
        _hasdoubleJumped = false;
    }

    private void Movement()
    {
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
        
        _rb.velocity = new Vector2(Input.GetAxis("Horizontal") * (_speed * Time.smoothDeltaTime),_rb.velocity.y);
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

    public void IncreaseLightLevel(float radius)
    {
        _razaLanterne += radius;
        UpdateLanterLightRadius();
    }

    private void UpdateLanterLightRadius()
    {
        _lanterne[0].transform.localScale = new Vector3(_razaLanterne,_razaLanterne,_razaLanterne);
        _lanterne[1].transform.localScale = new Vector3(_razaLanterne,_razaLanterne,_razaLanterne);
    }

    public void IncreaseSpeed(float increment)
    {
        _speed += increment;
    }

    public void IncreaseJumpHeight(float increment)
    {
        _jumpHeight += increment;
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
        Lanterne();
    }
}
