using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    [SerializeField] private GameObject _player;
    [SerializeField] private float smoothTime = 0.3f;
    private Vector3 Velocity;

    private void CameraFollowPlayer()
    {
        Vector3 pos = transform.position;
        Vector3 playerPos = _player.transform.position;
        Vector3 SmoothPos = playerPos;
        pos = new Vector3(pos.x,_player.transform.position.y,-10f); 
        
        if (SmoothPos.y > 6.6f)
        {
            pos = new Vector3(pos.x,6.6f,-10f);
        }else if (SmoothPos.y < 1)
        {
            pos = new Vector3(pos.x,1f,-10f);
        }
        
        if (playerPos.x > -26.42f)
        {
            pos = new Vector3(playerPos.x ,pos.y,-10f);
        }
        SmoothPos = Vector3.SmoothDamp(transform.position, pos, ref Velocity, smoothTime);
        transform.position = new Vector3(pos.x,SmoothPos.y,-10f);
    }

    // Update is called once per frame
    void Update()
    {
        CameraFollowPlayer();
    }
}
