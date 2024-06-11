using UnityEngine; 

/**
 * PlayerController脚本控制玩家角色的基本移动行为。
 * 实现了基于输入系统的移动逻辑，支持切换行走速度，并可拓展至新输入系统。
 */
public class PlayerController : MonoBehaviour
{
    /**
     * 基本参数设置，用于控制玩家移动速度。
     */
    [Header("基本参数")]
    public float normalSpeed; // 正常行走速度
    public float runSpeed;   // 奔跑速度
    public float speed;      // 当前移动速度（正常或奔跑）
    public Vector2 faceDir;  // 玩家面向方向

    /**
     * 基本组件引用，用于物理移动和碰撞检测。
     */
    [Header("基本组件")]
    private Rigidbody2D rd;          // 2D刚体组件，用于物理模拟
    private BoxCollider2D coll;       // 碰撞盒组件，用于碰撞检测
    //private InventoryUI inventoryUI; // 库存UI引用（已注释，未使用）

    /**
     * 新输入系统集成，支持更灵活的输入控制。
     */
    [Header("新输入系统")]
    private Simulation inputSystem; // 自定义输入系统实例

    /**
     * 初始化组件引用和速度参数。
     */
    private void Awake()
    {
        rd = GetComponent<Rigidbody2D>(); // 获取刚体组件
        coll = GetComponent<BoxCollider2D>(); // 获取碰撞盒组件
        inputSystem = new Simulation(); // 初始化输入系统
        speed = normalSpeed; // 默认速度设置为正常速度
    }

    /**
     * 启用时激活输入系统。
     */
    private void OnEnable()
    {
        inputSystem.Enable();
    }

    /**
     * 每帧更新，读取玩家输入的方向。
     */
    private void Update()
    {
        faceDir = inputSystem.Player.Move.ReadValue<Vector2>(); // 读取移动输入
        // Debug.LogWarning 显示鼠标世界坐标示例（注释掉以避免日志输出）
    }

    /**
     * 固定时间间隔更新，处理物理移动。
     */
    private void FixedUpdate()
    {
        if (faceDir.x <= 0.5f || faceDir.y < 0.5f) // 当移动输入不全为正时（简化条件判断）
        {
            Move(); // 执行移动逻辑
        }
    }

    /**
     * 禁用时关闭输入系统。
     */
    private void OnDisable()
    {
        inputSystem.Disable();
    }

    /**
     * 实现玩家的移动逻辑。
     * 根据面向方向和当前速度调整角色位置。
     */
    private void Move()
    {
        rd.MovePosition(transform.localPosition + (Vector3)faceDir * (speed * Time.deltaTime)); // 更新角色位置
    }
}