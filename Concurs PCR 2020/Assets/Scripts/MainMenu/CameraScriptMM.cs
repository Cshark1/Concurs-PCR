﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScriptMM : MonoBehaviour
{
    [SerializeField] private float _speed = 7f;
    
    // Update is called once per frame
    void Update()
    {
        transform.Translate(_speed,0f,0f);
    }
}
