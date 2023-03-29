using System.Collections;
using UnityEngine;

public class Climb : IClimb
{
    private ClimbData climbData;

    private Rigidbody2D rigidbody;
    private IGravity gravity;
    private ITurning turning;

    private Coroutine searchCoroutine;
    private IClimbableObject climbableObject;

    public bool CanClimb => climbableObject != null;
    public bool IsClimbing { get; private set; }
    public bool HaveToTurn { get; private set; }

    public Climb(ClimbData climbData, Rigidbody2D rigidbody, IGravity gravity, ITurning turning)
    {
        this.climbData = climbData;
        this.rigidbody = rigidbody;
        this.gravity = gravity;
        this.turning = turning;

        searchCoroutine = Coroutines.StartCoroutine(SearchClimbableObject());
    }

    private IEnumerator SearchClimbableObject()
    {
        yield return new WaitForSeconds(climbData.fallProhibitionPeriod);

        while (true)
        {
            yield return new WaitForSeconds(climbData.searchPeriod);

            Collider2D[] colliders = Physics2D.OverlapCircleAll(rigidbody.position, climbData.searchRadius, climbData.climbableObjectLayer);
            if (colliders.Length == 0)
            {
                IsClimbing = false;
                continue;
            }

            foreach (Collider2D collider in colliders)
            {
                if (collider.TryGetComponent(out climbableObject) == true)
                {
                    IsClimbing = true;
                    break;
                }
            }
        }
    }

    public void StartClimb()
    {
        //IsClimbing = true;
        gravity.Disable(this);
        rigidbody.MovePosition(new(climbableObject.XPosition, rigidbody.position.y));
        rigidbody.velocity = new(0f, 0f);

        HaveToTurn = false;
        if (turning.Direction != climbableObject.PlayerDirection)
        {
            HaveToTurn = true;
        }
    }

    public void BreakClimb()
    {
        IsClimbing = false;
        gravity.Enable(this);
        rigidbody.velocity = new(0f, 0f);
        climbableObject = null;

        Coroutines.StopCoroutine(ref searchCoroutine);
        searchCoroutine = Coroutines.StartCoroutine(SearchClimbableObject());
    }

    public void ClimbUp()
    {
        rigidbody.velocity = new(rigidbody.velocity.x, climbData.speed);
    }

    public void ClimbDown()
    {
        rigidbody.velocity = new(rigidbody.velocity.x, -climbData.speed);
    }

    public void StopClimb()
    {
        rigidbody.velocity = new(rigidbody.velocity.x, 0f);
    }
}
