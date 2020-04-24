using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    [SerializeField] private GameObject _player;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void CameraFollowPlayer()
    {
        if (_player.transform.position.x >= 0)
        {
            transform.position = new Vector3(_player.transform.position.x,transform.position.y,-10f);
            return;
        }

        transform.position = new Vector3(0f,_player.transform.position.y,-10f);
    }

    // Update is called once per frame
    void Update()
    {
        CameraFollowPlayer();
    }
}
