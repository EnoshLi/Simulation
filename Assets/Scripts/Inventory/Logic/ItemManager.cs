using UnityEngine;

// 命名空间声明，用于组织与库存物品管理相关的类和方法
namespace Keraz.Inventory
{
    /**
     * ItemManager负责处理游戏场景中物品的实例化与管理。
     * 它响应事件来在指定位置生成物品，并管理这些物品的父级变换。
     */
    public class ItemManager : MonoBehaviour
    {
        // 公有字段，用于存储作为模板的预制物品
        public Item itemPrefab;

        // 私有字段，保存物品实例化的父级变换对象
        private Transform itemParent;

        /**
         * 当此组件启用时，注册事件监听以便在场景中即时生成物品。
         */
        private void OnEnable()
        {
            EventHandle.InstantItemInScence += OnInstantItemInScence;
        }

        /**
         * 当此组件禁用时，注销事件监听。
         */
        private void OnDisable()
        {
            EventHandle.InstantItemInScence -= OnInstantItemInScence;
        }

        /**
         * Unity组件启动时调用，用于初始化物品父级变换对象。
         */
        private void Start()
        {
            // 通过标签查找场景中的"ItemParent"对象并获取其变换组件
            itemParent = GameObject.FindWithTag("ItemParent").transform;
        }

        /**
         * 根据接收到的物品ID和位置，在场景中实例化一个物品。
         *
         * @param ID 要实例化的物品的唯一标识符。
         * @param pos 物品在场景中的生成位置。
         */
        private void OnInstantItemInScence(int ID, Vector3 pos)
        {
            // 使用预制体在指定位置和方向实例化物品，并将其父级设为之前找到的itemParent
            var newItem = Instantiate(itemPrefab, pos, Quaternion.identity, itemParent);
            // 设置新实例化物品的ID
            newItem.itemID = ID;
        }
    }
}