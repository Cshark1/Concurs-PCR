using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    [SerializeField] private GameObject _player;

    private void CameraFollowPlayer()
    {
        transform.position = new Vector3(_player.transform.position.x,transform.position.y,-10f);
    }

    // Update is called once per frame
    void Update()
    {
        CameraFollowPlayer();
    }
}
