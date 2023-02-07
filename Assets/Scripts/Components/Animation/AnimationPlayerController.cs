using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationPlayerController : MonoBehaviour
{
    private Animator animator;
    private IMovement movement;
    private IJumping jump;
    private IWeapon weapon;
    private AbstractWeapon abstractWeapon;
    private IGravitational gravity;
    private IClimb climb;
    private IParry parry;
    private IRoll roll;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        movement = GetComponent<IMovement>();
        jump = GetComponent<IJumping>();
        weapon = GetComponent<IWeapon>();
        gravity = GetComponent<IGravitational>();
        climb = GetComponent<IClimb>();
        parry = GetComponent<IParry>();
        roll = GetComponent<IRoll>();
        abstractWeapon = GetComponent<AbstractWeapon>();
    }

    private void Start()
    {
        movement.EntityMoveEvent.AddListener(RunningAnimation);
        jump.EntityJumpEvent.AddListener(JumpingAnimation);
        jump.EntityJumpStateEvent.AddListener(JumpingStateAnimation);
        gravity.GravityFallEvent.AddListener(FallingAnimation);
        gravity.GravityFallStateEvent.AddListener(FallingStateAnimation);
        climb.EntityClimbEvent.AddListener(ClimbingAnimation);
        climb.EntityClimbStateEvent.AddListener(ClimbingStateAnimation);
        parry.StartParryEvent.AddListener(StartParryAnimation);
        parry.StopParryEvent.AddListener(StopParryAnimation);
        roll.StartRollEvent.AddListener(StartRollAnimation);
        roll.StopRollEvent.AddListener(StopRollAnimation);
        weapon.StartAttackEvent.AddListener(StartAttackAnimation);
        weapon.StopAttackEvent.AddListener(StopAttackAnimation);
        weapon.ReleaseAttackEvent.AddListener(ReleaseAttackAnimation);
    }

    private void Update()
    {
        AttackSeriesAnimation();
    }

    private void ReleaseAttackAnimation()
    {
        
    }

    private void StopAttackAnimation()
    {
        animator.SetBool("IsAttacking", false);
        animator.SetBool("Hit1", false);
        animator.SetBool("Hit2", false);
        animator.SetBool("Hit3", false);
    }

    private void StartAttackAnimation()
    {
        animator.SetBool("IsAttacking", true);
    }

    private void ClimbingStateAnimation(float arg0, float arg1)
    {
        animator.SetFloat("ClimbBlendX", arg0);
        animator.SetFloat("ClimbBlendY", arg1);
    }

    private void FallingStateAnimation(float num)
    {
        animator.SetFloat("FallBlend", num, 0.5f, Time.deltaTime * 15f);
    }

    private void FallingAnimation(bool isFalling)
    {
        animator.SetBool("IsFalling", isFalling);
    }

    private void StopRollAnimation()
    {
        animator.SetBool("IsRolling", false);
    }

    private void StartRollAnimation()
    {
        animator.SetBool("IsRolling", true);
    }

    private void RunningAnimation(bool isRunning)
    {
        animator.SetBool("IsRunning", isRunning);
    }

    private void JumpingAnimation(bool isJumping)
    {
        animator.SetBool("IsJumping", isJumping);
    }

    private void JumpingStateAnimation(float num)
    {
        animator.SetFloat("JumpBlend", num, 0.5f, Time.deltaTime * 15f);
    }

    private void AttackSeriesAnimation()
    {
        int count = abstractWeapon.attackSeries;
        if (count == 1)
        {
            animator.SetBool("Hit1", true);
        }    
        else if (count == 2 && animator.GetCurrentAnimatorStateInfo(0).IsName("Attack 1"))
        { 
            animator.SetBool("Hit2", true); 
        }   
        else if (count == 3 && animator.GetCurrentAnimatorStateInfo(0).IsName("Attack 2"))
        {
            animator.SetBool("Hit3", true);
        }
        else if (count != 0)
        {
            animator.SetBool("Hit1", true);
        }   
    }

    private void ClimbingAnimation(bool isClimbing)
    {
        animator.SetBool("IsClimbing", isClimbing);
    }

    private void StartParryAnimation()
    {
        animator.SetBool("IsParrying", true);
    }

    private void StopParryAnimation()
    {
        animator.SetBool("IsParrying", false);
    }
}
