using DG.Tweening; 
using UnityEngine; 

/// <summary>
/// ItemFader脚本控制游戏物体（如房子、树木等）的淡入淡出效果，
/// 当游戏角色经过这些物体时，可以通过FadeOut方法使物体逐渐变透明，
/// 离开后通过FadeIn方法恢复物体的原始不透明度。
/// </summary>
public class ItemFader : MonoBehaviour
{
    private SpriteRenderer spriteRenderer; // 引用挂载物体上的SpriteRenderer组件，用于控制颜色（包括透明度）

    /**
     * Awake是Unity的生命周期方法，当脚本实例被创建时调用。
     * 初始化spriteRenderer以供后续方法使用。
     */
    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>(); // 获取当前游戏对象上的SpriteRenderer组件
    }
    
    /**
     * 使物体恢复到完全不透明状态。
     *
     * 使用DOTween库平滑过渡颜色至完全不透明（alpha值为1）。
     */
    public void FadeIn()
    {
        Color targetColor = new Color(1, 1, 1, 1); // 目标颜色为白色且完全不透明
        spriteRenderer.DOColor(targetColor, Settings.FadeIn); // 平滑过渡到目标颜色，持续时间由Settings.fadeIn定义
    }

    /**
     * 使物体逐渐变为透明。
     *
     * 根据Settings.targetAlpha设置目标透明度，使用DOTween平滑改变颜色。
     */
    public void FadeOut()
    {
        Color targetColor = new Color(1, 1, 1, Settings.TargetAlpha); // 目标颜色的alpha通道由Settings.targetAlpha决定
        spriteRenderer.DOColor(targetColor, Settings.FadeIn); // 平滑过渡到具有指定透明度的目标颜色，持续时间同样由Settings.fadeIn定义
    }
}