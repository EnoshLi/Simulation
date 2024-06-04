using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class EventHandle
{
    //更新装备UI
    public static event Action<InventoryLocation,List<InventoryItem>> UpdateInventoryUI;
    
    //地图上生成物品的事件
    public static  event Action<int,Vector3> InstantItemInScence;

    public static void CallUpdateInventoryUI(InventoryLocation location, List<InventoryItem> itemList)
    {
        UpdateInventoryUI?.Invoke(location,itemList);
    }
    public static void CallInstantItemInScence(int itemID,Vector3 position)
    {
        InstantItemInScence?.Invoke(itemID,position);
    }

}
