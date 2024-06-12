/**
 * 物品种类枚举。
 * 
 * 本枚举列出了游戏中不同类型的物品，主要分为四大类：植物种子、日常消耗品、家具、工具以及可收获的场景装饰。
 * 每个类型对应游戏中具体的一系列物品，用于逻辑区分和管理。
 * 
 * @enum Seed 植物种子，用于种植获取作物或其他植物资源。
 * @enum Commodity 日用品，包括但不限于食物、药品等，满足游戏角色的基本需求。
 * @enum Furniture 家具，装饰和功能性家具，用于布置家园或提供特殊功能。
 * @enum HoeTool 锄头工具，用于耕作土地，准备播种环境。
 * @enum ChopTool 砍树工具，用于采集木材，砍伐树木。
 * @enum BreakTool 砸石头工具，用于开采石头或矿物资源。
 * @enum WaterTool 水壶工具，用于浇灌作物，保证植物生长。
 * @enum CollectTool 收集工具，泛指各类用于收集特定资源的工具。
 * @enum ReapableScenery 可收获的场景装饰，指游戏中可互动并获取资源的装饰性物品，如草丛、果树等。
 */
public enum ItemType
{
    /// <summary>
    /// 种子
    /// </summary>
    Seed,
    
    /// <summary>
    /// 日用品
    /// </summary>
    Commodity, 
    
    /// <summary>
    /// 家具
    /// </summary>
    Furniture, 
    
    /// <summary>
    /// 锄头工具
    /// </summary>
    HoeTool, 
    
    /// <summary>
    /// 砍树工具
    /// </summary>
    ChopTool,  
    
    
    ///  <summary>
    /// 砸石头工具
    /// </summary>
    BreakTool, 
    
    /// <summary>
    /// 浇水工具
    /// </summary>
    WaterTool, 
    
    ///  <summary>
    /// 收集工具
    /// </summary>
    CollectTool, 
    
    /// <summary>
    /// 可收获的场景装饰
    /// </summary>
    ReapableScenery 
}
/**
 * 描述物品栏位类型的枚举。
 *
 * 定义了三种不同的物品存放位置类型，用于区分游戏中的不同物品存放逻辑或界面展示。
 *
 * @enum Bag 物品背包类型，通常指的是玩家角色携带的个人物品空间。
 * @enum Box 存储箱类型，表示一个用于长期存储物品的空间，区别于随身携带的背包。
 * @enum Shop 商店类型，特指游戏中用于展示可购买或出售物品的界面或系统。
 */
public enum SlotType
{
    /// <summary>
    /// 物品背包
    /// </summary>
    Bag, 
    /// <summary>
    /// 存储箱
    /// </summary>
    Box, 
    /// <summary>
    /// 商店
    /// </summary>
    Shop 
}
/**
 * 库存位置枚举。
 *
 * 此枚举定义了游戏中库存物品可能存放的两种基本位置，主要用于区分玩家直接管理的背包
 * 与用于长期储存的箱子，以便在游戏中进行库存管理和用户界面的定位展示。
 *
 * @enum PlayerBag 玩家背包，指玩家角色直接携带的物品空间，通常用于存放常用或任务相关的物品。
 * @enum Box 箱子，提供额外的储存空间，玩家可以将不常用的物品存放于此，以节省背包空间。
 */
public enum InventoryLocation
{
    /// <summary>
    /// 玩家背包
    /// </summary>
    PlayerBag, 
    /// <summary>
    /// 箱子
    /// </summary>
    Box 
}
