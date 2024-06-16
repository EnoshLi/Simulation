using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using UnityEngine;

public static class EventHandle
{
    //更新背包栏的事件
    public static event Action<InventoryLocation, List<InventoryItem>> UpdateInventoryUI;

    /**
     * 触发更新库存界面的事件方法。
     *
     * 此静态方法用于手动激发 `UpdateInventoryUI` 事件，从而通知所有订阅者
     * 更新其界面以反映最新的库存信息。它传递库存位置及当前库存物品列表，
     * 使得订阅者能够根据这些数据进行具体的UI更新操作。
     *
     * @param location 库存位置信息，用于定位到库存UI的具体展示区域。
     * @param itemList 当前库存物品的列表，包含每个物品的详细信息。
     */
    public static void CallUpdateInventoryUI(InventoryLocation location, List<InventoryItem> itemList)
    {
        // 使用null条件运算符安全地调用事件，避免在无订阅者时产生空指针异常
        UpdateInventoryUI?.Invoke(location, itemList);
    }

    //地图上生成物品的事件
    public static event Action<int, Vector3> InstantItemInScence;


    /**
     * 即时在场景中生成指定物品的方法。
     *
     * 该静态方法用于触发 `InstantItemInScene` 事件，目的是在游戏场景的指定位置
     * 立即实例化一个指定ID的物品。这通常用于响应玩家操作或游戏逻辑需求，
     * 如掉落物品、创建道具等。
     *
     * @param itemID 要生成的物品唯一标识符，用于识别具体的物品类型。
     * @param position 物品生成的三维坐标位置，决定物品在场景中的摆放位置。
     */
    public static void CallInstantItemInScence(int itemID, Vector3 position)
    {
        // 使用null条件运算符安全地调用事件，避免在无订阅者时产生空指针异常
        InstantItemInScence?.Invoke(itemID, position);
    }

    //物品选中事件
    public static event Action<ItemDetails, bool> ItemSelectedEvent;


    /**
     * 触发【物品选中状态变更】事件的方法。
     *
     * 通知系统中关心物品选中状态变化的组件，例如更新UI反馈或执行逻辑操作。
     *
     * @param itemDetails 变更选中状态的物品详细信息。
     * @param isSelected 物品当前是否被选中的布尔值。
     */
    public static void CallItemSelectedEvent(ItemDetails itemDetails, bool isSelected)
    {
        // 使用null条件运算符安全地调用事件，避免在无订阅者时产生空指针异常
        ItemSelectedEvent?.Invoke(itemDetails, isSelected);
    }

    //时间改变的事件
    public static event Action<int, int> GameMinuteEvent;


    /**
     * 触发【时间分钟变化】事件的方法。
     *
     * 通知系统中关心时间变化的组件，例如更新时间反馈或执行逻辑操作。
     *
     * @param minute 分钟
     * @param hour 小时。
     */
    public static void CallGameMinuteEvent(int minute, int hour)
    {
        // 使用null条件运算符安全地调用事件，避免在无订阅者时产生空指针异常
        GameMinuteEvent?.Invoke(minute, hour);
    }

    //日期改变时间
    public static event Action<int, int, int, int, Season> GameDataEvent;


    /// <summary>
    /// 调用游戏数据事件。
    /// </summary>
    /// <param name="hour">当前小时。</param>
    /// <param name="day">当前天数。</param>
    /// <param name="month">当前月份。</param>
    /// <param name="year">当前年份。</param>
    /// <param name="season">当前季节。</param>
    public static void CallGameDataEvent(int hour, int day, int month, int year, Season season)
    {
        // 使用null条件运算符安全地调用事件，避免在无订阅者时产生空指针异常
        GameDataEvent?.Invoke(hour, day, month, year, season);
    }

    //场景转换事件
    public static event Action<string, Vector3> TransitionEvent;

    /// <summary>
    /// 调用场景过渡事件。
    /// </summary>
    /// <param name="sceneName">目标场景的名称。</param>
    /// <param name="targetPosition">在目标场景中的目标位置。</param>
    /// <remarks>
    public static void CallTransitionEvent(string sceneName, Vector3 targetPosition)
    {
        // 使用null条件运算符安全地调用事件，避免在无订阅者时产生空指针异常
        TransitionEvent?.Invoke(sceneName, targetPosition);
    }

    /// <summary>
    /// 在场景卸载前触发的事件。
    /// </summary>
    public static event Action BeforeSceneUnloadEvent;

    /// <summary>
    /// 触发场景卸载前的事件。
    /// </summary>
    /// <remarks>
    /// 此方法用于安全地触发<see cref="BeforeSceneUnloadEvent"/>事件。如果没有任何订阅者，
    /// 则不会发生任何事情。使用null条件运算符(?.)可以避免在事件没有订阅者时引发空指针异常。
    /// </remarks>
    public static void CallBeforeSceneUnloadEvent()
    {
        // 使用null条件运算符安全地调用事件，避免在无订阅者时产生空指针异常
        BeforeSceneUnloadEvent?.Invoke();
    }

    /// <summary>
    /// 定义一个静态事件，用于在场景加载后触发回调。
    /// </summary>
    public static event Action AfterSceneLoadedEvent;

    /// <summary>
    /// 触发场景加载后的事件。
    /// </summary>
    /// <remarks>
    /// 此方法用于调用注册到AfterSceneLoadedEvent事件的所有回调函数。
    /// 它的存在是为了在场景加载完成后通知监听此事件的任何部分 of the application。
    /// </remarks>
    public static void CallAfterSceneLoadedEvent()
    {
        AfterSceneLoadedEvent?.Invoke();
    }

    /// <summary>
    /// 定义一个静态事件，用于触发人物移动到特定位置的操作。
    /// </summary>
    public static event Action<Vector3> MoveToPosition;

    /// <summary>
    /// 触发移动到指定位置的事件。
    /// </summary>
    /// <param name="position">目标位置的三维向量坐标。</param>
    public static  void CallMoveToPosition(Vector3 position)
    {
        // 使用条件运算符和Invoke方法安全地触发事件，避免如果事件为空时出现的null引用异常。
        MoveToPosition?.Invoke(position);
    }
}