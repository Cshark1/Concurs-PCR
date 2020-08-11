﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxBG : MonoBehaviour
{
    private float _length, _startPos;
    [SerializeField] private GameObject _cam;
    [SerializeField] private float parallaxEffect;
    
    // Start is called before the first frame update
    void Start()
    {
        _startPos = transform.position.x;
        _length = this.gameObject.GetComponent<SpriteRenderer>().bounds.size.x;
    }

    // Update is called once per frame
    void Update()
    {
        float temp = _cam.transform.position.x * (1 - parallaxEffect);
        float dist = _cam.transform.position.x * parallaxEffect;

        transform.position = new Vector3(_startPos + dist, transform.position.y, transform.position.z);

        if (temp > _startPos + _length) _startPos += _length;
        else if (temp < _startPos - _length) _startPos -= _length;
    }
}
