using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.HID;

namespace Keraz.Inventory
{
    public class InventoryUI : MonoBehaviour
    {
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

        private void OpenBagUI(InputAction.CallbackContext obj)
        {
            OpenBagUI();
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
        /// 打开或者关闭背包
        /// </summary>
        public  void OpenBagUI()
        {
            bagOpened=!bagOpened;
            playerBagUI.SetActive(bagOpened);
        }
    }
}