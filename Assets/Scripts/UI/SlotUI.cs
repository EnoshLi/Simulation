using System;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;
using Button = UnityEngine.UI.Button;
using Image = UnityEngine.UI.Image;

namespace Keraz.Inventory
{
    public class SlotUI : MonoBehaviour,IPointerClickHandler,IBeginDragHandler,IDragHandler,IEndDragHandler
    {
        [Header("组件获取")] 
        [SerializeField] private Image slotImage;
        [SerializeField] private TextMeshProUGUI slotAmount;
        public Image slotHightlight;
        [SerializeField] private Button button;
        public SlotType slotType;
        public int slotIndex;

        public bool isSelected;

        //物品信息
        public ItemDetails itemDetails;
        public int itemAmount;
        private InventoryUI inventoryUI =>GetComponentInParent<InventoryUI>();

        private void Start()
        {
            isSelected = false;
            if (itemDetails.itemID == 0)
            {
                UpdateEmptySlot();
            }
        }

        

        /**
         * 更新物品槽信息的公共方法。
         *
         * 此方法用于刷新UI界面上单个物品槽的显示内容，根据传入的物品详情和数量来设置图标、文本以及其他交互属性。
         * 它使得物品槽能够正确反映当前所装备或存储的物品及其数量，同时激活或停用按钮交互状态。
         *
         * @param item ItemDetails 类型的参数，包含了物品的详细信息，如图标、名称等。
         * @param amount 整型参数，表示物品的数量，将被显示在物品槽旁的文字中。
         */
        public void UpdateSlot(ItemDetails item, int amount)
        {
            // 启用物品槽的图像显示
            slotImage.enabled = true;
    
            // 更新当前物品的详细信息
            itemDetails = item;
    
            // 设置物品槽显示的图标为传入物品的图标
            slotImage.sprite = item.itemIcon;
    
            // 更新物品的数量
            itemAmount = amount;
    
            // 将物品数量转换为字符串并显示在UI文本上
            slotAmount.text = amount.ToString();
    
            // 激活物品槽对应的按钮交互
            button.interactable = true;
        }


        /**
         * 更新空物品槽状态的公共方法。
         *
         * 此方法用于重置和禁用UI上的物品槽显示，当物品槽变为空闲或者需要取消选中状态时调用。
         * 它会隐藏物品图标、清空数量显示、禁用按钮交互，并可选地取消选中状态。
         *
         * 注意：如果物品槽之前被选中，此方法会先取消选中状态。
         */
        public void UpdateEmptySlot()
        {
            // 如果物品槽当前被选中，则取消选中状态
            if (isSelected)
            {
                isSelected = false;
            }
    
            // 禁用物品槽的图像显示，使其不可见
            slotImage.enabled = false;
    
            // 清空物品数量的文本显示
            slotAmount.text = string.Empty;
    
            // 禁用与该物品槽关联的按钮交互
            button.interactable = false;
        }

        /**
         * 处理鼠标指针点击事件的方法。
         *
         * 此方法响应UI元素上的指针点击事件，特别设计用于交互式物品槽。当用户点击该物品槽时，
         * 它会检查物品槽是否有物品（即物品数量是否大于0）。如果有物品，则会切换该槽的选中状态，
         * 并通知库存UI界面更新相应的高亮效果，以给予用户视觉反馈。
         *
         * @param eventData PointerEventData 类型的参数，包含了有关指针点击事件的所有数据。
         */
        public void OnPointerClick(PointerEventData eventData)
        {
            // 如果物品槽内物品数量为0，则直接返回，不做进一步处理
            if (itemAmount == 0)
            {
                return;
            }
    
            // 切换当前物品槽的选中状态
            isSelected = !isSelected;
    
            // 通知库存UI界面更新指定索引的物品槽高亮状态
            inventoryUI.UpdateSlotHighlight(slotIndex);
        }

