using Unity.VisualScripting;
using UnityEngine;

/**
 * PlayerController脚本控制玩家角色的基本移动行为。
 * 实现了基于输入系统的移动逻辑，支持切换行走速度，并可拓展至新输入系统。
 * 走路的功能未实现
 */
public class PlayerController : MonoBehaviour
{
    /**
     * 基本参数设置，用于控制玩家移动速度。
     */
    [Header("基本参数")] public float normalSpeed; // 正常行走速度

    public float runSpeed; // 奔跑速度
    public float speed; // 当前移动速度（正常或奔跑）
    public Vector2 faceDir; // 玩家面向方向
    private Animator[] _animators; // 动画组件
    public bool isMove; //移动判断
    private bool inputDisable;

    /**
     * 基本组件引用，用于物理移动和碰撞检测。
     */
    [Header("基本组件")] private Rigidbody2D rd; // 2D刚体组件，用于物理模拟

    private BoxCollider2D coll; // 碰撞盒组件，用于碰撞检测
    //private InventoryUI inventoryUI; // 库存UI引用（已注释，未使用）

    /**
     * 新输入系统集成，支持更灵活的输入控制。
     */
    [Header("新输入系统")] private Simulation inputSystem; // 自定义输入系统实例


    /**
     * 初始化组件引用和速度参数。
     */
    private void Awake()
    {
        rd = GetComponent<Rigidbody2D>(); // 获取刚体组件
        coll = GetComponent<BoxCollider2D>(); // 获取碰撞盒组件
        inputSystem = new Simulation(); // 初始化输入系统
        speed = normalSpeed; // 默认速度设置为正常速度
        _animators = GetComponentsInChildren<Animator>();
    }


    private void OnEnable()
    {
        inputSystem.Enable(); // 启用输入系统
        EventHandle.BeforeSceneUnloadEvent += OnBeforeSceneUnloadEvent; // 在场景卸载前订阅事件
        EventHandle.AfterSceneLoadedEvent += OnAfterSceneLoadedEvent; // 在场景加载后订阅事件
        EventHandle.MoveToPosition += OnMoveToPosition; // 订阅移动到位置的事件
    }


    private void OnDisable()
    {
        inputSystem.Disable(); // 禁用输入系统
        EventHandle.BeforeSceneUnloadEvent -= OnBeforeSceneUnloadEvent; // 取消订阅场景卸载前的事件
        EventHandle.AfterSceneLoadedEvent -= OnAfterSceneLoadedEvent; // 取消订阅场景加载后的事件
        EventHandle.MoveToPosition -= OnMoveToPosition; // 取消订阅移动到位置的事件
    }

    /**
     * 当收到移动到指定位置的事件时调用的方法。
     * @param targetPosition 目标位置的向量。
     */
    private void OnMoveToPosition(Vector3 targetPosition)
    {
        transform.position = targetPosition; // 将当前游戏对象的transform位置设置为目标位置
    }

    /**
     * 当场景加载完成后调用的方法。
     * 主要用于重置输入系统为可用状态。
     */
    private void OnAfterSceneLoadedEvent()
    {
        inputDisable = false; // 设置输入系统为可用
    }

    /**
     * 当场景卸载前调用的方法。
     * 主要用于禁用输入系统，避免在场景切换过程中产生错误的输入。
     */
    private void OnBeforeSceneUnloadEvent()
    {
        inputDisable = true; // 设置输入系统为禁用
    }


    /**
     * 每帧更新，读取玩家输入的方向。
     */
    private void Update()
    {
        faceDir = inputSystem.Player.Move.ReadValue<Vector2>(); // 读取移动输入
        SwitchAnimation();
    }

    /**
     * 固定时间间隔更新，处理物理移动。
     */
    private void FixedUpdate()
    {
        if (!inputDisable)
        {
            Move(); // 执行移动逻辑
        }
    }


    /**
     * 实现玩家的移动逻辑。
     * 根据面向方向和当前速度调整角色位置。
     */
    private void Move()
    {
        rd.MovePosition(transform.localPosition + (Vector3)faceDir * (speed * Time.deltaTime)); // 更新角色位置
    }

    /**
     * 切换角色动画状态的方法。
     *
     * 此方法利用C# 9.0引入的模式匹配语法来判断角色的移动方向向量(faceDir)是否为非零向量，即角色是否正在移动。
     * 如果faceDir的x分量和y分量均不为0，则认为角色正在移动，更新动画状态以反映这一变化。
     *
     * 方法流程：
     * 1. **检测移动状态**：使用模式匹配检查`faceDir`是否在x轴和y轴上都有非零分量，据此设置布尔值`isMove`。
     * 2. **遍历动画控制器**：对关联的所有`_animators`进行迭代。
     * 3. **更新动画参数**：如果角色正在移动(`isMove == true`)，则设置动画参数`IsMoving`为true，并根据`faceDir`的x和y分量设置动画的水平和垂直输入参数。
     *     - 这会影响角色在动画中的视觉表现，如切换行走或跑步动画，并调整动画的方向。
     *
     * 注意：此方法依赖于`_animators`集合包含角色的所有动画控制器实例，以及`Settings`类中定义的动画参数常量（如`IsMoving`, `InputX`, `InputY`）。
     */
    private void SwitchAnimation()
    {
        // 使用C#的模式匹配功能来判断向量faceDir是否是非零向量。
        // 具体来说，该表达式检查：
        // 1. faceDir 是否是一个具有 x 和 y 分量的对象。
        // 2. faceDir 的 x 分量是否不等于 0。
        // 3. faceDir 的 y 分量是否也不等于 0。
        // 如果上述所有条件都满足，则说明faceDir不是指向原点的方向向量（即有非零的水平或垂直移动分量），
        // 此时表达式的值为 true，否则为 false。
        isMove = faceDir is not { x: 0, y: 0 };

        foreach (var animator in _animators)
        {
            animator.SetBool(Settings.IsMoving, isMove);
            if (isMove)
            {
                animator.SetFloat(Settings.InputX, faceDir.x);
                animator.SetFloat(Settings.InputY, faceDir.y);
            }
        }
    }
}