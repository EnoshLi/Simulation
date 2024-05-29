using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Keraz.Inventory
{
    public class InventoryUI : MonoBehaviour
    {
        [SerializeField]private  SlotUI[] playerSlots;

        private void OnEnable()
        {
            EventHandle.UpdateInventoryUI+=OnUpdaetInventoryUI;
        }

        private void OnDisable()
        {
            EventHandle.UpdateInventoryUI-=OnUpdaetInventoryUI;
        }
        
        private void Start()
        {
            for (int i = 0; i < playerSlots.Length; i++)
            {
                playerSlots[i].slotIndex = i;
            }
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
    }
}
