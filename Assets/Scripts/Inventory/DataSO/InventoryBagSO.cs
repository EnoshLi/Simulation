using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "InventoryBagSO",menuName = "Inventory/InventoryBagSO")]
public class InventoryBagSO : ScriptableObject
{
    public List<InventoryItem> ItemList;
}
