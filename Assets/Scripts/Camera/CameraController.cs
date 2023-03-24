using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    private Transform player;

    private Vector3 offset;

    private void Start()
    {
        offset = new Vector3(0f, 0.63f, -2.81f);
    }

    private void LateUpdate()
    {
        if(player != null)
            transform.position = offset + new Vector3(player.position.x, player.position.y, 0f);
    }
}
