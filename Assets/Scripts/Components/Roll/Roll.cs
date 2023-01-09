using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Roll : MonoBehaviour, IRoll
{
    [SerializeField]
    private float speed;

    [SerializeField]
    private float duration;

    private Coroutine rollCoroutine;
    private Vector2 initialColliderSize;
    private Vector2 initialColliderOffset;

    public bool IsRolling { get => rollCoroutine != null; }
    public UnityEvent StartRollEvent { get; } = new();
    public UnityEvent StopRollEvent { get; } = new();

    private new BoxCollider2D collider;
    private new Rigidbody2D rigidbody;

    private void Start()
    {
        collider = GetComponent<BoxCollider2D>();
        rigidbody = GetComponent<Rigidbody2D>();
    }

    public void StartRoll(eDirection direction)
    {
        if (IsRolling)
            return;

        rollCoroutine = StartCoroutine(RollCoroutine(direction));

        StartRollEvent.Invoke();
    }

    public void StopRoll()
    {
        if (!IsRolling)
            return;

        StopCoroutine(rollCoroutine);

        ResetRollConditions();

        StopRollEvent.Invoke();
    }

    private IEnumerator RollCoroutine(eDirection direction)
    {
        SetRollConditions(direction);

        if (duration > 0f)
            yield return new WaitForSeconds(duration);

        ResetRollConditions();

        StopRollEvent.Invoke();
    }

    private void SetRollConditions(eDirection direction)
    {
        initialColliderSize = collider.size;
        initialColliderOffset = collider.offset;
        collider.size = collider.size / 2f;
        collider.offset = new Vector2(collider.offset.x, collider.offset.y - collider.size.y / 2f);

        float directionalSpeed = direction == eDirection.Right ? speed : -speed;
        rigidbody.velocity = new Vector2(directionalSpeed, rigidbody.velocity.y);
    }

    private void ResetRollConditions()
    {
        rigidbody.velocity = new Vector2(0f, rigidbody.velocity.y);

        collider.offset = initialColliderOffset;
        collider.size = initialColliderSize;

        rollCoroutine = null;
    }
}
