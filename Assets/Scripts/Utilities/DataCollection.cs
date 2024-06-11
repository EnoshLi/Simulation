using UnityEngine;

//// <summary>
/// 物品详细信息类，封装了关于游戏内物品的所有详细属性，用于统一管理和序列化。
/// 包括ID、名称、类型、图标、地面显示图标、描述、使用范围、拾取/丢弃/携带状态以及价格和出售折扣等。
/// </summary>
[System.Serializable]
public class ItemDetails
{
    /// <summary>
    /// 物品唯一标识符。
    /// </summary>
    public int itemID;

    /// <summary>
    /// 物品的名称。
    /// </summary>
    public string itemName;
    
    /// <summary>
    /// 物品类型，使用枚举ItemType定义。
    /// </summary>
    public ItemType itemType;
    
    /// <summary>
    /// 物品的图标。
    /// </summary>
    public Sprite itemIcon;

    /// <summary>
    /// 物品在游戏世界中显示的图标（如地面上的图标）。
    /// </summary>
    public Sprite itemOnWorldSprite;
    
    /// <summary>
    /// 物品的描述信息。
    /// </summary>
    public string itemDescription;

    /// <summary>
    /// 物品的使用半径或有效范围。
    /// </summary>
    public int itemUseRadius;

    /// <summary>
    /// 是否允许该物品被玩家拾取。
    /// </summary>
    public bool canPickedup;
    
    /// <summary>
    /// 是否允许玩家丢弃该物品。
    /// </summary>
    public bool canDropped;

    /// <summary>
    /// 物品是否可以被玩家携带。
    /// </summary>
    public bool canCarried;

    /// <summary>
    /// 物品的购买价格。
    /// </summary>
    public int itemPrice;

    /// <summary>
    /// 出售该物品时的价格折扣百分比，范围0到1。
    /// </summary>
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

