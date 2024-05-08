using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Keraz.Inventory
{

    public class InventoryManager : Singleton<InventoryManager>
    {
        public ItemDataListSO itemDataListSO;
        
        //获取物品ID
        public ItemDetails GetItemDetails(int ID)
        {
            return itemDataListSO.itemDetailsList.Find(i => i.itemID == ID);
        }
    }
}
