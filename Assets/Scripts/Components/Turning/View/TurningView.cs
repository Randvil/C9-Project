using UnityEngine;

public class TurningView : ITurningView
{
    private GameObject turnGameObject;

    private TurningViewData turningViewData;

    private ITurning turning;

    public TurningView(GameObject turnGameObject, TurningViewData turningViewData, ITurning turning)
    {
        this.turnGameObject = turnGameObject;

        this.turningViewData = turningViewData;

        this.turning = turning;
    }

    public void Turn()
    {
        if (Mathf.Approximately((float)turning.Direction, turnGameObject.transform.rotation.eulerAngles.y))
        {
            return;
        }

        float deltaAngle = (turning.Direction == eDirection.Left)
            ? (turningViewData.turnSpeed * Time.deltaTime)
            : -(turningViewData.turnSpeed * Time.deltaTime);

        float newPlayerRotation = turnGameObject.transform.rotation.eulerAngles.y + deltaAngle;
        if (newPlayerRotation < 0f || newPlayerRotation > 180f)
        {
            FinishTurn();
            return;
        }

        turnGameObject.transform.Rotate(new(0f, deltaAngle, 0f));
    }

    public void FinishTurn()
    {
        turnGameObject.transform.eulerAngles = new(turnGameObject.transform.eulerAngles.x, (float)turning.Direction, turnGameObject.transform.eulerAngles.z);
    }
}
