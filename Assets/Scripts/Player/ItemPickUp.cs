using UnityEngine;

namespace Keraz.Inventory
{
    /**
     * ItemPickUp脚本负责处理物品的拾取逻辑。
     * 当附着此脚本的游戏对象（通常是玩家或其他可以拾取物品的实体）与可交互的物品碰撞时，
     * 会触发物品拾取过程，并与库存管理系统互动。
     */
    public class ItemPickUp : MonoBehaviour
    {
        /**
         * OnTriggerEnter2D是Unity物理引擎的回调方法，当此游戏对象的碰撞器进入另一个2D碰撞器时被调用。
         *
         * @param other 与当前碰撞器发生接触的其他碰撞器的游戏对象引用。
         */
        private void OnTriggerEnter2D(Collider2D other)
        {
            // 尝试从碰撞的游戏对象中获取Item组件
            Item item = other.GetComponent<Item>();
            // 检查游戏对象是否带有Item组件
            if (item != null)
            {
                // 检查物品是否允许被拾取
                if (item.itemDetails.canPickedup)
                {
                    // 调用InventoryManager单例的AddItem方法拾取物品，并传递true指示应销毁物品对象
                    InventoryManager.Instance.AddItem(item, true);
                }
            }
        }
    }
}
