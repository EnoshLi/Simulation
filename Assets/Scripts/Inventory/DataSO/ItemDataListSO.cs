using System.Collections.Generic; 
using UnityEngine; 

/**
 * ItemDataListSO是一个ScriptableObject资产，用于存储和管理游戏内所有物品的详细数据列表。
 * 通过Unity编辑器的资源系统，可以轻松地创建和维护这些数据，便于物品系统的配置和扩展。
 *
 * 【特性】
 * - **创建菜单**: 通过Unity编辑器的"Inventory/ItemDataListSO"菜单项快速生成该类型的资源。
 * - **物品数据列表**: 包含一个`List<ItemDetails>`，用于存储每个物品的详细描述信息实体。
 *
 * 【应用场景】
 * - 配置游戏中所有可用物品的基础属性和行为。
 * - 动态加载不同游戏阶段或模式下的物品列表。
 *
 * 【使用方法】
 * 1. 在Unity项目视图右键 -> "Create" -> "Inventory" -> "ItemDataListSO" 创建新资源。
 * 2. 在Inspector窗口编辑`itemDetailsList`，添加或移除`ItemDetails`条目，配置每项物品的具体数据。
 */
[CreateAssetMenu(fileName = "ItemDataListSO", menuName = "Inventory/ItemDataListSO")] // 自定义资源创建菜单路径
public class ItemDataListSO : ScriptableObject // 继承ScriptableObject以实现资源（asset）功能
{
    /// <summary>
    /// 物品数据列表，保存了游戏中所有物品的详细信息实体。
    /// 每个`ItemDetails`对象代表一种物品，包含名称、描述、图标、类型、堆叠限制等属性。
    /// </summary>
    public List<ItemDetails> itemDetailsList; // 公开列表，可在Inspector中直接编辑
}

