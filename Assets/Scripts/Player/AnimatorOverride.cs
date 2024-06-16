using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorOverride : MonoBehaviour
{
    private Animator[] animators; //存储人物的动画组件
    public SpriteRenderer holdItem; //人物搬运物品的图片
    public List<AnimatorType> animatorTypes; //人物的动画类型
    private Dictionary<string, Animator> animatorNameDict = new(); //存储不同的动画名称和动画组件

    private void Awake()
    {
        animators = GetComponentsInChildren<Animator>();
        //获取人物上的动画组件
        foreach (var animator in animators)
        {
            animatorNameDict.Add(animator.name, animator);
            //print(animator.name);
        }
    }

    private void OnEnable()
    {
        // 监听物品选择事件，注册事件处理器
        EventHandle.ItemSelectedEvent += OnItemSelectedEvent;
        EventHandle.BeforeSceneUnloadEvent += OnBeforeSceneUnloadEvent;
    }

    private void OnDisable()
    {
        // 监听物品选择事件，清理事件处理器
        EventHandle.ItemSelectedEvent -= OnItemSelectedEvent;
        //场景卸载前的触发事件
        EventHandle.BeforeSceneUnloadEvent += OnBeforeSceneUnloadEvent;
    }

    /// <summary>
    /// 在场景卸载前调用的事件处理程序。
    /// </summary>
    /// <remarks>
    /// 此方法的目的是在场景切换前对游戏对象的状态进行调整，确保场景切换过程中的平滑过渡。
    /// 它通过禁用holdItem对象和切换动画状态来实现这一点。
    /// </remarks>
    private void OnBeforeSceneUnloadEvent()
    {
        // 禁用holdItem游戏对象，以防止在场景切换过程中出现不期望的行为。
        holdItem.enabled = false;

        // 切换动画控制器的状态到None，为场景切换做准备。
        /// <param name="partType">表示要切换到的动画部分类型。</param>
        SwitchAnimator(PartType.None);
    }
    
    /// <summary>
    /// 当`ItemSelectedEvent`事件被触发时，此私有方法会被调用，以响应物品选中人物的动画变化。
    /// </summary>
    /// <param name="itemDetails">物品信息</param>
    /// <param name="selected">是否选中</param>
    private void OnItemSelectedEvent(ItemDetails itemDetails, bool selected)
    {
        //WORKFLOW: 根据物品类型，获取对应的动画类型(待续)
        PartType currentType = itemDetails.itemType switch
        {
            ItemType.Seed => PartType.Carry,
            ItemType.Commodity => PartType.Carry,
            _ => PartType.None
        };
        if (!selected)
        {
            currentType = PartType.None;
            holdItem.enabled = false;
        }
        else
        {
            //人物搬运状态
            if (currentType == PartType.Carry)
            {
                // 当选中且存在世界空间图标时，使用世界空间图标，当选中但不存在世界空间图标时，使用常规图标
                holdItem.sprite = itemDetails.itemOnWorldSprite != null
                    ? itemDetails.itemOnWorldSprite
                    : itemDetails.itemIcon;
                holdItem.enabled = true;
            }
        }

        // 当选中且存在世界空间图标时，使用世界空间图标，当选中但不存在世界空间图标时，使用常规图标
        holdItem.sprite = itemDetails.itemOnWorldSprite != null ? itemDetails.itemOnWorldSprite : itemDetails.itemIcon;
        //切换人物动画
        SwitchAnimator(currentType);
    }

    /// <summary>
    /// 人物动画切换
    /// </summary>
    /// <param name="partType"></param>
    private void SwitchAnimator(PartType partType)
    {
        // 遍历动画类型集合 `animatorTypes`，该集合可能存储了多个部件的动画配置信息。
        foreach (var item in animatorTypes)
        {
            // 检查当前循环项的部件类型 (`item.partType`) 是否与传入的目标部件类型 (`partType`) 匹配。
            if (item.partType == partType)
            {
                // 若匹配，则根据当前项的部件名称 (`item.partName`) 在 `animatorNameDict` 字典中查找对应的动画控制器，
                // 并将其 `runtimeAnimatorController` 属性设置为该配置项提供的动画覆盖控制器 (`item.animatorOverride`)。
                // 这样做可以动态改变角色或物体的特定部位的动画表现，例如在游戏内切换武器装备时改变角色的手部动画。
                animatorNameDict[item.partName.ToString()].runtimeAnimatorController = item.animatorOverride;
            }
        }
    }
}