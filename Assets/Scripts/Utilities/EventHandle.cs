using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class EventHandle
{
    //更新背包栏的事件
    public static event Action<InventoryLocation,List<InventoryItem>> UpdateInventoryUI;
    
    //地图上生成物品的事件
    public static event Action<int,Vector3> InstantItemInScence;
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
        UpdateInventoryUI?.Invoke(location,itemList);
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
    public static void CallInstantItemInScence(int itemID,Vector3 position)
    {
        // 使用null条件运算符安全地调用事件，避免在无订阅者时产生空指针异常
        InstantItemInScence?.Invoke(itemID,position);
    }

}
