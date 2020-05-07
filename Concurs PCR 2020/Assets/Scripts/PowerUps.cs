using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUps : MonoBehaviour
{
    [SerializeField] private float _rotationSpeed = 120f;
    [SerializeField] private byte _powerUpID;
    [SerializeField] private float _radiusIncrement = 2.5f;
    [SerializeField] private float _speedIncrement = 25f;
    [SerializeField] private float _jumpHeightIncrement = 1f;
    private Player _player;
    
    // Start is called before the first frame update
    void Start()
    {
        _player = GameObject.Find("Player").GetComponent<Player>();
        
        if(_player == null)
            Debug.LogError("_player is NULL");
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.transform.name == "Player")
        {
            switch (_powerUpID)
            {
                case 0:
                {
                    _player.IncreaseLightLevel(_radiusIncrement);
                    _player.MarkPowerUpAsCollected(other);
                    Destroy(this.gameObject);
                    break;
                }
                case 1:
                {
                    _player.IncreaseSpeed(_speedIncrement);
                    _player.MarkPowerUpAsCollected(other);
                    Destroy(this.gameObject);
                    break;
                }
                case 2:
                {
                    _player.IncreaseJumpHeight(_jumpHeightIncrement);
                    _player.MarkPowerUpAsCollected(other);
                    Destroy(this.gameObject);
                    break;
                }
            }
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
