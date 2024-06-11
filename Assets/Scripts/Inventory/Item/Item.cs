using UnityEngine; // 引入Unity引擎的核心命名空间

// 命名空间声明，组织代码逻辑
namespace Keraz.Inventory
{
    /**
     * Item类表示游戏世界中的一个可交互物品对象，继承自MonoBehaviour，用于处理物品的渲染、碰撞及初始化逻辑。
     * 
     * 【关键功能】
     * - **初始化物品**: 根据提供的物品ID设置物品的外观、碰撞大小等属性。
     * - **渲染管理**: 动态调整SpriteRenderer以匹配物品的图标或世界中的展示精灵。
     * - **碰撞调整**: 根据物品精灵的实际尺寸调整BoxCollider2D的尺寸和位置，确保碰撞准确。
     * 
     * 【使用场景】
     * - 场景中掉落的物品对象。
     * - 可拾取或互动的环境元素。
     * 
     * 【重要成员】
     * - `itemID`: 物品的唯一标识符。
     * - `spriteRenderer`: 控制物品外观的SpriteRenderer组件。
     * - `coll`: 物品碰撞的BoxCollider2D组件。
     * - `itemDetails`: 存储物品详细信息的对象，从InventoryManager获取。
     */
    public class Item : MonoBehaviour
    {
        public int itemID; // 物品的ID，用于识别具体的物品类型

        private SpriteRenderer spriteRenderer; // 物品的渲染组件
        private BoxCollider2D coll; // 物品的碰撞组件
        public ItemDetails itemDetails; // 物品的详细描述信息

        /**
         * Awake是Unity生命周期方法，当脚本实例被载入时调用。
         * 初始化渲染器和碰撞组件的引用。
         */
        private void Awake()
        {
            spriteRenderer = GetComponentInChildren<SpriteRenderer>(); // 获取子对象中的SpriteRenderer
            coll = GetComponent<BoxCollider2D>(); // 获取自身的BoxCollider2D组件
        }

        /**
         * Start是Unity生命周期方法，在Awake之后，第一帧更新前调用。
         * 如果物品ID非零，则调用Init方法进行物品的详细初始化。
         */
        private void Start()
        {
            if (itemID != 0)
            {
                Init(itemID); // 物品ID有效时初始化物品信息
            }
        }

        /**
         * 初始化物品的外观和物理属性。
         * 
         * @param ID 物品的唯一ID，用于从InventoryManager获取物品详情。
         */
        public void Init(int ID)
        {
            itemID = ID; // 设置物品ID

            // 从InventoryManager获取物品详细信息
            itemDetails = InventoryManager.Instance.GetItemDetails(itemID);

            if (itemDetails != null)
            {
                // 根据物品详情设置SpriteRenderer的Sprite
                spriteRenderer.sprite = itemDetails.itemOnWorldSprite ?? itemDetails.itemIcon;

                // 根据Sprite的尺寸调整BoxCollider2D的大小和位置
                Vector2 newSize = new Vector2(spriteRenderer.sprite.bounds.size.x, spriteRenderer.sprite.bounds.size.y);
                coll.size = newSize;
                coll.offset = new Vector2(0, spriteRenderer.sprite.bounds.center.y);
            }
        }
    }
}
