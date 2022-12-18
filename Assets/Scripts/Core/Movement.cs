using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement
{
    public Vector3 Move(float moveSpeed, Vector2 vector2, float y)
    {
        float deltaX = vector2.x * moveSpeed;
        return new Vector3(deltaX, y, 0f);
    }

    public Vector3 Jump(float initialJumpVelocity, float x, float moveSpeed)
    {
        return new Vector3(x * moveSpeed, initialJumpVelocity * .5f, 0f);
    }

    /*public void Turn(float deltaX, float direction, Coroutine turnCoroutine)
    {
        if (deltaX > 0f && direction != 0f)
        {
            direction = 0f;
            if (turnCoroutine != null) StopCoroutine(turnCoroutine);
            turnCoroutine = StartCoroutine(TurnCoroutine(direction));
        }
        else if (deltaX < 0f && direction != 180f)
        {
            direction = 180;
            if (turnCoroutine != null) StopCoroutine(turnCoroutine);
            turnCoroutine = StartCoroutine(TurnCoroutine(direction));
        }
    }*/

}
