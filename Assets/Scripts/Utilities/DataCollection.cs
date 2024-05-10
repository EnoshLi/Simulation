using UnityEngine;

[System.Serializable]
//物品详细信息
public class ItemDetails
{
    public int itemID;

    public string itemName;
    
    //枚举类型
    public ItemType itemType;
    
    public Sprite itemIcon;

    public Sprite itemOnWorldSprite;
    
    public string itemDescription;

    public int itemUseRadius;

    public bool canPickedup;
    
    public bool canDropped;

    public bool canCarried;

    public int itemPrice;

    [Range(0, 1)]
    public float sellPercentage;
}

[System.Serializable]
//玩家背包
public struct InventoryItem
{
    public int itemID;
    public int itemAmount;
}
