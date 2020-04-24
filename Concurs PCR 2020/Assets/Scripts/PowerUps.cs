using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUps : MonoBehaviour
{
    [SerializeField] private float _rotationSpeed = 120f;
    [SerializeField] private byte _powerUpID;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        switch (_powerUpID)
        {
            
        }
    }

    private void PowerUpRotation()
    {
        transform.Rotate(new Vector3(0f,1f,0f), 1f * Time.deltaTime * _rotationSpeed);
    }

    // Update is called once per frame
    void Update()
    {
        PowerUpRotation();
    }
}
