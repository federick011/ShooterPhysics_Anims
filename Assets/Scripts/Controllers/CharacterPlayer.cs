using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterPlayer : MonoBehaviour
{
    public enum CharacterMovementState 
    {
        Stop,
        Move
    }

    [SerializeField]
    private C_AnimatorController animatorController;

    [SerializeField]
    private C_AnimatorController.AnimationStates currentAnimation = C_AnimatorController.AnimationStates.HouseDance;

    public CharacterMovementState currentMovementState = CharacterMovementState.Stop;

    public void ChangeAnimation(C_AnimatorController.AnimationStates animationStates) 
    {
        if (animatorController == null) return;

        currentAnimation = animationStates;

        animatorController.SetAnimation(animationStates);
    }

    private void OnEnable()
    {
        CharacterManager.changeAnimation += ChangeAnimation;
    }

    private void OnDisable()
    {
        CharacterManager.changeAnimation -= ChangeAnimation;
    }
}
