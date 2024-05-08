using UnityEngine;

[System.Serializable]
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
