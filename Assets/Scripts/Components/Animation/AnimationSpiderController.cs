using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationSpiderController : MonoBehaviour
{
    private Animator animator;
    private IMovement movement;
    private IWeapon weapon;
    private void Awake()
    {
        animator = GetComponent<Animator>();
        movement = GetComponent<IMovement>();
        weapon = GetComponent<IWeapon>();
    }

    private void Start()
    {
        movement.EntityMoveEvent.AddListener(RunningAnimation);
        weapon.StartAttackEvent.AddListener(StartAttackAnimation);
        weapon.StopAttackEvent.AddListener(StopAttackAnimation);
        weapon.ReleaseAttackEvent.AddListener(ReleaseAttackAnimation);
    }

    private void RunningAnimation(bool isRunning)
    {
        animator.SetBool("IsRunning", isRunning);
    }

    private void ReleaseAttackAnimation()
    {

    }

    private void StopAttackAnimation()
    {
        animator.SetBool("IsAttacking", false);
    }

    private void StartAttackAnimation()
    {
        animator.SetBool("IsAttacking", true);
    }
}
