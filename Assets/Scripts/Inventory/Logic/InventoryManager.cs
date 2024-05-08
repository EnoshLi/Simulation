using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Keraz.Inventory
{

    public class InventoryManager : Singleton<InventoryManager>
    {
        public ItemDataListSO itemDataListSO;
        
        /// <summary>
        /// 获取物品ID
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public ItemDetails GetItemDetails(int ID)
        {
            return itemDataListSO.itemDetailsList.Find(i => i.itemID == ID);
        }
        /// <summary>
        /// 物品添加到玩家背包
        /// </summary>
        /// <param name="item"></param>
        /// <param name="toAdd"></param>
        public void AddItem(Item item, bool toAdd)
        {
            Debug.LogWarning(GetItemDetails(item.itemID).itemID+"Name"+GetItemDetails(item.itemID).itemName);
            //添加的物品--把物品从地图上删除
            if (toAdd)
            {
                Destroy(item.gameObject);
            }
        }
    }
}
