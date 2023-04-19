using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestCameraController : MonoBehaviour
{
    [SerializeField]
    private Transform pointOfView;

    private void LateUpdate()
    {
        if (pointOfView != null)
        {
            transform.position = new(pointOfView.position.x, pointOfView.position.y, transform.position.z);
        }        
    }
}
