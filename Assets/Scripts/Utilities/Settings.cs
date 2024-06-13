using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Settings
{
    //物体渐变透明的时间
    public const float FadeIn = 1f;
    //物体渐变透明度
    public const float TargetAlpha = 0.3f;
    
    // 为了提高动画参数访问的效率，尤其是在循环或频繁调用的场景下，
    // 我们预先通过 Animator.StringToHash 方法将动画参数的名称转换为整数哈希值。
    // 这样做可以避免在每次访问参数时因字符串比较而造成的性能开销
    public static readonly int InputX = Animator.StringToHash("InputX");
    public static readonly int InputY = Animator.StringToHash("InputY");
    public static readonly int IsMoving = Animator.StringToHash("isMoving");
}
