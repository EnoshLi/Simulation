//工具枚举
public enum ItemType
{
    //种子，日用品，家具，
    Seed,Commodity,Furniture,
    //锄头工具，砍树工具，砸石头工具，水壶工具，收集工具，
    HoeTool,ChopTool,BreakTool,WaterTool,CollectTool,
    //杂草
    ReapableScenery
}
/// <summary>
/// 背包枚举
/// </summary>
public enum SlotType
{
    Bag,Box,Shop
}
/// <summary>
/// 物品所在位置枚举
/// </summary>
public enum InventoryLocation
{
    PlayerBag,Box,
}
