using System;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

namespace Keraz.Inventory
{
    public class SlotUI : MonoBehaviour
    {
        [Header("组件获取")] [SerializeField] private Image slotImage;
        [SerializeField] private TextMeshProUGUI slotAmount;
        [SerializeField] private Image slotHightlight;
        [SerializeField] private Button button;
        public SlotType SlotType;
        public int slotIndex;

        public bool isSelected;

        //物品信息
        public ItemDetails itemDetails;
        public int itemAmount;

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
    }
}
