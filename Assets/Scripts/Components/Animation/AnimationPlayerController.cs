using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationPlayerController : MonoBehaviour
{
    private Animator animator;
    private IMovement movement;
    private IJumping jump;
    private IWeapon weapon;
    private IGravitational gravity;
    private IClimb climb;
    private void Awake()
    {
        animator = GetComponent<Animator>();
        movement = GetComponent<IMovement>();
        jump = GetComponent<IJumping>();
        weapon = GetComponent<IWeapon>();
        gravity = GetComponent<IGravitational>();
        climb = GetComponent<IClimb>();
    }

    private void Start()
    {
        movement.EntityMoveEvent.AddListener(RunningAnimation);
        jump.EntityJumpEvent.AddListener(JumpingAnimation);
        weapon.EntityAttackEvent.AddListener(SingleAttackAnimation);
        gravity.GravityFallEvent.AddListener(JumpingAnimation);
        climb.EntityClimbEvent.AddListener(ClimbingAnimation);
    }

    private void RunningAnimation(int num)
    {
        animator.SetInteger("Run", num);
    }

    private void JumpingAnimation(int num)
    {
        animator.SetInteger("Jump", num);
    }

    private void SingleAttackAnimation(int num)
    {
        animator.SetInteger("SingleAttack", num);
    }

    private void ClimbingAnimation(int num)
    {
        animator.SetInteger("Climb", num);
    }
}
