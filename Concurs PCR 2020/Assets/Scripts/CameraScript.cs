using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    [SerializeField] private GameObject _player;

    private void CameraFollowPlayer()
    {
        Vector3 pos = new Vector3(_player.transform.position.x,transform.position.y,-10f);
        if (pos.x > -26.42f)
        {
            transform.position = pos;
        }
    }

    // Update is called once per frame
    void Update()
    {
        CameraFollowPlayer();
    }
}
