using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using UnityEngine;

public static class EventHandle
{
    //更新背包栏的事件
    public static event Action<InventoryLocation, List<InventoryItem>> UpdateInventoryUI;

    //地图上生成物品的事件
    public static event Action<int, Vector3> InstantItemInScence;

    //物品选中事件
    public static event Action<ItemDetails, bool> ItemSelectedEvent;

    //时间改变的事件
    public static event Action<int, int> GameMinuteEvent;

    public static event Action<int, int, int, int, Season> GameDataEvent; 

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
    /// <summary>
    /// 调用游戏数据事件。
    /// </summary>
    /// <param name="hour">当前小时。</param>
    /// <param name="day">当前天数。</param>
    /// <param name="month">当前月份。</param>
    /// <param name="year">当前年份。</param>
    /// <param name="season">当前季节。</param>
    public static void CallGameDataEvent(int hour,int day,int month,int year,Season season)
    {
        // 使用null条件运算符安全地调用事件，避免在无订阅者时产生空指针异常
        GameDataEvent?.Invoke(hour,day,month,year,season);
    }
}