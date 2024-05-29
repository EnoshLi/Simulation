using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class EventHandle
{
    public static event Action<InventoryLocation,List<InventoryItem>> UpdateInventoryUI;

    public static void CallUpdateInventoryUI(InventoryLocation location, List<InventoryItem> itemList)
    {
        UpdateInventoryUI?.Invoke(location,itemList);
    }

}
