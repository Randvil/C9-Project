using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[RequireComponent(typeof(NewMovement), typeof(NewJump), typeof(NewTurning))]
[RequireComponent(typeof(NewAttack), typeof(NewDamageInteraction))]
public class NewPlayerController : MonoBehaviour
{
    private eDirection direction = eDirection.Right;
    private bool alive = true;

    private new Collider collider;
    private NewMovement movement;
    private NewJump jump;
    private NewTurning turning;
    private NewAttack attack;
    private NewDamageInteraction damageInteraction;
    private PlayerInterface playerInterface;

    private void Start()
    {
        collider= GetComponent<Collider>();
        movement = GetComponent<NewMovement>();
        jump = GetComponent<NewJump>();
        turning = GetComponent<NewTurning>();
        attack = GetComponent<NewAttack>();
        damageInteraction = GetComponent<NewDamageInteraction>();
        playerInterface = GetComponent<PlayerInterface>();

        damageInteraction.DieEvent.AddListener(OnDie);
        damageInteraction.DamageEvent.AddListener(playerInterface.OnDamageTaken);
    }

    private void Update()
    {
        if (!alive) return;

        /*float moveInput = Input.GetAxisRaw("Horizontal");
        if (moveInput == 1f)
        {
            movement.MoveRight();
            direction = turning.TurnRight();

        }
        else if (moveInput == -1f)
        {
            movement.MoveLeft();
            direction = turning.TurnLeft();
        }
        else movement.StopMoving();

        float jumpInput = Input.GetAxisRaw("Jump");
        if (jumpInput == 1f)
        {
            jump.Jump();
        }

        if (Input.GetMouseButtonDown(0))
        {
            attack.StartAttack(direction);
        }*/
    }

    public void PlayerMove(Vector2 vector2)
    {
        if (vector2.x == 1f)
        {
            movement.MoveRight();
            direction = turning.TurnRight();

        }
        else if (vector2.x == -1f)
        {
            movement.MoveLeft();
            direction = turning.TurnLeft();
        }
        else movement.StopMoving();
    }

    public void PlayerJump()
    {
        jump.Jump();
    }

    public void PlayerAttack()
    {
        attack.StartAttack(direction);
    }

    private void OnDie()
    {
        //TODO: Добавить запуск анимации смерти, когда та будет готова
        collider.enabled = false;

        //TODO: После внедрения анимации, убрать это, чтоб труп оставался лежать, но удалить healthbar
        Destroy(gameObject, 3f);
    }
}
