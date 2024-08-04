using UnityEngine;

public class PlayerHandsAnimations : ScriptableObject
{
    [SerializeField] private AnimationClip _handleLightWeaponClip;
    [SerializeField] private AnimationClip _handleCommonWeaponClip;
    [SerializeField] private AnimationClip _handleHeavyWeaponClip;

    public AnimationClip HandleLightWeaponClip => _handleLightWeaponClip;
    public AnimationClip HandleCommonWeaponClip => _handleCommonWeaponClip;
    public AnimationClip HandleHeavyWeaponClip => _handleHeavyWeaponClip;
}
