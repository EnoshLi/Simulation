using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class SwitchBounds : MonoBehaviour
{
    //TODO: 后期会改变方法位置
    private void Start()
    {
        SwitchConfinerShape();
    }
    /**
     * 切换限制器形状的私有方法。
     *
     * 此方法用于动态更改摄像机限制区域（CinemachineConfiner）的边界形状，以适配场景变化或游戏状态调整。
     * 它首先通过标签查找场景中的 "BoundsConfiner" 游戏对象，并获取其上的 PolygonCollider2D 组件作为新的边界形状。
     * 随后，将此形状应用于当前游戏对象上的 CinemachineConfiner 组件，以更新摄像机的移动和视角限制范围。
     * 最后，清空原有的路径缓存以确保新边界规则即时生效，避免使用旧的边界信息。
     *
     * 注意：此方法应在具有 CinemachineConfiner 组件的游戏对象上调用，并且场景中需存在标记为 "BoundsConfiner" 的对象。
     */
    private void SwitchConfinerShape()
    {
        // 根据标签找到场景中的边界限制器，并获取其多边形碰撞器组件
        PolygonCollider2D confinerShape = GameObject.FindGameObjectWithTag("BoundsConfiner").GetComponent<PolygonCollider2D>();
    
        // 获取当前游戏对象上的 CinemachineConfiner 组件
        CinemachineConfiner confiner = GetComponent<CinemachineConfiner>();
    
        // 设置 CinemachineConfiner 的边界形状为新获取的多边形碰撞器
        confiner.m_BoundingShape2D = confinerShape;
    
        // 清除之前的路径缓存，确保新的边界约束立即应用
        confiner.InvalidatePathCache();
    }

}
