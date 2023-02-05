using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody2D))]
public class RigidbodyMovement : MonoBehaviour, IMovement
{
    [SerializeField]
    private float speed;
    public float Speed { get => speed; set => speed = value; }

    private bool isMoving;
    public bool IsMoving { get => isMoving; }

    public UnityEvent<int> EntityMoveEvent { get; } = new();

    private new Rigidbody2D rigidbody;

    private void Start()
    {
        rigidbody= GetComponent<Rigidbody2D>();
    }

    public void StartMove(eDirection direction)
    {
        EntityMoveEvent.Invoke(1);
        float directionalSpeed = direction == eDirection.Right ? speed : -speed;
        rigidbody.velocity = new Vector3(directionalSpeed, rigidbody.velocity.y);

        isMoving = true; 
    }

    public void StopMove()
    {
        rigidbody.velocity = new Vector3(0f, rigidbody.velocity.y);

        isMoving = false;
        EntityMoveEvent.Invoke(0);
    }    
}
