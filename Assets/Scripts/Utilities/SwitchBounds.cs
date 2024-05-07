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
    /// <summary>
    /// 获取场景的边界，防止场景穿帮
    /// </summary>
    private void SwitchConfinerShape()
    {
        PolygonCollider2D confinerShape =
            GameObject.FindGameObjectWithTag("BoundsConfiner").GetComponent<PolygonCollider2D>();
        CinemachineConfiner confiner = GetComponent<CinemachineConfiner>();
        confiner.m_BoundingShape2D = confinerShape;
        
        //切换场景时，清除原来的边界信息
        confiner.InvalidatePathCache();
    }
}
