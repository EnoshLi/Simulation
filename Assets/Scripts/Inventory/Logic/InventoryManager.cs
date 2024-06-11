using System.Linq; // 提供泛型集合类
using UnityEngine; // Unity引擎的核心命名空间

// 命名空间声明，组织与库存相关的代码逻辑
namespace Keraz.Inventory
{
    /**
     * InventoryManager是单例类，负责管理游戏中的所有库存相关操作，包括物品数据访问、背包管理等。
     * 
     * 【关键功能】
     * - 维护物品数据列表和玩家背包数据。
     * - 提供方法查询物品详情、向背包添加或交换物品。
     * - 检查背包容量、更新UI显示。
     * 
     * 【成员变量】
     * - `itemDataListSO`: 存储所有物品数据的ScriptableObject。
     * - `playerBag`: 玩家背包的ScriptableObject实例。
     * 
     * 【方法摘要】
     * - `Start`: 初始化时触发，更新玩家背包UI。
     * - `GetItemDetails`: 通过ID获取物品详细信息。
     * - `AddItem`: 将物品添加到玩家背包，并根据情况销毁物品游戏对象。
     * - `CheckBagCapacity`: 检查背包是否有空位。
     * - `GetItemIndexInBag`: 查找物品在背包中的索引。
     * - `AddItemAtIndex`: 在指定索引处添加或更新物品。
     * - `SwapItem`: 交换背包内两个物品的位置。
     */
    public class InventoryManager : Singleton<InventoryManager> // 继承自Singleton，确保全局唯一实例
    {
        [Header("物品数据")] // 编辑器中分类显示的标签
        public ItemDataListSO itemDataListSO; // 引用物品数据列表SO

        [Header("背包数据")] // 编辑器中分类显示的标签
        public InventoryBagSO playerBag; // 引用玩家背包数据SO

        /**
         * Unity启动时调用，用于初始化UI更新。
         */
        private void Start()
        {
            EventHandle.CallUpdateInventoryUI(InventoryLocation.PlayerBag, playerBag.ItemList); // 更新玩家背包UI
        }

        /**
         * 根据ID查找物品详细信息。
         * 
         * @param ID 物品的唯一ID。
         * @return 返回匹配的物品详细信息，未找到则为null。
         */
        public ItemDetails GetItemDetails(int ID)
        {
            return itemDataListSO.itemDetailsList.Find(i => i.itemID == ID);
        }

        /**
         * 向玩家背包添加物品并根据需要销毁物品游戏对象。
         * 
         * @param item 被添加的物品游戏对象。
         * @param toAdd 是否在添加后销毁物品游戏对象。
         */
        public void AddItem(Item item, bool toAdd)
        {
            // 查找物品在背包中的索引
            var index = GetItemIndexInBag(item.itemID);
            // 添加物品到背包特定索引位置
            AddItemAtIndex(item.itemID, index, 1);
            Debug.LogWarning(GetItemDetails(item.itemID).itemID + "Name" + GetItemDetails(item.itemID).itemName);
            // 根据标志销毁物品对象
            if (toAdd)
            {
                Destroy(item.gameObject);
            }
            // 触发UI更新
            EventHandle.CallUpdateInventoryUI(InventoryLocation.PlayerBag, playerBag.ItemList);
        }

        /**
         * 检查玩家背包是否有足够的空间容纳更多物品。
         * 
         * @return 背包有空位则返回true，否则false。
         */
        private bool CheckBagCapacity()
        {
            /*for (int i = 0; i < playerBag.ItemList.Count; i++)
            {
                if (playerBag.ItemList[i].itemID==0)
                {
                    return true;
                }
            }
            return false;*/
            return playerBag.ItemList.Any(i => i.itemID == 0);
        }

        /**
         * 获取背包中指定物品ID的索引位置。
         * 
         * @param ID 物品的ID。
         * @return 物品索引，未找到则返回-1。
         */
        private int GetItemIndexInBag(int ID)
        {
            return playerBag.ItemList.FindIndex(i => i.itemID == ID);
        }
        
        /**
         * 在指定索引处增加或更新背包中的物品。
         * 
         * @param ID 物品ID。
         * @param Index 物品在背包中的索引位置。
         * @param amount 增加的数量。
         */

        #region 代码优化前
        /*private void AddItemAtIndex(int ID,int Index,int amount)
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
        }*/
        

        #endregion

        #region 优化后
        private void AddItemAtIndex(int ID, int Index, int amount)
        {
            // 如果背包没有该物品且有空位，则插入新物品
            if (Index == -1 && CheckBagCapacity())
            {
                InsertNewItemIntoFirstEmptySlot(ID, amount);
            }
            // 如果背包已有该物品，则更新其数量
            else if (Index != -1)
            {
                UpdateExistingItemAtIndex(Index, ID, amount);
            }
        }

        /**
         * 在背包的第一个空位插入新物品。
         * 
         * @param ID 物品ID。
         * @param amount 数量。
         */
        private void InsertNewItemIntoFirstEmptySlot(int ID, int amount)
        {
            InventoryItem newItem = new() { itemID = ID, itemAmount = amount };
            int emptySlotIndex = playerBag.ItemList.FindIndex(i => i.itemID == 0);
            if (emptySlotIndex != -1)
            {
                playerBag.ItemList[emptySlotIndex] = newItem;
            }
        }

        /**
         * 更新指定索引处的现有物品数量。
         * 
         * @param index 索引位置。
         * @param ID 物品ID。
         * @param amount 新的数量。
         */
        private void UpdateExistingItemAtIndex(int index, int ID, int amount)
        {
            playerBag.ItemList[index] = new InventoryItem { itemID = ID, itemAmount = playerBag.ItemList[index].itemAmount + amount };
        }
        #endregion

        /**
         * 交换背包内两个指定索引位置的物品。
         * 
         * @param fromIndex 起始索引。
         * @param targetIndex 目标索引。
         */
        public void SwapItem(int fromIndex, int targetIndex)
        {
            // 仅在目标位置非空时交换，或清空起始位置并移动目标物品
            if (playerBag.ItemList[targetIndex].itemID != 0)
            {
                (playerBag.ItemList[fromIndex], playerBag.ItemList[targetIndex]) = (playerBag.ItemList[targetIndex], playerBag.ItemList[fromIndex]);
            }
            else
            {
                playerBag.ItemList[targetIndex] = playerBag.ItemList[fromIndex];
                playerBag.ItemList[fromIndex] = new InventoryItem();
            }
            // 更新UI
            EventHandle.CallUpdateInventoryUI(InventoryLocation.PlayerBag, playerBag.ItemList);
        }
    }
}

