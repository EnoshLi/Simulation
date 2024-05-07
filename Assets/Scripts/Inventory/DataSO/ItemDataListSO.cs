using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemDataListSO",menuName = "Inventory/ItemDataListSO")]
public class ItemDataListSO : ScriptableObject
{
    public List<ItemDetails> ItemDetailsList;
}
