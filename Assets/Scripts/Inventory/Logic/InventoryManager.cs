using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Keraz.Inventory
{

    public class InventoryManager : Singleton<InventoryManager>
    {
        [Header("物品数据")]
        public ItemDataListSO itemDataListSO;

        [Header("背包数据")] 
        public InventoryBagSO playerBag;

        private void Start()
        {
            EventHandle.CallUpdateInventoryUI(InventoryLocation.PlayerBag,playerBag.ItemList);
        }

        /// <summary>
        /// 获取物品ID
        /// </summary>
        /// <param name="ID"></param>
        /// <returns>返回物品的编号</returns>
        public ItemDetails GetItemDetails(int ID)
        {
            return itemDataListSO.itemDetailsList.Find(i => i.itemID == ID);
        }
        
        /// <summary>
        /// 物品添加到玩家背包
        /// </summary>
        /// <param name="item">物品</param>
        /// <param name="toAdd">bool值</param>
        public void AddItem(Item item, bool toAdd)
        {
           //是否有该物品
           var index = GetItemIndexInBag(item.itemID);
           //添加到背包
           AddItemAtIndex(item.itemID,index,1);
            Debug.LogWarning(GetItemDetails(item.itemID).itemID+"Name"+GetItemDetails(item.itemID).itemName);
            //添加的物品--把物品从地图上删除
            if (toAdd)
            {
                Destroy(item.gameObject);
            }
            //更新UI
            EventHandle.CallUpdateInventoryUI(InventoryLocation.PlayerBag,playerBag.ItemList);
        }
        
        /// <summary>
        /// 检查玩家背包是否有空位
        /// </summary>
        /// <returns></returns>
        private bool CheckBagCapacity()
        {
            for (int i = 0; i < playerBag.ItemList.Count; i++)
            {
                if (playerBag.ItemList[i].itemID==0)
                {
                    return true;
                }
            }
            return false;
        }
        
        /// <summary>
        /// 通过物品ID找到物品在背包中的索引
        /// </summary>
        /// <param name="ID">物品ID</param>
        /// <returns>-1 则表示背包没有该物品没有,返回index表示背包有该物品 并返回物品在背包的list的序号/returns>
        private int GetItemIndexInBag(int ID)
        {
            for (int index = 0; index < playerBag.ItemList.Count; index++)
            {
                if (playerBag.ItemList[index].itemID== ID)
                {
                    return index;
                }
            }

            return -1;
        }
        
        /// <summary>
        /// 在指定背包序号位置添加物品
        /// </summary>
        /// <param name="ID">物品ID</param>
        /// <param name="Index">序号</param>
        /// <param name="amount">数量</param>
        private void AddItemAtIndex(int ID,int Index,int amount)
        {
            //背包不存在该物品
            if (Index==-1 && CheckBagCapacity())
            {
                InventoryItem item = new()
                {
                    itemID = ID,
                    itemAmount = amount
                };
                for (int i = 0; i < playerBag.ItemList.Count; i++)
                {
                    if (playerBag.ItemList[i].itemID== 0)
                    {
                        playerBag.ItemList[i] = item;
                        break;
                    }
                }
            }
            //存在该物品
            else
            {
                int currentAmount = playerBag.ItemList[Index].itemAmount+amount;
                InventoryItem item = new()
                {
                    itemID = ID,
                    itemAmount = currentAmount
                };
                playerBag.ItemList[Index] = item;
            } 
        }
        /// <summary>
        /// 玩家背包范围物品交换
        /// </summary>
        /// <param name="fromIndex"></param>
        /// <param name="targetIndex"></param>
        public void    SwapItem(int fromIndex,int targetIndex)
        {
            InventoryItem currentItem = playerBag.ItemList[fromIndex];
            InventoryItem targetItem = playerBag.ItemList[targetIndex];
            if (targetItem.itemID!=0)
            {
                playerBag.ItemList[fromIndex] = targetItem;
                playerBag.ItemList[targetIndex] = currentItem;
            }
            else
            {
                playerBag.ItemList[targetIndex] = currentItem;
                playerBag.ItemList[fromIndex] = new InventoryItem();
            }
            EventHandle.CallUpdateInventoryUI(InventoryLocation.PlayerBag,playerBag.ItemList);
            
        }
    }
}
