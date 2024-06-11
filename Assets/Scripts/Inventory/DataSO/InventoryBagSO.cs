using System.Collections.Generic; 
using UnityEngine; 

/**
 * InventoryBagSO（ScriptableObject）是一种可通过资源窗口创建的库存背包数据容器。
 * 它允许开发者通过Unity编辑器直接编辑和维护背包内物品的列表，便于库存系统的配置和管理。
 *
 * 【特性】
 * - 可通过Unity菜单"Inventory/InventoryBagSO"快速创建新的库存背包数据资产。
 * - 包含一个`ItemList`列表，用于存储`InventoryItem`类型的对象，每个对象代表背包内的一个物品及其数量。
 *
 * 【使用场景】
 * - 设计游戏内角色的初始背包配置。
 * - 动态加载不同的背包配置以适应游戏的不同阶段或角色需求。
 *
 * 【创建方式】
 * 在Unity编辑器中，选择"Assets/Create/Inventory/InventoryBagSO"菜单项即可创建一个新的背包配置资源。
 */
[CreateAssetMenu(fileName = "InventoryBagSO", menuName = "Inventory/InventoryBagSO")] // 定义资源创建菜单路径和文件名模板
public class InventoryBagSO : ScriptableObject // 继承自ScriptableObject，使其成为可序列化的资源对象
{
    /**
     * `ItemList` 是一个列表，存储了背包内所有物品的详细信息。
     * 每个`InventoryItem`条目都包含了物品的唯一ID和该物品在背包中的数量。
     */
    public List<InventoryItem> ItemList; // 公开的ItemList成员变量，用于在Inspector面板中编辑物品列表
}

