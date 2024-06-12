using TMPro;
using UnityEngine;
using UnityEngine.UI;

/**
 * <summary>
 * 这个类表示一个用于展示游戏内物品详细信息的工具提示UI组件。
 * 它被设计为附着在Unity游戏对象上，并根据提供的数据来设置物品详情。
 * </summary>
 */
public class ItemToolTip : MonoBehaviour
{
    // 可在Unity编辑器中设置的字段
    [SerializeField] private TextMeshProUGUI nameText; // 显示物品的名称
    [SerializeField] private TextMeshProUGUI typeText; // 显示物品的类型（作为字符串）
    [SerializeField] private TextMeshProUGUI descriptionText; // 描述物品的文本
    [SerializeField] private Text valueText; // 显示物品价值的文本，当前方法中未直接使用
    [SerializeField] private GameObject bottomPart; // 一个UI部件，根据物品类型决定是否激活显示

    /**
     * <summary>
     * 根据传入的物品详情和槽位类型设置工具提示的内容。
     * 对于种子、商品或家具类型，会根据槽位类型调整价格并显示底部部件；
     * 其他类型则不显示底部部件。
     * </summary>
     * <param name="itemDetails">包含物品信息的对象</param>
     * <param name="slotType">物品所在的槽位类型</param>
     */
    public void SetUpToolTip(ItemDetails itemDetails, SlotType slotType)
    {
        nameText.text = itemDetails.itemName; // 设置物品名称
        typeText.text = GetItemType(itemDetails.itemType); // 设置物品类型文本
        descriptionText.text =itemDetails.itemDescription ; // 设置物品描述文本
        
        
        // 根据物品类型判断是否显示价格部分
        if (itemDetails.itemType == ItemType.Seed || 
            itemDetails.itemType == ItemType.Commodity || 
            itemDetails.itemType == ItemType.Furniture)
        {
            bottomPart.SetActive(true); // 激活底部部件
            
            // 计算并可能调整价格显示（此处未直接设置valueText，需补充逻辑）
            var price = itemDetails.itemPrice;
            if (slotType == SlotType.Bag)
            {
                price = (int)(price * itemDetails.sellPercentage); // 如果在背包槽位，应用出售百分比
            }
            // 注意：调整价格后的显示逻辑需要实现，
            valueText.text = price.ToString();
        }
        else
        {
            bottomPart.SetActive(false); // 非指定类型时不显示底部部件
        }
        
        // 强制立即重建当前游戏对象的RectTransform组件所对应的布局。
        // 这通常用于在运行时动态改变UI布局后，确保UI元素能立即按照新的布局要求更新显示，
        // 而不是等待下一帧的布局更新。这对于需要即时反馈的UI调整非常有用。
        LayoutRebuilder.ForceRebuildLayoutImmediate(GetComponent<RectTransform>());
    }

    private string GetItemType(ItemType itemType)
    {
        return  itemType switch
        {
            ItemType.Seed => "种子",
            ItemType.Commodity => "商品",
            ItemType.Furniture => "家具",
            ItemType.BreakTool => "工具",
            ItemType.CollectTool => "工具",
            ItemType.ChopTool => "工具",
            ItemType.HoeTool => "工具",
            ItemType.WaterTool => "工具",
                _ => "无"
            };
    }
}

