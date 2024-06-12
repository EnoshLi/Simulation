using System.Collections.Generic; 
using UnityEngine; 
using UnityEngine.InputSystem; 
using UnityEngine.UI; 
/*
 * 该脚本定义了一个名为InventoryUI的类，负责管理游戏UI中的背包界面逻辑，包括物品显示、拖放交互、背包开关控制及槽位高亮等功能。
 * 使用了Unity引擎和UnityEngine.InputSystem进行输入处理，以及自定义的EventHandle和InventoryManager类来同步库存数据。
 */
// 命名空间声明，组织代码逻辑
namespace Keraz.Inventory
{
    /**
     * InventoryUI类管理游戏界面中的背包功能，包括显示、交互逻辑等。
     */
    public class InventoryUI : MonoBehaviour
    {
        public ItemToolTip ItemToolTip; // 声明一个类型为ItemToolTip的公共变量，用于引用ItemToolTip组件实例
        
        // 属性和字段声明
        [Header("拖拽图片")] // Unity编辑器中分类显示的标签
        public Image dragImge; // 拖放时显示的图像组件

        [Header("玩家背包UI")] // 编辑器UI分组标签
        [SerializeField] private GameObject playerBagUI; // 玩家背包UI的GameObject引用

        private bool bagOpened; // 背包是否开启的状态标志

        [SerializeField] private SlotUI[] playerSlots; // 玩家背包UI中的槽位数组

        private Simulation inputSystem; // 输入模拟系统实例，用于处理手柄或键盘输入

        // 生命周期方法
        private void OnEnable()
        {
            // 监听库存UI更新事件，注册事件处理器
            EventHandle.UpdateInventoryUI += OnUpdateInventoryUI;
            // 启用输入系统
            inputSystem.Enable();
        }

        private void OnDisable()
        {
            // 清理事件监听，避免内存泄漏
            EventHandle.UpdateInventoryUI -= OnUpdateInventoryUI;
            // 禁用输入系统
            inputSystem.Disable();
        }

        private void Awake()
        {
            // 初始化输入系统实例
            inputSystem = new Simulation();
        }

        private void Start()
        {
            // 初始化每个槽位的索引
            for (int i = 0; i < playerSlots.Length; i++)
            {
                playerSlots[i].slotIndex = i;
            }

            // 初始状态下背包是否开启
            bagOpened = playerBagUI.activeInHierarchy;
        }

        private void Update()
        {
            // 监听打开背包的输入事件
            inputSystem.UI.PlayerBag.started += OpenBagUI;
        }

        // 方法定义
        /**
         * 根据库存位置和物品列表更新UI显示。
         * 
         * @param location 库存位置枚举
         * @param list 物品列表，包含物品ID和数量
         */
        private void OnUpdateInventoryUI(InventoryLocation location, List<InventoryItem> list)
        {
            switch (location)
            {
                case InventoryLocation.PlayerBag:
                    // 遍历玩家背包槽位，根据物品列表更新显示
                    for (int i = 0; i < playerSlots.Length; i++)
                    {
                        if (list[i].itemAmount > 0)
                        {
                            // 获取物品详细信息并更新槽位显示
                            var item = InventoryManager.Instance.GetItemDetails(list[i].itemID);
                            playerSlots[i].UpdateSlot(item, list[i].itemAmount);
                        }
                        else
                        {
                            // 槽位无物品时更新为空槽
                            playerSlots[i].UpdateEmptySlot();
                        }
                    }
                    break;
            }
        }

        /**
         * 处理打开或关闭背包的输入事件，支持手柄和键盘调用。
         */
        private void OpenBagUI(InputAction.CallbackContext obj)
        {
            OpenBagUI(); // 调用统一的背包开关方法
        }

        /**
         * 公共方法，用于按钮调用来打开或关闭背包界面。
         */
        public void OpenBagUI()
        {
            // 切换背包开启状态并同步UI显示
            bagOpened = !bagOpened;
            playerBagUI.SetActive(bagOpened);
        }

        /**
         * 更新槽位的高亮显示，用于视觉反馈。
         * 
         * @param index 被操作的槽位索引
         */
        public void UpdateSlotHighlight(int index)
        {
            // 遍历所有槽位，根据选择状态更新高亮
            foreach (var slot in playerSlots)
            {
                if (slot.isSelected && slot.slotIndex == index)
                {
                    slot.slotHightlight.gameObject.SetActive(true); // 高亮显示
                }
                else
                {
                    slot.isSelected = false; // 取消选中状态
                    slot.slotHightlight.gameObject.SetActive(false); // 关闭高亮
                }
            }
        }
    }
}
