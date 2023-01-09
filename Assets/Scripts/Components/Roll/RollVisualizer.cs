using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RollVisualizer : MonoBehaviour
{
    [SerializeField]
    private GameObject rollingGameObject;

    [SerializeField]
    private float rollSpeed;

    private Vector3 initialScale;
    private float initialAngle;
    private Coroutine rollCoroutine;

    private IRoll roll;

    private void Start()
    {
        roll = GetComponent<IRoll>();

        roll.StartRollEvent.AddListener(OnStartRoll);
        roll.StopRollEvent.AddListener(OnStopRoll);
    }

    private void OnStartRoll()
    {
        if (rollCoroutine != null)
            return;

        rollCoroutine = StartCoroutine(RollCoroutine());
    }

    private void OnStopRoll()
    {
        StopCoroutine(rollCoroutine);
        ResetRollConditions();
        rollCoroutine = null;
    }

    private IEnumerator RollCoroutine()
    {
        initialScale = rollingGameObject.transform.localScale;
        initialAngle = rollingGameObject.transform.eulerAngles.z;

        rollingGameObject.transform.localScale /= 1.5f;

        while (true)
        {
            Vector3 newRotation = new(0f, rollingGameObject.transform.eulerAngles.y, rollingGameObject.transform.eulerAngles.z - rollSpeed * Time.deltaTime);
            rollingGameObject.transform.eulerAngles = newRotation;

            yield return null;
        }
    }

    private void ResetRollConditions()
    {
        rollingGameObject.transform.localScale = initialScale;
        rollingGameObject.transform.eulerAngles = new(rollingGameObject.transform.eulerAngles.x, rollingGameObject.transform.eulerAngles.y, initialAngle);
    }
}