        /**
         * 开始拖动事件的处理方法。
         *
         * 此方法在用户开始拖动物品槽时被调用，专为支持UI拖放功能设计。如果物品槽中有物品（物品数量不为0），
         * 它将启动拖放过程：启用拖放图像、设置拖放图像的图标为当前物品图标、调整图像大小至原生尺寸，
         * 并确保物品槽处于选中状态，同时更新库存UI中的槽位高亮效果，以指示拖动操作的来源。
         *
         * @param eventData PointerEventData 类型，封装了与拖动开始相关的事件数据。
         */
        public void OnBeginDrag(PointerEventData eventData)
        {
            // 检查物品槽内是否有物品
            if (itemAmount != 0)
            {
                // 启用拖放使用的图像组件
                inventoryUI.dragImge.enabled = true;
        
                // 设置拖放图像的Sprite为当前物品槽的图标
                inventoryUI.dragImge.sprite = slotImage.sprite;
        
                // 调整拖放图像的大小至其原始尺寸
                inventoryUI.dragImge.SetNativeSize();
        
                // 将当前物品槽设置为选中状态
                isSelected = true;
        
                // 更新库存UI中与此槽位相关的高亮效果
                inventoryUI.UpdateSlotHighlight(slotIndex);
            }
        }

        /**
         * 拖动进行中的处理方法。
         *
         * 在用户持续拖动UI元素期间，此方法不断被调用。它负责更新拖放图像的位置，使之跟随鼠标指针（或触控输入）的当前位置，
         * 从而实现平滑的拖动物理效果，让用户直观地看到拖动过程中的物品位置变化。
         *
         * @param eventData PointerEventData 类型，包含了当前拖动事件的相关数据，如鼠标位置等。
         */
        public void OnDrag(PointerEventData eventData)
        {
            // 实时更新拖放图像的位置为当前鼠标指针的位置
            inventoryUI.dragImge.transform.position = Input.mousePosition;
        }


        /**
         * 结束拖动事件的处理方法。
         *
         * 当用户完成拖动操作时，此方法被触发。它负责处理拖放结束时的各种逻辑，包括隐藏拖放图像、判断拖动目标的有效性，
         * 以及执行相应的交换物品或放置物品逻辑。具体功能包括：
         * - 隐藏拖放辅助图像。
         * - 检查拖动结束时指针所在的游戏对象是否有效，且是否为一个`SlotUI`组件。
         * - 若拖放目标有效，且源和目标均为背包槽位，调用`InventoryManager`进行物品交换。
         * - 最后，清除所有槽位的高亮状态。
         *
         * 注释还包含了一段未启用的代码，用于演示如何处理将物品丢弃到游戏世界中的逻辑，包括检查物品是否可丢弃及计算世界坐标。
         *
         * @param eventData PointerEventData 类型，包含了拖动结束时的事件数据，如释放时指针位置和命中对象等。
         */
        public void OnEndDrag(PointerEventData eventData)
        {
            // 隐藏拖放使用的图像
            inventoryUI.dragImge.enabled = false;
    
            // 检查拖动结束时指针是否指向有效游戏对象，并且该对象是否带有 SlotUI 组件
            if (eventData.pointerCurrentRaycast.gameObject != null && eventData.pointerCurrentRaycast.gameObject.GetComponent<SlotUI>() != null)
            {
                // 获取拖动目标的 SlotUI 组件
                var targetSlot = eventData.pointerCurrentRaycast.gameObject.GetComponent<SlotUI>();
                int targetIndex = targetSlot.slotIndex;

                // 若拖动发生在玩家背包内部，则执行物品交换
                if (slotType == SlotType.Bag && targetSlot.slotType == SlotType.Bag)
                {
                    InventoryManager.Instance.SwapItem(slotIndex, targetIndex);
                }
        
                // 清除所有槽位的高亮效果
                inventoryUI.UpdateSlotHighlight(-1);
            }
    
            /* 以下代码为示例，展示了如何实现将物品丢弃到游戏世界中的逻辑
            else
            {
                if (itemDetails.canDropped)
                {
                    // 计算鼠标位置对应的世界坐标
                    var pos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, -Camera.main.transform.position.z));
                    EventHandle.CallInstantItemInScence(itemDetails.itemID, pos);
                }
            }*/
        }

    }
}
