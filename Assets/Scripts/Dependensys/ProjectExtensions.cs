using UnityEngine;

public static class ProjectExtensions
{
    public static bool IsLayerExist(this Animator animator, string layerName)
    {
        for (int i = 0; i < animator.layerCount; i++)
        {
            if (animator.GetLayerName(i) == layerName)
            {
                return true;
            }
        }

        return false;
    }
    
    public static bool TrySetLayerWeight(this Animator animator, string layerName, float layerWeight)
    {
        if (!animator.IsLayerExist(layerName))
        {
            Debug.LogError($"animator {animator.name} doesnt contains the layer {layerName}");

            return false;
        }

        int layerIndex = animator.GetLayerIndex(layerName);

        animator.SetLayerWeight(layerIndex, layerWeight);

        return true;
    }
}
