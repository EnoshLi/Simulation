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

        

        /// <summary>
        /// 更新格子UI和信息
        /// </summary>
        /// <param name="item">ItemDetials</param>
        /// <param name="amount">数量</param>
        public void UpdateSlot(ItemDetails item, int amount)
        {
            slotImage.enabled = true;
            itemDetails = item;
            slotImage.sprite = item.itemIcon;
            itemAmount = amount;
            slotAmount.text = amount.ToString();
            button.interactable = true;
        }

        /// <summary>
        /// 将slot更新为空
        /// </summary>
        public void UpdateEmptySlot()
        {
            if (isSelected)
            {
                isSelected = false;
            }

            slotImage.enabled = false;
            slotAmount.text = string.Empty;
            button.interactable = false;
        }
        /// <summary>
        /// 点击装备栏(ActionBar)高亮显示
        /// </summary>
        /// <param name="eventData"></param>
        /// <exception cref="NotImplementedException"></exception>
        public void OnPointerClick(PointerEventData eventData)
        {
            if (itemAmount==0)
            {
                return;
            }

            isSelected = !isSelected;
            inventoryUI.UpdateSlotHighlight(slotIndex);
        }
        /// <summary>
        /// 开始退拽
        /// </summary>
        /// <param name="eventData"></param>
        public void OnBeginDrag(PointerEventData eventData)
        {
            if (itemAmount!=0)
            {
                inventoryUI.dragImge.enabled = true;
                inventoryUI.dragImge.sprite = slotImage.sprite;
                inventoryUI.dragImge.SetNativeSize();
                isSelected = true;
                inventoryUI.UpdateSlotHighlight(slotIndex);
            }
        }
        /// <summary>
        /// 正在拖拽
        /// </summary>
        /// <param name="eventData"></param>
        public void OnDrag(PointerEventData eventData)
        {
            inventoryUI.dragImge.transform.position = Input.mousePosition;
        }

        /// <summary>
        /// 拖拽结束
        /// </summary>
        /// <param name="eventData"></param>
        public void OnEndDrag(PointerEventData eventData)
        {
            inventoryUI.dragImge.enabled = false;
            //Debug.LogWarning(eventData.pointerCurrentRaycast.gameObject.name);
            if (eventData.pointerCurrentRaycast.gameObject != null)
            {
                if (eventData.pointerCurrentRaycast.gameObject.GetComponent<SlotUI>() == null)
                    return;

                var targetSlot = eventData.pointerCurrentRaycast.gameObject.GetComponent<SlotUI>();
                int targetIndex = targetSlot.slotIndex;

                //在Player自身背包范围内交换
                if (slotType == SlotType.Bag && targetSlot.slotType == SlotType.Bag)
                {
                    InventoryManager.Instance.SwapItem(slotIndex, targetIndex);
                }

                //清空所有高亮显示
                inventoryUI.UpdateSlotHighlight(-1);
            }
            /*else    //测试扔在地上
            {
                if (itemDetails.canDropped)
                {
                    //鼠标对应世界坐标
                    var pos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, -Camera.main.transform.position.z));
                    EventHandle.CallInstantItemInScence(itemDetails.itemID,pos);
                }
            }*/
        }
    }
}
