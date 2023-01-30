using System.Collections;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody2D))]
public class RigidbodyClimb : MonoBehaviour, IClimb
{
    [SerializeField]
    private float speed;
    public float Speed { get => speed; set => speed = value; }

    private bool isClimbing;
    public bool IsClimbing { get => isClimbing; }

    private eClimbState climbState = eClimbState.Grounded;
    public eClimbState ClimbState { get => climbState; }

    public UnityEvent<int> EntityClimbEvent { get; } = new();

    private new Rigidbody2D rigidbody;
    private IGravitational gravityPhysics;
    private Ladder currentLadder;

    private void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        gravityPhysics = GetComponent<IGravitational>();
    }

    private void Update()
    {
        UpdateClimbingState();
    }

    public void HandleClimb(int dir, Ladder ladder)
    {
        if (climbState == eClimbState.Grounded) 
            MoveToLadder(ladder);

        switch (dir){
            case 1:
                climbState = eClimbState.ClimbingUp;
                rigidbody.velocity = new Vector3(rigidbody.velocity.x, speed);
                break;
            case -1:
                climbState = eClimbState.ClimbingDown;
                rigidbody.velocity = new Vector3(rigidbody.velocity.x, -speed);
                break;
            case 0:
                if(climbState == eClimbState.ClimbingDown || climbState == eClimbState.ClimbingUp)
                {
                    climbState = eClimbState.Hanging;
                    rigidbody.velocity = new Vector3(rigidbody.velocity.x, 0f);
                }  
                break;
        }
    }

    public void UpdateClimbingState()
    {
        switch (climbState)
        {
            case eClimbState.JumpingOff:
                isClimbing = false;
                if (gravityPhysics.IsGrounded())
                {
                    climbState = eClimbState.Landed;
                }
                break;
            case eClimbState.Landed:
                StartCoroutine(GroundedCoroutine());
                break;
            case eClimbState.Grounded:
                EntityClimbEvent.Invoke(0);
                isClimbing = false;
                break;
            case eClimbState.ClimbingUp:
                EntityClimbEvent.Invoke(1);
                isClimbing = true;
                break;
            case eClimbState.ClimbingDown:
                EntityClimbEvent.Invoke(-1);
                isClimbing = true;
                if (gravityPhysics.IsGrounded())
                {
                    climbState = eClimbState.Grounded;
                }
                break;
            case eClimbState.Hanging:
                EntityClimbEvent.Invoke(2);
                isClimbing = true;
                break;
        }
    }

    public void MoveToLadder(Ladder ladder)
    {
        currentLadder = ladder;
        transform.position = new Vector3(ladder.transform.position.x + 0.2f, transform.position.y, 0f);
        if (ladder.climbingSide == eDirection.Left) 
            GetComponent<ITurning>().Turn(eDirection.Right); 
        else GetComponent<ITurning>().Turn(eDirection.Left);
    }

    public void OnTriggerExit2D(Collider2D other)
    {
        if (other.GetComponent<Ladder>() != null && (climbState == eClimbState.Hanging || climbState == eClimbState.ClimbingUp || climbState == eClimbState.ClimbingDown))
        {
            climbState = eClimbState.JumpingOff;
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (currentLadder != null && collision.gameObject.Equals(currentLadder.topLadder))
        {
            if(GetComponent<ITurning>().Direction == eDirection.Right)
                transform.position = new Vector3(transform.position.x + 1f, transform.position.y, 0f);
            else transform.position = new Vector3(transform.position.x - 1f, transform.position.y, 0f);
            climbState = eClimbState.Grounded;
        }
    }

    public IEnumerator GroundedCoroutine()
    {
        yield return new WaitForSeconds(.04f);
        climbState = eClimbState.Grounded;
    }
}
