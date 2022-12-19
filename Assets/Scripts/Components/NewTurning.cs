using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewTurning : MonoBehaviour
{
    [SerializeField]
    private GameObject turnGameObject;

    [SerializeField]
    private float turnSpeed;

    private eDirection direction = eDirection.Right;
    private Coroutine turnCoroutine;

    public eDirection TurnLeft()
    {
        if (direction == eDirection.Left) return eDirection.Left;
        if (turnCoroutine != null) StopCoroutine(turnCoroutine);
        turnCoroutine = StartCoroutine(TurnCoroutine(eDirection.Left));
        return eDirection.Left;
    }

    public eDirection TurnRight()
    {
        if (direction == eDirection.Right) return eDirection.Right;
        if (turnCoroutine != null) StopCoroutine(turnCoroutine);
        turnCoroutine = StartCoroutine(TurnCoroutine(eDirection.Right));
        return eDirection.Right;
    }

    private IEnumerator TurnCoroutine(eDirection direction)
    {
        this.direction = direction;

        float remainingAngle = (float)direction - turnGameObject.transform.rotation.eulerAngles.y;
        while ((direction == eDirection.Left) ? (remainingAngle > 0f) : (remainingAngle < 0f))
        {
            float deltaAngle = (direction == eDirection.Left) ? (turnSpeed * Time.deltaTime) : -(turnSpeed * Time.deltaTime);
            turnGameObject.transform.Rotate(new(0f, deltaAngle, 0f));
            remainingAngle -= deltaAngle;
            yield return null;
        }
        turnGameObject.transform.eulerAngles = new(0f, (float)direction, 0f);

        turnCoroutine = null;
    }
}
