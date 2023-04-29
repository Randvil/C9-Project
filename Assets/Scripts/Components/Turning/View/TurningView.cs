using System.Collections;
using UnityEngine;

public class TurningView
{
    private MonoBehaviour owner;
    private GameObject turnGameObject;
    private float turnSpeed;

    private Coroutine turnCoroutine;
    private eDirection newDirection;

    public TurningView(MonoBehaviour owner, GameObject turnGameObject, TurningViewData turningViewData, ITurning turning)
    {
        this.owner = owner;
        this.turnGameObject = turnGameObject;
        turnSpeed = turningViewData.turnSpeed;
        turning.TurnEvent.AddListener(Turn);
    }

    public void Turn(eDirection newDirection)
    {
        this.newDirection = newDirection;

        if (turnCoroutine != null)
        {
            owner.StopCoroutine(turnCoroutine);
        }

        turnCoroutine = owner.StartCoroutine(TurnCoroutine());
    }

    public IEnumerator TurnCoroutine()
    {
        while (Mathf.Approximately((float)newDirection, turnGameObject.transform.rotation.eulerAngles.y) == false)
        {
            float deltaAngle = (newDirection == eDirection.Left)
                ? (turnSpeed * Time.deltaTime)
                : -(turnSpeed * Time.deltaTime);

            float newPlayerRotation = turnGameObject.transform.rotation.eulerAngles.y + deltaAngle;
            if (newPlayerRotation < 0f || newPlayerRotation > 180f)
            {
                turnGameObject.transform.eulerAngles = new(turnGameObject.transform.eulerAngles.x, (float)newDirection, turnGameObject.transform.eulerAngles.z);
                break;
            }

            turnGameObject.transform.Rotate(new(0f, deltaAngle, 0f));

            yield return null;
        }
    }
}
