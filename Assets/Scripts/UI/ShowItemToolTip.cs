using Keraz.Inventory;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

/*
命名空间：Keraz.Inventory
描述：此命名空间包含了与库存相关的功能实现。
*/
namespace Keraz.Inventory
{
    
// 使用 RequireComponent 特性确保此类挂载时必须有一个 SlotUI 组件。
    [RequireComponent(typeof(SlotUI))]
    public class ShowItemToolTip : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        // 私有变量，持有当前游戏对象的 SlotUI 组件实例。
        private SlotUI slotUI;

        // 提供对 InventoryUI 的便捷访问，利用属性实现懒汉式单例模式以优化性能。
        private InventoryUI inventoryUI => GetComponentInParent<InventoryUI>();

        /**
         * <summary>
         * 初始化方法，在脚本实例被唤醒时调用，确保所有依赖组件已就绪。
         * </summary>
         */
        private void Awake()
        {
            // 初始化 slotUI，获取此游戏对象上的 SlotUI 组件。
            slotUI = GetComponent<SlotUI>();
        }

        /**
         * <summary>
         * 实现 IPointerEnterHandler 接口，当鼠标指针进入此游戏对象时被调用。
         * 根据 SlotUI 中的物品数量显示或隐藏工具提示，并设置工具提示内容。
         * </summary>
         * <param name="eventData">包含事件数据的结构体</param>
         */
        public void OnPointerEnter(PointerEventData eventData)
        {
            // 激活工具提示并设置其内容
            if (slotUI.itemDetails.itemID != 0)
            {
                inventoryUI.ItemToolTip.gameObject.SetActive(true);
                //Debug.LogWarning(slotUI.itemAmount);
                
                inventoryUI.ItemToolTip.SetUpToolTip(slotUI.itemDetails, slotUI.slotType);
                
                //工具提示内容显示的位置 
                //重新设置工具物体的UI锚点
                inventoryUI.ItemToolTip.GetComponent<RectTransform>().pivot = new(0.5f,0);
                inventoryUI.ItemToolTip.transform.position = transform.position + Vector3.up * 60;
            }
            // 如果物品数量为0，则隐藏工具提示
            else
            {
                inventoryUI.ItemToolTip.gameObject.SetActive(false);
            }

        }

        /**
         * <summary>
         * 实现 IPointerExitHandler 接口，当鼠标指针离开此游戏对象时被调用。
         * 隐藏工具提示。
         * </summary>
         * <param name="eventData">包含事件数据的结构体</param>
         */
        public void OnPointerExit(PointerEventData eventData)
        {
            // 鼠标离开时，隐藏工具提示
            inventoryUI.ItemToolTip.gameObject.SetActive(false);
        }
    }
}


