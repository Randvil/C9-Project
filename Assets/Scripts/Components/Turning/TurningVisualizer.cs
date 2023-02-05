using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TurningVisualizer : MonoBehaviour
{
    [SerializeField]
    private GameObject turnGameObject;

    [SerializeField]
    private float turnSpeed;

    private Coroutine turnCoroutine;

    private ITurning turning;

    private void Start()
    {
        turning= GetComponent<Turning>();

        turning.TurnEvent.AddListener(Turn);
    }

    public void Turn(eDirection direction)
    {
        if (turnCoroutine != null) StopCoroutine(turnCoroutine);
        turnCoroutine = StartCoroutine(TurnCoroutine(direction));
    }

    private IEnumerator TurnCoroutine(eDirection direction)
    {
        float remainingAngle = (float)direction - turnGameObject.transform.rotation.eulerAngles.y;
        while ((direction == eDirection.Left) ? (remainingAngle > 0f) : (remainingAngle < 0f))
        {
            float deltaAngle = (direction == eDirection.Left) ? (turnSpeed * Time.deltaTime) : -(turnSpeed * Time.deltaTime);
            turnGameObject.transform.Rotate(new(0f, deltaAngle, 0f));
            remainingAngle -= deltaAngle;
            yield return null;
        }
        turnGameObject.transform.eulerAngles = new(turnGameObject.transform.eulerAngles.x, (float)direction, turnGameObject.transform.eulerAngles.z);

        turnCoroutine = null;
    }
}
