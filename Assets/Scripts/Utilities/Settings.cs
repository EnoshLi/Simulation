using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Settings
{
    #region 物体渐入渐出
    
        //物体渐变透明的时间
        public const float itemFadeIn = 1f;
        
        //物体渐变透明度
        public const float itemTargetAlpha = 0.3f;
    
    #endregion

    #region 动画参数
    
        // 为了提高动画参数访问的效率，尤其是在循环或频繁调用的场景下，
        // 我们预先通过 Animator.StringToHash 方法将动画参数的名称转换为整数哈希值。
        // 这样做可以避免在每次访问参数时因字符串比较而造成的性能开销
        public static readonly int InputX = Animator.StringToHash("InputX");
        
        public static readonly int InputY = Animator.StringToHash("InputY");
        
        public static readonly int IsMoving = Animator.StringToHash("isMoving");
    

    #endregion

    #region 时间相关
    
        // 定义秒的阈值，用于判断时间间隔是否满足条件
        public const float secondThreshold = 0.00005f;
        
        // 定义秒的持续最大值，用于计时功能
        public const int secondHold = 59;
        
        // 定义分的持续最大值，用于计时功能
        public const int minuteHold = 59;
        
        // 定义小时的持续最大值，用于计时功能
        public const int hourHold = 23;
        
        // 定义天的持续最大值，用于计时功能
        public const int dayHold = 30;
        
        // 定义月的持续最大值，用于计时功能
        public const int monthHold = 12;
        
        // 定义季节的持续最大值，用于特定周期的计时或循环逻辑
        public const int seasonHold = 3;
        
    #endregion

    #region 场景切换的渐入渐出

    public const float fadeDuration = 1.5f;
    


    #endregion

}
