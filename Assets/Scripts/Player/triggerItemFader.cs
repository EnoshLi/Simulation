using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class TriggerItemFader : MonoBehaviour
{
    /// <summary>
    /// 碰到物体变透明触发器
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter2D(Collider2D other)
    {
        ItemFader[] faders = other.GetComponentsInChildren<ItemFader>();
        if (faders.Length>0)
        {
            foreach (var item in faders)
            {
                item.FadeOut();
            }
        }
    }
    /// <summary>
    /// 物体恢复正常颜色的触发器
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerExit2D(Collider2D other)
    {
        ItemFader[] faders = other.GetComponentsInChildren<ItemFader>();
        if (faders.Length>0)
        {
            foreach (var item in faders)
            {
                item.FadeIn();
            }
        }
    }
}
