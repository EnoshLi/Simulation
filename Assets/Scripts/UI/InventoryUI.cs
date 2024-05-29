using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;


namespace Keraz.Inventory
{
    
    public class InventoryUI : MonoBehaviour
    {
        [Header("拖拽图片")]
        public Image dragImge;
        
        [Header("玩家背包UI")]
        
        [SerializeField]private  GameObject playerBagUI;

        private bool bagOpened;
        
        [SerializeField]private  SlotUI[] playerSlots;
        
        private Simulation inputSystem;

        private void OnEnable()
        {
            EventHandle.UpdateInventoryUI+=OnUpdaetInventoryUI;
            inputSystem.Enable();
        }

        private void OnDisable()
        {
            EventHandle.UpdateInventoryUI-=OnUpdaetInventoryUI;
            inputSystem.Disable();
        }

        private void Awake()
        {
            inputSystem = new Simulation();
        }

        private void Start()
        {
            
            for (int i = 0; i < playerSlots.Length; i++)
            {
                playerSlots[i].slotIndex = i;
            }

            bagOpened = playerBagUI.activeInHierarchy;
        }

        private void Update()
        {
            inputSystem.UI.PlayerBag.started += OpenBagUI;
        }
       

        private void OnUpdaetInventoryUI(InventoryLocation location, List<InventoryItem> list)
        {
            switch (location)
            {
                case InventoryLocation.PlayerBag:
                    for (int i = 0; i < playerSlots.Length; i++)
                    {
                        if (list[i].itemAmount>0)
                        {
                            var item=InventoryManager.Instance.GetItemDetails(list[i].itemID);
                            playerSlots[i].UpdateSlot(item,list[i].itemAmount);
                        }
                        else
                        {
                            playerSlots[i].UpdateEmptySlot();
                        }
                    }
                    break;
            }
        }
        /// <summary>
        /// 打开或者关闭背包,手柄键盘调用
        /// </summary>
        /// <param name="obj"></param>
        private void OpenBagUI(InputAction.CallbackContext obj)
        {
            OpenBagUI();
        }
        /// <summary>
        /// 打开或者关闭背包,Button调用
        /// </summary>
        public  void OpenBagUI()
        {
            bagOpened=!bagOpened;
            playerBagUI.SetActive(bagOpened);
        }
        /// <summary>
        /// ActionBar高亮显示
        /// </summary>
        /// <param name="index"></param>
        public void UpdateSlotHighlight(int index)
        {
            foreach (var slot in playerSlots)
            {
                if (slot.isSelected && slot.slotIndex == index)
                {
                    slot.slotHightlight.gameObject.SetActive(true);
                }
                else
                {
                    slot.isSelected = false;
                    slot.slotHightlight.gameObject.SetActive(false);
                }
            }
        }
    }
}
