using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class C_AnimatorController : MonoBehaviour
{
    public enum AnimationStates
    {
        None = 0,
        HouseDance,
        MacarenaDance,
        HipHopDance
    }

    [SerializeField]
    private Animator animatorController;
    

    public Dictionary<AnimationStates, string> animationStatesDic = new Dictionary<AnimationStates, string>() 
    {
        {AnimationStates.HouseDance, "House Dancing" },
        {AnimationStates.MacarenaDance, "Macarena Dance" },
        {AnimationStates.HipHopDance, "Wave Hip Hop Dance" }
    };

    private void Start()
    {
        SetAnimation(AnimationStates.HouseDance);
    }

    public void SetAnimation(AnimationStates animationState) 
    {
        if (animatorController == null) return;

        animatorController.Play(animationStatesDic[animationState]);
    }
}
