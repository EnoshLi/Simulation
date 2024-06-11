using UnityEngine;

/// <summary>
/// 物品的详细信息(ID,名字,类型,图标,描述,使用范围,是否可以拾取,是否可以丢弃,是否可以携带,价格,出售价格(几折))
/// </summary>
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
/**
 * 库存物品结构体。
 *
 * 该结构体用于封装库存中单个物品的信息，包括物品的唯一标识符（ID）和数量。
 * 它是库存管理系统中的基本单元，用于跟踪和管理玩家或游戏世界中的各项物品资源。
 *
 * @field itemID 物品唯一标识符，用于区分不同类型的物品，与游戏数据库或配置中的物品记录相对应。
 * @field itemAmount 物品数量，表示该类型物品在库存中的具体数量。
 */
[System.Serializable]
public struct InventoryItem
{
    public int itemID;     // 物品唯一ID
    public int itemAmount; // 物品数量
}

