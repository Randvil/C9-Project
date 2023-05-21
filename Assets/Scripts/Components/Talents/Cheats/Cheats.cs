using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Cheats : ITalent
{
    private IPlayerInput playerInput;
    private IEffectManager effectManager;
    private IModifierManager attackModifierManager;
    private IModifierManager defenceModifierManager;
    private Jump jump;

    private ISlowEffect moveSpeedEffect;
    private IDamageModifier increaseWeaponDamageModifier;
    private IDamageModifier decreaseTakenDamageModifier;
    private int initialJumpCount;

    private bool isMoveSpeedCheatActive;
    private bool isWeaponDamageCheatActive;
    private bool isTakenDamageCheatActive;
    private bool isCountlessJumpsCheatActive;

    public eTalentType TalentType => eTalentType.Cheats;
    public UnityEvent<eTalentType> LearnEvent { get; } = new();
    public UnityEvent<eTalentType> ForgetEvent { get; } = new();

    public Cheats(IPlayerInput playerInput, IEffectManager effectManager, IModifierManager attackModifierManager, IModifierManager defenceModifierManager, Jump jump)
    {
        this.playerInput = playerInput;
        this.effectManager = effectManager;
        this.attackModifierManager = attackModifierManager;
        this.defenceModifierManager = defenceModifierManager;
        this.jump = jump;

        SlowEffectData slowEffectData = (SlowEffectData)ScriptableObject.CreateInstance(typeof(SlowEffectData));
        slowEffectData.movementSlowValue = -4f;
        slowEffectData.duration = Mathf.Infinity;
        moveSpeedEffect = new SlowEffect(slowEffectData);

        increaseWeaponDamageModifier = new RelativeDamageModifier(9f);

        decreaseTakenDamageModifier = new RelativeDamageModifier(-1f);

        initialJumpCount = jump.MaxJumpCount;
    }

    public void Learn()
    {
        Debug.Log("Cheats are available. You can activate them with the keyboard shortcuts \"C\" + \"7\" ... \"C\" + \"0\", but you won't!");
        playerInput.ToggleMoveSpeedCheatEvent.AddListener(ToggleMoveSpeedCheat);
        playerInput.ToggleIncreaseWeaponDamageCheatEvent.AddListener(ToggleWeaponDamageCheat);
        playerInput.ToggleDecreaseTakenDamageCheatEvent.AddListener(ToggleTakenDamageCheat);
        playerInput.ToggleCountlessJumpsCheatEvent.AddListener(ToggleCounlessJumpsCheat);
    }

    public void Forget()
    {
        playerInput.ToggleMoveSpeedCheatEvent.RemoveListener(ToggleMoveSpeedCheat);
        playerInput.ToggleIncreaseWeaponDamageCheatEvent.RemoveListener(ToggleWeaponDamageCheat);
        playerInput.ToggleDecreaseTakenDamageCheatEvent.RemoveListener(ToggleTakenDamageCheat);
        playerInput.ToggleCountlessJumpsCheatEvent.RemoveListener(ToggleCounlessJumpsCheat);
    }

    private void ToggleMoveSpeedCheat()
    {
        if (isMoveSpeedCheatActive)
        {
            effectManager.RemoveEffect(moveSpeedEffect);
            Debug.Log("Move speed cheat deactivated.");
        }
        else
        {
            effectManager.AddEffect(moveSpeedEffect);
            Debug.Log("Move speed cheat activated.");
        }

        isMoveSpeedCheatActive = !isMoveSpeedCheatActive;
    }

    private void ToggleWeaponDamageCheat()
    {
        if (isWeaponDamageCheatActive)
        {
            attackModifierManager.RemoveModifier(increaseWeaponDamageModifier);
            Debug.Log("Attack damage cheat deactivated.");
        }
        else
        {
            attackModifierManager.AddModifier(increaseWeaponDamageModifier);
            Debug.Log("Attack damage cheat activated.");
        }

        isWeaponDamageCheatActive = !isWeaponDamageCheatActive;
    }

    private void ToggleTakenDamageCheat()
    {
        if (isTakenDamageCheatActive)
        {
            defenceModifierManager.RemoveModifier(decreaseTakenDamageModifier);
            Debug.Log("Invulnerability cheat deactivated.");
        }
        else
        {
            defenceModifierManager.AddModifier(decreaseTakenDamageModifier);
            Debug.Log("Invulnerability cheat activated.");
        }

        isTakenDamageCheatActive = !isTakenDamageCheatActive;
    }

    private void ToggleCounlessJumpsCheat()
    {
        if (isCountlessJumpsCheatActive)
        {
            jump.MaxJumpCount = initialJumpCount;
            Debug.Log("Countless jumps cheat deactivated.");
        }
        else
        {
            jump.MaxJumpCount = int.MaxValue;
            Debug.Log("Countless jumps cheat activated.");
        }

        isCountlessJumpsCheatActive = !isCountlessJumpsCheatActive;
    }
}
