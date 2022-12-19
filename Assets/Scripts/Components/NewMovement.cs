using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class NewMovement : MonoBehaviour
{
    [SerializeField]
    private float speed;

    private new Rigidbody rigidbody;

    private void Start()
    {
        rigidbody= GetComponent<Rigidbody>();
    }

    public void MoveRight()
    {
        rigidbody.velocity = new Vector3(speed, rigidbody.velocity.y, rigidbody.velocity.z);
    }

    public void MoveLeft()
    {
        rigidbody.velocity = new Vector3(-speed, rigidbody.velocity.y, rigidbody.velocity.z);
    }

    public void StopMoving()
    {
        rigidbody.velocity = new Vector3(0f, rigidbody.velocity.y, rigidbody.velocity.z);
    }
}
