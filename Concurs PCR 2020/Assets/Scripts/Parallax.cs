using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    private float _length, _startPos;
    private GameObject _cam;
    private GameObject _player;
    [SerializeField] private float parallaxEffect;
    
    // Start is called before the first frame update
    private void Start()
    {
        InitializeVariables();
        _startPos = transform.position.x;
        _length = this.gameObject.GetComponent<SpriteRenderer>().bounds.size.x;
    }

    private void InitializeVariables()
    {
        _cam = GameObject.Find("Main Camera");

        if (_cam == null)
        {
            Debug.LogError("_cam is NULL");
        }

        _player = GameObject.FindWithTag("Player");
        
        if (_player)
        {
            Debug.LogError("_player is NULL");
        }
    }

    // Update is called once per frame
    private void Update()
    {
        float temp = _player.transform.position.x * (1 - parallaxEffect);
        float dist = _player.transform.position.x * parallaxEffect;

        transform.position = new Vector3(_startPos + dist, transform.position.y, transform.position.z);

        if (temp > _startPos + _length) _startPos += _length;
        else if (temp < _startPos + _length)
        {
            _startPos -= _length;
        }
    }
}
